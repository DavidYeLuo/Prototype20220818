using System;
using DefaultNamespace;
using UnityEngine;

namespace Player.Abilities
{
    public class IreliaW : Ability
    {
        [SerializeField] private float groundOffset;
        
        [Space]
        [SerializeField] private RayCaster rayCaster;
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        public override void PerformAbility()
        {
            if(rayCaster.MouseOverObject(out var hit))
            {
                lineRenderer.SetPosition(0, player.transform.position);
                var offset = hit.point + Vector3.up * groundOffset;
                lineRenderer.SetPosition(1, offset);
            }
        }
    }
}