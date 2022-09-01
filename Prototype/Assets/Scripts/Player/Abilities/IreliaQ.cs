using UnityEngine;

namespace Player.Abilities
{
    public class IreliaQ : Ability
    {
        public override void PerformAbility()
        {
           player.Speed += 2;
        }
    }
}