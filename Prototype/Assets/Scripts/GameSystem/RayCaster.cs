using UnityEngine;

namespace GameSystem
{
    public class RayCaster : MonoBehaviour
    {
        [Header("Raycast Source")]
        [SerializeField] private Camera cam;
        public bool MouseOverObject(out RaycastHit hit)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out hit);
        }
    }
}