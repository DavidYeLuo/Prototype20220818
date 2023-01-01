using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    /// <summary>
    /// Unity Component that maps a specific keyboard key to a script function.
    /// </summary>
    public class KeyListener : MonoBehaviour
    {
        [Header("Key Option")]
        [SerializeField] private KeyCode key;
        
        [Header("Key Events")]
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