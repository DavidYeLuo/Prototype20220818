using UnityEngine;

namespace Player
{
    public interface IMove
    {
        public void Move(Vector3 position);
        public void Move(Vector3 position, float speed);
    }
}