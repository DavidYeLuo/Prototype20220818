using System.Collections.Generic;
using Drivers.Health;
using Entity;
using Player.Abilities;
using UnityEngine;

namespace Player
{
    /**
     * Prototype code: Note this is a monolithic class.
     * Purpose of this class is for showcase proposed ideas.
     */
    public class Entity : MonoBehaviour, IAbilityUser, IMove, ICursorPosition, IHealth
    {
        [Header("Drivers")] 
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

        /**
         * Moves the character at base speed
         */
        public void Move(Vector3 position)
        {
            movement.Move(position);
        }
        
        /**
         * Moves the character at a set speed
         */
        public void Move(Vector3 position, float speed)
        {
            movement.Move(position, speed);
        }

        public void UseAbility(int n)
        {
            abilityList[n].PerformAbility();
        }

        public Vector3 GetCursorPosition()
        {
            return cursor.GetPosition();
        }

        public void SetCursorPosition(Vector3 position)
        {
            cursor.SetPosition(position);
        }

        public void Heal(int amount)
        {
            health.IncreaseHealth(amount);
        }

        public void TakeDamage(int amount)
        {
            health.DecreaseHealth(amount);
        }
    }
}