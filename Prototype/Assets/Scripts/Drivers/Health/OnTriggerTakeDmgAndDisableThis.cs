using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drivers.Health
{
    public class OnTriggerTakeDmgAndDisableThis : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private List<Collider> ignoreCollider;
        private Dictionary<Collider, bool> map;

        [FormerlySerializedAs("hasCollided")]
        [Header("Debug")] 
        [SerializeField] private bool isInIgnoreList;
        private void Start()
        {
            map = new Dictionary<Collider, bool>();
            foreach (var item in ignoreCollider)
            {
                map.Add(item, true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            map.TryGetValue(other, out isInIgnoreList);
            if (isInIgnoreList == true) return;
            IHealth health = other.gameObject.GetComponent<IHealth>();
            if (health == null) return;
            
            health.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}