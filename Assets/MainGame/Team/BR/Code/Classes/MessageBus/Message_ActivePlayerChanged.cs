using Assets.MainGame.Team.BR.Code.Enumerations;

namespace Assets.MainGame.Team.BR.Code.Classes.MessageBus
{
    public struct Message_ActivePlayerChanged
    {
        public PlayerLocations UpdatedActivePlayer;
    }
}