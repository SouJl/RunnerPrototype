namespace Features.Storage.Upgrade
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        void Restore();
    }
}
