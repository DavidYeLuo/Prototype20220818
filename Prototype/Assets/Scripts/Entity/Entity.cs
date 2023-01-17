using System.Collections.Generic;
using Drivers.Abilities.Irelia;
using Drivers.Cursor;
using Drivers.Health;
using Drivers.Movement;
using UnityEngine;

namespace Entity
{
    /// <summary>
    /// Unity component that represents the entity. <br/>
    /// This should be the primary way to interact to the entity. <br/>
    /// </summary>
    /// <cursor>
    /// Uses a virtual cursor system that some abilities require. <br/>
    /// This virtual is to separate cursor dependencies. <br/>
    /// Meaning that we can have AI control our character if we want to <br/>
    /// without needing to create a new class <br/>
    /// </cursor>
    public class Entity : MonoBehaviour, IAbilityUser, IMove, ICursorPosition, IHealth
    {
        [Header("Drivers/Components")] 
        [SerializeField] private Health health;
        [SerializeField] private CursorPos cursor;
        [SerializeField] private Movement movement;
        [SerializeField] private List<Ability> abilityList;
        
        void Start()
        {
            foreach (var ability in abilityList)
            {
                ability.Init(this);
            }
        }

        /// <summary>
        /// Moves the current entity at base speed.
        /// </summary>
        /// <param name="position">Move to</param>
        public void Move(Vector3 position)
        {
            movement.Move(position);
        }
        
        /// <summary>
        /// Moves the current entity at a specific speed
        /// </summary>
        /// <param name="position">Move to</param>
        /// <param name="speed">Custom speed</param>
        public void Move(Vector3 position, float speed)
        {
            movement.Move(position, speed);
        }

        /// <summary>
        /// Uses Entity's ability. <br/>
        /// Some entity have multiple abilities,
        /// so they are accessed through an index
        /// Index starts from 1
        /// </summary>
        /// <param name="n">index</param>
        public void UseAbility(int n)
        {
            abilityList[n].PerformAbility();
        }

        /// <summary>
        /// Accessor for current cursor position
        /// </summary>
        /// <returns>cursor position</returns>
        public Vector3 GetCursorPosition()
        {
            return cursor.GetPosition();
        }

        /// <summary>
        /// Sets the cursor position for the entity <br/>
        /// Some abilities require the player's cursor. <br/>
        /// </summary>
        /// <param name="position"></param>
        public void SetCursorPosition(Vector3 position)
        {
            cursor.SetPosition(position);
        }

        /// <summary>
        /// Adds a given amount to the current health
        /// </summary>
        /// <param name="amount">Heal amount</param>
        public void Heal(int amount)
        {
            health.IncreaseHealth(amount);
        }

        /// <summary>
        /// Subtracts a given amount to the current health
        /// </summary>
        /// <param name="amount">damage amount</param>
        public void TakeDamage(int amount)
        {
            health.DecreaseHealth(amount);
        }
    }
}