namespace Interfaces
{
    public interface IMagazine
    {
        IAmmo Ammo { get; }
        int Capacity { get; }

        void FullRecharge();

        void Recharge(int number);

        IAmmo Release();
    }
}
