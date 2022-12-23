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
        [SerializeField] private float speed; // Base Speed
        [SerializeField] private Vector3 groundOffset;
        
        [Header("Debug Movement")]
        private Movement movementDriver;
        private Coroutine movementCoroutine;

        [SerializeField] private List<Ability> abilityList;

        void Start()
        {
            movementDriver = new Movement(this.gameObject, Time.deltaTime, groundOffset);
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
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(movementDriver.Move_Coroutine(position, speed));
        }
        
        /**
         * Moves the character at a set speed
         */
        public void Move(Vector3 position, float speed)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(movementDriver.Move_Coroutine(position, speed));
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public void UseAbility(int n)
        {
            abilityList[n].PerformAbility();
        } 
    }
}