using System;
using Entity;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Abilities
{
    public class IreliaW : Ability
    {
        [Header("Config")]
        [SerializeField] private GameObject currentEntity;
        [SerializeField] private float groundOffset;

        [Space] [Header("Dependencies")] [SerializeField]
        private GameObject cursorInterface;
        
        [Space]
        private LineRenderer lineRenderer;
        private ICursorPosition cursor;

        private void Awake()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            cursor = cursorInterface.GetComponent<ICursorPosition>();
            if(cursor == null) Debug.Log("Warning cursor is NULL.");
        }

        public override void PerformAbility()
        {
            lineRenderer.SetPosition(0, currentEntity.transform.position);
            var offset = cursor.GetCursorPosition() + Vector3.up * groundOffset;
            lineRenderer.SetPosition(1, offset);
        }
    }
}