using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drivers.Health
{
    /// <summary>
    /// When objects touch, this object will damage it.
    /// Then immediately, this object will be disabled
    /// </summary>
    /// <remarks>
    /// Attach on Unity Components that are trigger
    /// </remarks>
    /// TODO: We should rename it to deal damage
    /// Since this object is inflicting damage to other object
    public class OnTriggerTakeDmgAndDisableThis : MonoBehaviour
    {
        [SerializeField] private int damage;
        [Tooltip("Contains list of object that will ignore.")]
        [SerializeField] private List<Collider> ignoreCollider;
        private Dictionary<Collider, bool> map;

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

        /// <summary>
        /// Damage the other object when touch happen
        /// and then disable this game object.
        /// </summary>
        /// <param name="other"></param>
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