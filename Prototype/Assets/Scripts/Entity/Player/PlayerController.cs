using System;
using Entity;
using GameSystem;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private RayCaster rayCaster;

        private IAbilityUser abilityUserDriver;
        private IMove movementDriver;
        private ICursorPosition cursorPositionDriver;

        private Vector3 cursorPosition;

        public void Start()
        {
            abilityUserDriver = player.GetComponent<IAbilityUser>();
            movementDriver = player.GetComponent<IMove>();
            cursorPositionDriver = player.GetComponent<ICursorPosition>();
            
            if(abilityUserDriver == null)
                Debug.Log("Warning abilityUserDriver is NULL");
            if(movementDriver == null)
                Debug.Log("Warning movementDriver is NULL");
            
            cursorPosition = player.transform.forward;
        }

        public void MovePlayerTowardMousePosition()
        {
            GetCursorPosition();
            cursorPositionDriver.SetCursorPosition(cursorPosition);
            movementDriver.Move(cursorPosition);
        }

        /**
         * Note: Ability starts from 1 to n (Doesn't start from 0)
         */
        public void UsePlayerAbility(int n)
        {
            GetCursorPosition();
            cursorPositionDriver.SetCursorPosition(cursorPosition);
            abilityUserDriver.UseAbility(n-1);
        }

        public void GetCursorPosition()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                cursorPosition = hit.point;
            }
        }
    }
}