using System;
using Drivers.Health;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Player.Abilities
{
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

        public override void PerformAbility()
        {
            cursorPosition = cursor.GetCursorPosition();
            Vector3 offset = Vector3.up * groundOffset/2;

            if (eCount == 0)
            {
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
                
                EnablePillarFunctionality();
                
                // Draw line here from startE to endE
                lineRenderer.SetPosition(0, startE);
                lineRenderer.SetPosition(1, endE);
                
                RaycastHit hit;
                Vector3 eDirection = endE - startE;
                float eDistance = Vector3.Distance(startE, endE);

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
        private void StartTimerAndDisableLineRendererAfter()
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(abilityHitboxTimer.SetTimer(abilityDuration));
        }
        private void EnableLineRenderer()
        {
            lineRenderer.enabled = true;
        }
        private void DisableLineRenderer()
        {
            lineRenderer.enabled = false;
        }

        private void EnablePillarFunctionality()
        {
            firstDmgDriver.enabled = true;
            secondDmgDriver.enabled = true;
        }
        private void DisablePillarFunctionality()
        {
            firstDmgDriver.enabled = false;
            secondDmgDriver.enabled = false;
        }
    }
}