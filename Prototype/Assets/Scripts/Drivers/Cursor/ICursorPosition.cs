using UnityEngine;

namespace Entity
{
    /// <summary>
    /// Interface for objects/entities that have a cursor. <br/>
    /// </summary>
    /// <example>
    /// Player's mouse position
    /// </example>
    public interface ICursorPosition
    {
        /// <summary>
        /// Accessor for where the object's cursor is.
        /// </summary>
        /// <returns>Position of the cursor</returns>
        public Vector3 GetCursorPosition();
        /// <summary>
        /// Sets where the cursor is.
        /// </summary>
        /// <param name="position">Desired position</param>
        public void SetCursorPosition(Vector3 position);
    }
}