using Entity;
using UnityEngine;

namespace Player.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        protected IAbilityUser AbilityUser;

        public void Init(IAbilityUser abilityUser)
        {
            this.AbilityUser = abilityUser;
        }
        public abstract void PerformAbility();
    }
}