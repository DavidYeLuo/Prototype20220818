namespace Drivers.Abilities.Irelia
{
    /// <summary>
    /// Interface for Objects/Entities with ability(s)
    /// </summary>
    public interface IAbilityUser
    {
        /// <summary>
        /// Use desired ability.
        /// </summary>
        /// <param name="n">Desired ability</param>
        /// <remarks>Abilities starts from 1</remarks>
        public void UseAbility(int n);
    }
}