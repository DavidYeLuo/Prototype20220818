using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform objectToMove;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 groundOffset;

        private float timeStep;

        [Header("Debug")]
        [SerializeField] private bool isMoving;
        
        [SerializeField] private Vector3 moveToward;
        [SerializeField] private Vector3 destination;
        
        private WaitForSeconds waitForShortTime;
        
        private Coroutine movementCoroutine;

        public void Start()
        {
            waitForShortTime = new WaitForSeconds(timeStep);
            isMoving = false;
            timeStep = Time.deltaTime;
        }

        public bool IsMoving()
        {
            return isMoving;
        }
        
        public void Move(Vector3 dest)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(Move_Coroutine(dest, speed));
        }
        
        public void Move(Vector3 dest, float speed)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(Move_Coroutine(dest, speed));
        }

        private IEnumerator Move_Coroutine(Vector3 dest, float speed)
        {
            destination = dest;

            isMoving = true;
            while (Vector3.Distance(objectToMove.position, dest) > 0.1)
            {
                moveToward = Vector3.MoveTowards(objectToMove.position, destination + groundOffset, speed * timeStep);
                objectToMove.position = moveToward;

                yield return waitForShortTime;
            }

            isMoving = false;
        }
    }
}