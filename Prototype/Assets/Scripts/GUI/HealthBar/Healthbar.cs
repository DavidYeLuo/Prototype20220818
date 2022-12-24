using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.HealthBar
{
    public class Healthbar : MonoBehaviour
    {
        [Header("Dependencies")]
        [Header("GUI")]
        [Tooltip("Insert current health image")]
        [SerializeField] private Image image;
        [Header("ScriptableObjects")]
        [SerializeField] private IntReference currentHealth;
        [SerializeField] private IntReference maxHealth;

        private void Start()
        {
            UpdateGUI();
        }

        private void OnEnable()
        {
            currentHealth.subscribers += UpdateGUI;
            maxHealth.subscribers += UpdateGUI;
        }
        private void OnDisable()
        {
            currentHealth.subscribers -= UpdateGUI;
            maxHealth.subscribers -= UpdateGUI;
        }

        public void UpdateGUI()
        {
            image.fillAmount = (float) currentHealth.Get() / maxHealth.Get();
        }
    }
}