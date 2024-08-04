using FactoryBots.App.Services;
using FactoryBots.Menu;
using FactoryBots.Game;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FactoryBots.App.States
{
    public class MenuState : IPayloadedState<MenuPanel>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        private MenuPanel _menuPanel;

        public MenuState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(MenuPanel menuPanel)
        {
            _menuPanel = menuPanel;
            _menuPanel.StartAction += LaunchGame;
            _menuPanel.ExitAction += QuitGame;
        }

        private void LaunchGame(GameMode gameMode)
        {
            Unsubscribe();
            _menuPanel.gameObject.SetActive(false);

            _appStateMachine.Enter<LaunchGameState, GameMode>(gameMode);
        }

        private void QuitGame()
        {
            Unsubscribe();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        private void Unsubscribe()
        {
            _menuPanel.StartAction -= LaunchGame;
            _menuPanel.ExitAction -= QuitGame;
        }

        public void Exit() { }
    }
}
