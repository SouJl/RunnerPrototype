namespace Runner.Features.Storage.Upgrade
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        float JumpHeight { get; set; }
        float Health { get; set; }

        void Restore();
    }
}
