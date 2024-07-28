using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotAnimator : MonoBehaviour
    {
        private const string BOT_IDLE = "Bot_Idle";
        private const string BOT_MOVE = "Bot_Move";

        [SerializeField] private Animator _animator;

        public void Initialize()
        {
            AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo.Length > 0)
            {
                float clipLength = clipInfo[0].clip.length;
                float randomStartTime = Random.Range(0f, clipLength);

                _animator.Play(clipInfo[0].clip.name, 0, randomStartTime / clipLength);
            }
        }

        public void PlayIdle() =>
            _animator.Play(BOT_IDLE);

        public void PlayMove() =>
            _animator.Play(BOT_MOVE);
    }
}
