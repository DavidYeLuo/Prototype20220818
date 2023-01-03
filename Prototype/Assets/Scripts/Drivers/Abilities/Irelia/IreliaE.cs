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
    /// Unity Component that is an ability implementation. <br/>
    /// Irelia's E spawns two pillars
    /// and then it connects through a straight line. <br/>
    /// Objects hit by the the line or pillar will be damaged and marked. <br/>
    /// When an object has the mark and the entity uses Q on that target,
    /// then the Q's cooldown is reset.
    /// </summary>
    /// <remarks>
    /// TODO: 1. Add Marks
    /// TODO: 2. Add Cooldown
    /// </remarks>
    public class IreliaE : Ability
    {
        [Header("Config")] 
        [SerializeField] private int damage;
        [SerializeField] private float groundOffset;
        [SerializeField] private float abilityDuration;

        [Space]
        [Header("Dependencies")]
        [SerializeField] private GameObject cursorInterface;
        [Header("E Pillar")]
        [SerializeField] private GameObject firstPillar;
        [SerializeField] private GameObject secondPillar;
        
        [Space]
        [Header("Debug")]
        [SerializeField] private int eCount;
        [SerializeField] private Vector3 startE;
        [SerializeField] private Vector3 endE;
        [SerializeField] private Vector3 cursorPosition;

        private LineRenderer lineRenderer;

        private ICursorPosition cursor;
        
        private Timer abilityHitboxTimer;
        private Coroutine timerCoroutine;
        
        // TODO: Use abstraction
        private OnTriggerTakeDmgAndDisableThisAfterSec firstDmgDriver;
        private OnTriggerTakeDmgAndDisableThisAfterSec secondDmgDriver;
        
        private void Awake()
        {
            lineRenderer = new GameObject("LineRenderer holder").AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;

            cursor = cursorInterface.GetComponent<ICursorPosition>();
            abilityHitboxTimer = new Timer();
            timerCoroutine = StartCoroutine(abilityHitboxTimer.SetTimer(0));
            
            // TODO: Use abstraction
            firstDmgDriver = firstPillar.GetComponent<OnTriggerTakeDmgAndDisableThisAfterSec>();
            secondDmgDriver = secondPillar.GetComponent<OnTriggerTakeDmgAndDisableThisAfterSec>();
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
        /// First Q will spawn a pillar. <br/>
        /// Second Q will spawn the second pillar.
        /// </summary>
        /// <remarks>
        /// When the second pillar is spawned, it will connect. <br/>
        /// Anything that is hit by this attack will take damage and become marked.
        /// </remarks>
        public override void PerformAbility()
        {
            cursorPosition = cursor.GetCursorPosition();
            Vector3 offset = Vector3.up * groundOffset/2;

            if (eCount == 0)
            {
                // Disable so that the pillar doesn't disappear
                DisablePillarFunctionality();
                // Place Pillar
                startE = cursorPosition + offset;
                firstPillar.transform.position = startE;
                firstPillar.SetActive(true);
            }
            else if (eCount == 1)
            {
                // Place Pillar
                endE = cursorPosition + offset;
                secondPillar.transform.position = endE;
                secondPillar.SetActive(true);
                
                // After the second pillar is placed, the first and second pillar will
                // disappear at the same time. (They are synced now)
                EnablePillarFunctionality();
                
                // Draw line here from startE to endE
                lineRenderer.SetPosition(0, startE);
                lineRenderer.SetPosition(1, endE);
                
                RaycastHit hit;
                Vector3 eDirection = endE - startE;
                float eDistance = Vector3.Distance(startE, endE);

                // Damage anything hit by the line
                if (Physics.Raycast(startE, eDirection, out hit, eDistance))
                {
                    IHealth health = hit.collider.GetComponent<IHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                }

                EnableLineRenderer();
                StartTimerAndDisableLineRendererAfter();
            }

            eCount++;
            eCount %= 2;
        }
        /// <summary>
        /// Make the line disappear
        /// </summary>
        /// <remarks>
        /// Normally, the Line produced stays forever,
        /// so we need to disable it after some time.
        /// </remarks>
        private void StartTimerAndDisableLineRendererAfter()
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(abilityHitboxTimer.SetTimer(abilityDuration));
        }
        
        /// <summary>
        /// Enable line renderer
        /// </summary>
        /// <remarks>
        /// Normally, the Line produced stays forever,
        /// so we need to disable it after some time.
        /// </remarks>
        private void EnableLineRenderer()
        {
            lineRenderer.enabled = true;
        }
        
        /// <summary>
        /// Disable line renderer
        /// </summary>
        /// <remarks>
        /// Normally, the Line produced stays forever,
        /// so we need to disable it after some time.
        /// </remarks>
        private void DisableLineRenderer()
        {
            lineRenderer.enabled = false;
        }

        /// <summary>
        /// Enables the pillar
        /// </summary>
        /// <remarks>
        /// The pillar is currently part of Irelia. <br/>
        /// We need to enable/disable
        /// or else the pillars will stay in the game forever
        /// </remarks>
        private void EnablePillarFunctionality()
        {
            firstDmgDriver.enabled = true;
            secondDmgDriver.enabled = true;
        }
        
        /// <summary>
        /// Disable the pillar
        /// </summary>
        /// <remarks>
        /// The pillar is currently part of Irelia. <br/>
        /// We need to enable/disable
        /// or else the pillars will stay in the game forever
        /// </remarks>
        private void DisablePillarFunctionality()
        {
            firstDmgDriver.enabled = false;
            secondDmgDriver.enabled = false;
        }
    }
}