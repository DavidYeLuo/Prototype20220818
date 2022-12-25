using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Drivers.Health
{
    public class OnTriggerTakeDmgAndDisableThisAfterSec : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private int damage;
        [SerializeField] private float duration;
        
        [Space]
        [Header("Objects to ignore")]
        [SerializeField] private List<Collider> ignoreCollider;
        
        private Dictionary<Collider, bool> map;
        
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

        private void OnEnable()
        {
            if(timerCoroutine != null)
                StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(DisableAfterSeconds(duration));
        }

        private void OnTriggerEnter(Collider other)
        {
            map.TryGetValue(other, out isInIgnoreList);
            if (isInIgnoreList == true) return;
            IHealth health = other.gameObject.GetComponent<IHealth>();
            if (health == null) return;
            
            health.TakeDamage(damage);
        }

        private IEnumerator DisableAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}