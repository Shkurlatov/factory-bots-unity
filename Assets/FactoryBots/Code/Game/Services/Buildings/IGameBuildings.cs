namespace FactoryBots.Game.Services.Buildings
{
    public interface IGameBuildings : IGameService
    {
        void Initialize(BoxFactory boxFactory);
    }
}
