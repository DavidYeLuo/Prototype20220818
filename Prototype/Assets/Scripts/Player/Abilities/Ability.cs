using UnityEngine;

namespace Player.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        protected Player player;

        public void Init(Player player)
        {
            this.player = player;
        }
        public abstract void PerformAbility();
    }
}