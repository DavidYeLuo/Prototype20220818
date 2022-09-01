using GameSystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private RayCaster rayCaster;

        public void MovePlayerTowardMousePosition()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                player.Move(hit.point);
            }
        }

        /**
         * Note: Ability starts from 1 to n (Doesn't start from 0)
         */
        public void UsePlayerAbility(int n)
        {
            player.UseAbility(n-1);
        }
    }
}