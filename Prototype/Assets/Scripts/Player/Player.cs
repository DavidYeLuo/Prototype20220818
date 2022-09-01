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
        [SerializeField] private float speed;
        [SerializeField] private float groundOffset;

        // [SerializeField] private GameObject ePillar;

        [Header("Debug Movement")] 
        [SerializeField] private Vector3 moveToward;
        [SerializeField] private Vector3 destination;

        [SerializeField] private List<Ability> abilityList;

        void Start()
        {
            foreach (var ability in abilityList)
            {
                ability.Init(this);
            }
        }

        public void Move(Vector3 position)
        {
            destination = position;
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
        

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 offset = Vector3.up * groundOffset;
            moveToward = Vector3.MoveTowards(transform.position, destination + offset, speed * Time.deltaTime);
            transform.position = moveToward;
        }
    }
}