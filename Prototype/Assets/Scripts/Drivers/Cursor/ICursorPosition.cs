using UnityEngine;

namespace Entity
{
    public interface ICursorPosition
    {
        public Vector3 GetCursorPosition();
        public void SetCursorPosition(Vector3 position);
    }
}