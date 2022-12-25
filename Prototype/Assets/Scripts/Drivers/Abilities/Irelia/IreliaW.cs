using System;
using Drivers.Health;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Player.Abilities
{
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

        public override void PerformAbility()
        {
            EnableLineRenderer();
            Vector3 startPosition = startHitbox.transform.position;
            // Note: endPosition isn't where the user click, it is offset by groundOffset
            Vector3 endPosition = cursor.GetCursorPosition() + Vector3.up * groundOffset;
            RaycastHit hit;
            
            // Display
            // lineRenderer.SetPosition(0, startPosition);
            // lineRenderer.SetPosition(1, endPosition);

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