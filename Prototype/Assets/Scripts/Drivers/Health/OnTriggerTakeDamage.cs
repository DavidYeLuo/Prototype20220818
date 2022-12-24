using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Drivers.Health
{
    public class OnTriggerTakeDamage : MonoBehaviour
    {
        [SerializeField] private int damage;

        private void OnTriggerEnter(Collider other)
        {
            IHealth health = other.gameObject.GetComponent<IHealth>();
            if (health == null) return;
            
            health.TakeDamage(damage);
        }
    }
}