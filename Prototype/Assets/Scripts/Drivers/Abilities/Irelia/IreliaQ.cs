using System;
using Drivers.Cursor;
using Drivers.Health;
using Drivers.Movement;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drivers.Abilities.Irelia
{
    /// <summary>
    /// Unity Component is an ability. <br/>
    /// The ability makes the current character to dash toward an enemy. <br/>
    /// If the character Q ability hits an enemy that has Irelia mark,
    /// then this Q ability's cooldown is reset.
    /// </summary>
    /// <remarks>
    /// TODO: 1. Q shouldn't work on non-enemy. (Shouldn't work non entity)
    /// TODO: 2. Add cooldown.
    /// TODO: 3. Add mark implementation.
    /// </remarks>
    public class IreliaQ : Ability
    {
        [Header("Config")]
        [SerializeField] private float modifiedSpeed;
        
        [Space]
        [Header("Dependencies")]
        [SerializeField] private GameObject movementInterface;
        [SerializeField] private GameObject cursorInterface;
        [SerializeField] private GameObject hitbox;

        private IMove movement;
        private ICursorPosition cursor;

        public void Start()
        {
            movement = movementInterface.GetComponent<IMove>();
            cursor = cursorInterface.GetComponent<ICursorPosition>();

            if (movement == null) Debug.Log("Warning movement is null.");
            if (cursor == null) Debug.Log("Warning cursor is null.");
            
            hitbox.SetActive(false);
        }

        /// <summary>
        /// Dash implementation: <br/>
        /// Dash toward the enemy and damage them.
        /// Reset cooldown when mark is hit.
        /// </summary>
        /// <hit>
        /// Current implementation for Hit:
        /// Enable this character's hitbox to hit the other object.
        /// </hit>
        /// <dash>
        /// Current implementation for dash:
        /// Set 1 move command for the character to the specific location.
        /// </dash>
        public override void PerformAbility()
        {
            hitbox.SetActive(true);
            movement.Move(cursor.GetCursorPosition(), modifiedSpeed);
        }
    }
}