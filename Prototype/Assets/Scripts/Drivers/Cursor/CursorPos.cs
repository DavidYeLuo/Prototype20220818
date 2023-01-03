using UnityEditorInternal;
using UnityEngine;

namespace Entity
{
    /// <summary>
    /// Unity Component that handles with cursor
    /// </summary>
    public class CursorPos : MonoBehaviour
    {
        [Header("Debug")]
        // Cursor's position
        [SerializeField] private Vector3 position;

        /// <summary>
        /// Sets the cursor's position
        /// </summary>
        /// <param name="pos">Desired position</param>
        public void SetPosition(Vector3 pos)
        {
            position = pos;
        }

        /// <summary>
        /// Accessor for the cursor's position
        /// </summary>
        /// <returns>Cursor's position</returns>
        public Vector3 GetPosition()
        {
            return position;
        }
    }
}