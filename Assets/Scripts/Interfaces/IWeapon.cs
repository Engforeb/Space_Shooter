namespace Interfaces
{
    public interface IWeapon
    {
        float FireRate { get; }
        IMagazine Magazine { get; }
        void Shoot();
    }
}
