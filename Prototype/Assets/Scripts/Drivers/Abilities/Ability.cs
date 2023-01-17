using Entity;
using UnityEngine;

namespace Drivers.Abilities.Irelia
{
    /// <summary>
    /// Template for an ability. 
    /// </summary>
    public abstract class Ability : MonoBehaviour
    {
        protected IAbilityUser AbilityUser; // TODO: Find some uses

        /// <summary>
        /// Cache the game object with the ability.
        /// </summary>
        /// <param name="abilityUser"></param>
        public void Init(IAbilityUser abilityUser)
        {
            this.AbilityUser = abilityUser;
        }
        /// <summary>
        /// Performs an ability
        /// </summary>
        public abstract void PerformAbility();
    }
}