namespace Drivers.Health
{
    public interface IHealth
    {
        public void Heal(int amount);
        public void TakeDamage(int amount);
    }
}