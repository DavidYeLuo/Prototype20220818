namespace Drivers.Health
{
    /// <summary>
    /// Interface for objects/entities that supports health capabilities
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// Adds the amount of health to the current health.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount);
        /// <summary>
        /// Subtracts the amount of health to the current health.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount);
    }
}