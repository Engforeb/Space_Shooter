namespace Interfaces
{
    public interface IMagazine
    {
        IAmmo Ammo { get; }
        int Capacity { get; }

        void FullRecharge();

        IAmmo Recharge(int number);
    }
}
