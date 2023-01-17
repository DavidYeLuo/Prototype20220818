using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Drivers.Movement
{
    /// <summary>
    /// Unity Component that moves the object
    /// by directly setting the position over time.
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform objectToMove;
        [SerializeField] private float speed; // TODO: rename this to baseSpeed
        [SerializeField] private Vector3 groundOffset;

        private float timeStep;

        [Header("Debug")]
        [SerializeField] private bool isMoving;
        
        [SerializeField] private Vector3 moveToward;
        [SerializeField] private Vector3 destination;
        
        private WaitForSeconds waitForShortTime;
        
        private Coroutine movementCoroutine;

        // TODO: Should be a private function
        public void Start()
        {
            waitForShortTime = new WaitForSeconds(timeStep);
            isMoving = false;
            timeStep = Time.deltaTime;
        }

        /// <summary>
        /// Getter to the moving state of the entity.
        /// </summary>
        /// <returns>true => it is moving</returns>
        public bool IsMoving()
        {
            return isMoving;
        }
        
        /// <summary>
        /// Moves the Entity by setting its position closer over time.
        /// </summary>
        /// <param name="dest">Destination</param>
        /// <remarks>Moves at the base speed</remarks>
        public void Move(Vector3 dest)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(Move_Coroutine(dest, speed));
        }
        
        /// <summary>
        /// Moves the Entity by settings its position closer over time.
        /// </summary>
        /// <param name="dest">Destination</param>
        /// <param name="speed">TODO: Add unit of speed?</param>
        public void Move(Vector3 dest, float speed)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(Move_Coroutine(dest, speed));
        }

        /// <summary>
        /// Implementation of the move(...) functions
        /// </summary>
        /// <param name="dest">Destination</param>
        /// <param name="speed">TODO: Add unit of speed?</param>
        /// <returns>coroutine signature</returns>
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