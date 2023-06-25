namespace Assets.MainGame.Team.BR.Code.Classes.MessageBus
{
    public struct Message_PlayerScored
    {
        public int hits;
        public float ballPositionX { get; set; }
    }
}