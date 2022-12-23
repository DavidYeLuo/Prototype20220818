using System.Collections.Generic;
using Player.Abilities;
using UnityEngine;

namespace Player
{
    /**
     * Prototype code: Note this is a monolithic class.
     * Purpose of this class is for showcase proposed ideas.
     */
    public class Player : MonoBehaviour
    {
        [Header("Drivers")]
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
    }
}