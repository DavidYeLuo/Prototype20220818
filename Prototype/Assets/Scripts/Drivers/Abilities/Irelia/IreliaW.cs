using System;
using Drivers.Health;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Player.Abilities
{
    /// <summary>
    /// Unity Component that is an ability.
    /// Throws a triangle shaped attack in the direction of the cursor. <br/>
    /// The pointy part faces toward the cursor. <br/>
    /// </summary>
    /// <TODO>
    /// The purpose of this ability is either to soften enemies so that
    /// the enemies are low enough for the Q ability to kill for reset. <br/>
    /// The second purpose which isn't implemented yet is damage reduction: <br/>
    /// Pressing W should start launching the attack for more damage while also
    /// reduce damage from incoming attacks. <br/>
    /// Then releasing the W will launch the triangle attack. Depending on how long
    /// the player charged for.
    /// </TODO>
    public class IreliaW : Ability
    {
        [Header("Config")] 
        [SerializeField] private float groundOffset;

        [Header("Hitbox")]
        [SerializeField] private GameObject hitbox;
        [Tooltip("Changes the hitbox's orientation to face where the player clicked.")]
        [SerializeField] private GameObject hitboxRotation;

        [Tooltip("Duration of the hitbox")]
        [SerializeField] private float abilityDuration;

        [FormerlySerializedAs("orientationToMatch")]
        [Space] [Header("Dependencies")] 
        [SerializeField] private GameObject startHitbox;
        [SerializeField] private GameObject cursorInterface;
        
        [Space]
        private LineRenderer lineRenderer;
        private ICursorPosition cursor;
        
        // Hitbox Duration
        private Timer abilityHitboxTimer;
        private Coroutine timerCoroutine;

        private void Awake()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            cursor = cursorInterface.GetComponent<ICursorPosition>();
            if(cursor == null) Debug.Log("Warning cursor is NULL.");
            
            abilityHitboxTimer = new Timer();
            // Line below used to avoid needing an if statement in the StartTimer...After()
            timerCoroutine = StartCoroutine(abilityHitboxTimer.SetTimer(0));
        }

        private void OnEnable()
        {
            abilityHitboxTimer.timeUpEvent += DisableLineRenderer;
        }

        private void OnDisable()
        {
            abilityHitboxTimer.timeUpEvent -= DisableLineRenderer;
        }

        /// <summary>
        /// Creates a triangle where the player is in the middle of the base. <br/>
        /// The center of the triangle faces the cursor. <br/>
        /// There are two stages: <br/>
        /// First stage happens when the player press and hold W.
        /// During this stage, Irelia take reduced damage and increase W damage. <br/>
        /// Then the second stage is when the player release the key
        /// which launches the triangle attack.
        /// </summary>
        /// <remarks>
        /// The current implementation is enable the hitbox of the triangle.
        /// TODO: Add first stage
        /// </remarks>
        public override void PerformAbility()
        {
            EnableLineRenderer();
            Vector3 startPosition = startHitbox.transform.position;
            Vector3 endPosition = cursor.GetCursorPosition() + Vector3.up * groundOffset;
            RaycastHit hit;

            // Might not be needed since it is covered by the hitbox
            StartTimerAndDisableLineRendererAfter();

            hitboxRotation.transform.forward = endPosition - startPosition; // Sets the hitbox rotation to face the cursor
            hitbox.SetActive(true);
        }

        private void EnableLineRenderer()
        {
            lineRenderer.enabled = true;
        }
        private void DisableLineRenderer()
        {
            lineRenderer.enabled = false;
        }

        private void StartTimerAndDisableLineRendererAfter()
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(abilityHitboxTimer.SetTimer(abilityDuration));
        }
    }
}