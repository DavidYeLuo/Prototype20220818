using UnityEngine;

namespace Drivers.Movement
{
    /// <summary>
    /// Interface for objects/entities that can move
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Interface that moves the entity at a base speed
        /// </summary>
        /// <param name="position"></param>
        public void Move(Vector3 position);
        /// <summary>
        /// Interface that moves the entity at a set speed
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public void Move(Vector3 position, float speed);
    }
}