using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class KeyListener : MonoBehaviour
    {
        [SerializeField] private KeyCode key;
        [SerializeField] private UnityEvent keyDown;
        [SerializeField] private UnityEvent keyUp;
        [SerializeField] private UnityEvent keyHold;
        
        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                keyDown.Invoke();
            }
            else if (Input.GetKeyUp(key))
            {
                keyUp.Invoke();
            }
            else if (Input.GetKey(key))
            {
                keyHold.Invoke();
            }
        }
    }
}