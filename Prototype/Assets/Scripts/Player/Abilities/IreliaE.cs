using System;
using DefaultNamespace;
using UnityEngine;

namespace Player.Abilities
{
    public class IreliaE : Ability
    {
        [SerializeField] private float groundOffset;
        
        [Space]
        [SerializeField] private RayCaster rayCaster;
        [SerializeField] private GameObject ePillar;
        
        [Header("Debug")]
        [SerializeField] private int eCount;
        [SerializeField] private Vector3 startE;
        [SerializeField] private Vector3 endE;

        private LineRenderer lineRenderer;
        
        private void Awake()
        {
            lineRenderer = new GameObject("LineRenderer holder").AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        public override void PerformAbility()
        {
            if (rayCaster.MouseOverObject(out var hit))
            {
                Vector3 offset = Vector3.up * groundOffset/2;
                Instantiate(ePillar, hit.point + offset, Quaternion.identity);

                if (eCount == 0)
                {
                    startE = hit.point;
                }
                else if (eCount == 1)
                {
                    endE = hit.point;
                    
                    // Draw line here from startE to endE
                    lineRenderer.SetPosition(0, startE + 0.5f * offset);
                    lineRenderer.SetPosition(1, endE + 0.5f * offset);
                }

                eCount++;
                eCount %= 2;
            }
        }
    }
}