using System;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Abilities
{
    public class IreliaQ : Ability
    {
        [Header("Config")]
        [SerializeField] private float modifiedSpeed;
        
        [Space]
        [Header("Dependencies")]
        [SerializeField] private GameObject movementInterface;
        [SerializeField] private GameObject cursorInterface;

        private IMove movement;
        private ICursorPosition cursor;

        public void Start()
        {
            movement = movementInterface.GetComponent<IMove>();
            cursor = cursorInterface.GetComponent<ICursorPosition>();

            if (movement == null) Debug.Log("Warning movement is null.");
            if (cursor == null) Debug.Log("Warning cursor is null.");
        }

        public override void PerformAbility()
        {
            movement.Move(cursor.GetCursorPosition(), modifiedSpeed);
        }
    }
}