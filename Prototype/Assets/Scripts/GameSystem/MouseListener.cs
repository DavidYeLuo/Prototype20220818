using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    /// <summary>
    /// Unity Component that maps a specific mouse keys to a script function.
    /// </summary>
    public class MouseListener : MonoBehaviour
    {
        [Header("Key Options")]
        [SerializeField] private MouseClick mouseOptions;
        
        [Header("Key Events")]
        [SerializeField] private UnityEvent keyDown;
        [SerializeField] private UnityEvent keyUp;
        [SerializeField] private UnityEvent keyHold;

        private int mouseKey;

        private void Awake()
        {
            switch (mouseOptions)
            {
                case MouseClick.LeftClick:
                    mouseKey = 0;
                    break;
                case MouseClick.RightClick:
                    mouseKey = 1;
                    break;
                default:
                    throw new Exception("Error: MouseClick isn't supported yet.");
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(mouseKey))
            {
                keyDown.Invoke();
            }
            else if (Input.GetMouseButtonUp(mouseKey))
            {
                keyUp.Invoke();
            }
            else if (Input.GetMouseButton(mouseKey))
            {
                keyHold.Invoke();
            }
        }

        private enum MouseClick
        {
            LeftClick,
            RightClick
        }
    }
}