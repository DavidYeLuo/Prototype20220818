using UnityEditorInternal;
using UnityEngine;

namespace Entity
{
    public class CursorPos : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private Vector3 position;

        public void SetPosition(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 GetPosition()
        {
            return position;
        }
    }
}