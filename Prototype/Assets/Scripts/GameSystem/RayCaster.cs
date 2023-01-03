using UnityEngine;

namespace GameSystem
{
    /// <summary>
    /// Unity Component that handles with ray casting.
    /// </summary>
    public class RayCaster : MonoBehaviour
    {
        [Header("Raycast Source")]
        // Camera to raycast from
        [SerializeField] private Camera cam;
        
        /// <summary>
        /// Gives the hit information of the position of the player's cursor. <br/>
        /// Hit information contains more than just the object it hit.
        /// </summary>
        /// <param name="hit">Contains information about the selected object.</param>
        /// <returns>True when the mouse is on a game object.</returns>
        public bool MouseOverObject(out RaycastHit hit)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out hit);
        }
    }
}