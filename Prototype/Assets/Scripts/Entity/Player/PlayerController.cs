using System;
using Entity;
using GameSystem;
using UnityEditor;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Unity Component that handles with users inputs. <br/>
    /// This class contains functions that serves as an intermediary from the player key inputs
    /// to the Entity class. <br/>
    /// </summary>
    /// TODO Make this more general: EntityController.cs instead
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        // RayCaster is tied to the user's mouse
        // TODO: Abstract this so that AI can use
        [SerializeField] private RayCaster rayCaster; 

        private IAbilityUser abilityUserDriver;
        private IMove movementDriver;
        private ICursorPosition cursorPositionDriver;

        private Vector3 cursorPosition;
        
        public void Start()
        {
            abilityUserDriver = player.GetComponent<IAbilityUser>();
            movementDriver = player.GetComponent<IMove>();
            cursorPositionDriver = player.GetComponent<ICursorPosition>();
            
            if(abilityUserDriver == null)
                Debug.Log("Warning abilityUserDriver is NULL");
            if(movementDriver == null)
                Debug.Log("Warning movementDriver is NULL");
            
            cursorPosition = player.transform.forward;
        }

        /// <summary>
        /// Prepare and call Entity's Move function.
        /// </summary>
        public void MovePlayerTowardMousePosition()
        {
            GetCursorPosition();
            cursorPositionDriver.SetCursorPosition(cursorPosition);
            movementDriver.Move(cursorPosition);
        }

        /// <summary>
        /// Prepapre and call Entity's UseAbility function.
        /// </summary>
        /// <remarks>First ability is 1</remarks>
        /// <param name="n">ability index</param>
        public void UsePlayerAbility(int n)
        {
            GetCursorPosition();
            cursorPositionDriver.SetCursorPosition(cursorPosition);
            abilityUserDriver.UseAbility(n-1);
        }

        /// <summary>
        /// Gets the user's mouse position.
        /// Some abilities requires the user's cursor position.<br/>
        /// </summary>
        /// TODO: Needs to be more clear. Maybe we should call it set instead?
        public void GetCursorPosition()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                cursorPosition = hit.point;
            }
        }
    }
}