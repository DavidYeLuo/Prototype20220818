using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Movement
    {
        private Transform transform;
        
        private float speed;
        private Vector3 groundOffset;

        private Vector3 moveToward;
        private Vector3 destination;

        private float timeStep;

        private bool isMoving;
        
        private WaitForSeconds waitForShortTime;

        public Movement(GameObject gameObject, float timeStep, Vector3 groundOffset)
        {
            transform = gameObject.transform;
            this.timeStep = timeStep;
            this.groundOffset = groundOffset;
            waitForShortTime = new WaitForSeconds(timeStep);
            isMoving = false;
        }

        public bool IsMoving()
        {
            return isMoving;
        }

        public IEnumerator Move_Coroutine(Vector3 dest, float speed)
        {
            destination = dest;

            isMoving = true;
            while (Vector3.Distance(transform.position, dest) > 0.1)
            {
                moveToward = Vector3.MoveTowards(transform.position, destination + groundOffset, speed * timeStep);
                transform.position = moveToward;

                yield return waitForShortTime;
            }

            isMoving = false;
        }
    }
}