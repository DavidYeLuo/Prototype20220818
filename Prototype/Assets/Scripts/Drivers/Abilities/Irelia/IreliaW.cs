using System;
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

        [Tooltip("Duration of the hitbox")]
        [SerializeField] private float abilityDuration;

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
            lineRenderer.SetPosition(0, startHitbox.transform.position);
            var offset = cursor.GetCursorPosition() + Vector3.up * groundOffset;
            lineRenderer.SetPosition(1, offset);
            StartTimerAndDisableLineRendererAfter();
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