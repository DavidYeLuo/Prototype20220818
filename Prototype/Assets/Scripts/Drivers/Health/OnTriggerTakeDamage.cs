using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Drivers.Health
{
    /// <summary>
    /// When objects touch, this object will damage it.
    /// </summary>
    /// <remarks>
    /// Attach on Unity Components that are trigger
    /// </remarks>
    /// <todo>
    /// TODO: We should rename it to deal damage
    /// Since this object is inflicting damage to other object
    /// </todo>
    public class OnTriggerTakeDamage : MonoBehaviour
    {
        [SerializeField] private int damage;

        /// <summary>
        /// When object touches, this will deal damage to the other object.
        /// </summary>
        /// <remarks>
        /// This will not continuously deal damage. <br/>
        /// Deals damage only when they touch.
        /// </remarks>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            IHealth health = other.gameObject.GetComponent<IHealth>();
            if (health == null) return;
            
            health.TakeDamage(damage);
        }
    }
}