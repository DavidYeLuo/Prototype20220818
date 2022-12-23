using GameSystem;
using UnityEngine;

namespace Player.Abilities
{
    public class IreliaQ : Ability
    {
        [SerializeField] private Player player;
        [SerializeField] private RayCaster rayCaster;

        [Header("Properties")]
        [SerializeField] private float modifiedSpeed;
        
        public override void PerformAbility()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                player.Move(hit.point, modifiedSpeed);
            }
        }
    }
}