using System;
using Entity;
using GameSystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private RayCaster rayCaster;

        private IAbilityUser abilityUserDriver;
        private IMove movementDriver;

        public void Start()
        {
            abilityUserDriver = player.GetComponent<IAbilityUser>();
            movementDriver = player.GetComponent<IMove>();
            
            if(abilityUserDriver == null)
                Debug.Log("Warning abilityUserDriver is NULL");
            if(movementDriver == null)
                Debug.Log("Warning movementDriver is NULL");
        }

        public void MovePlayerTowardMousePosition()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                movementDriver.Move(hit.point);
            }
        }

        /**
         * Note: Ability starts from 1 to n (Doesn't start from 0)
         */
        public void UsePlayerAbility(int n)
        {
            abilityUserDriver.UseAbility(n-1);
        }
    }
}