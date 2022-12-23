using System;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Abilities
{
    public class IreliaE : Ability
    {
        [Header("Config")]
        [SerializeField] private float groundOffset;
        
        [Space]
        [Header("Dependencies")]
        [SerializeField] private GameObject cursorInterface;
        [Header("Pillar")]
        [SerializeField] private GameObject ePillar;
        
        [Space]
        [Header("Debug")]
        [SerializeField] private int eCount;
        [SerializeField] private Vector3 startE;
        [SerializeField] private Vector3 endE;
        [SerializeField] private Vector3 cursorPosition;

        private LineRenderer lineRenderer;

        private ICursorPosition cursor;
        
        private void Awake()
        {
            lineRenderer = new GameObject("LineRenderer holder").AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;

            cursor = cursorInterface.GetComponent<ICursorPosition>();
        }

        public override void PerformAbility()
        {
            cursorPosition = cursor.GetCursorPosition();
            Vector3 offset = Vector3.up * groundOffset/2;
            Instantiate(ePillar, cursorPosition + offset, Quaternion.identity);

            if (eCount == 0)
            {
                startE = cursorPosition;
            }
            else if (eCount == 1)
            {
                endE = cursorPosition;
                
                // Draw line here from startE to endE
                lineRenderer.SetPosition(0, startE + 0.5f * offset);
                lineRenderer.SetPosition(1, endE + 0.5f * offset);
            }

            eCount++;
            eCount %= 2;
        }
    }
}