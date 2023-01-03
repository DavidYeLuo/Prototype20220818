using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Drivers.Health
{
    /// <summary>
    /// When objects touch, this object will damage it. <br/>
    /// This object will disappear after a given duration.
    /// </summary>
    /// <remarks>
    /// Attach on Unity Components that are trigger
    /// </remarks>
    /// TODO: We should rename it to deal damage
    /// Since this object is inflicting damage to other object
    public class OnTriggerTakeDmgAndDisableThisAfterSec : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private int damage;
        [SerializeField] private float duration;
        
        [Space]
        [Header("Objects to ignore")]
        [Tooltip("Contains list of object that will ignore.")]
        [SerializeField] private List<Collider> ignoreCollider;
        
        // Cache for faster lookup
        private Dictionary<Collider, bool> map;
        
        // Timer and Coroutine are cache to ensure
        // that there isn't multiple coroutines running.
        private Timer timer;
        private Coroutine timerCoroutine;

        [Header("Debug")] 
        [SerializeField] private bool isInIgnoreList;
        private void Start()
        {
            map = new Dictionary<Collider, bool>();
            foreach (var item in ignoreCollider)
            {
                map.Add(item, true);
            }

            timer = new Timer();
        }

        /// <summary>
        /// Disable after duration is up
        /// </summary>
        private void OnEnable()
        {
            if(timerCoroutine != null)
                StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(DisableAfterSeconds(duration));
        }

        /// <summary>
        /// Deal damage when object touches.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            map.TryGetValue(other, out isInIgnoreList);
            if (isInIgnoreList == true) return;
            IHealth health = other.gameObject.GetComponent<IHealth>();
            if (health == null) return;
            
            health.TakeDamage(damage);
        }

        /// <summary>
        /// Timer function that disable this game object after a duration
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private IEnumerator DisableAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}