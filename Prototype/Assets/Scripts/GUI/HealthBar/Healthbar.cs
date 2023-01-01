using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.HealthBar
{
    /// <summary>
    /// Unity Component that modifies the Image component. <br/>
    /// It listens to the entity's currentHealth event. <br/>
    /// Because currentHealth is IntReference, it broadcasts health change events. <br/>
    /// This allows this program to listen to the moment when currentHealth change
    /// and display accordingly. <br/>
    /// </summary>
    public class Healthbar : MonoBehaviour
    {
        [Header("Dependencies")]
        [Header("GUI")]
        [Tooltip("Insert current health image")]
        // Unity Image contains an attribute called fillAmount that the script
        // will use to display the entity health.
        [SerializeField] private Image image;
        
        // These are the data that the script will use to calculate
        // the appropriate fillAmount
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

        /// <summary>
        /// Calculates the appropriate the image fillAmount. <br/>
        /// </summary>
        /// <formula>
        /// fillAmount = currentHealth / maxHealth
        /// </formula>
        public void UpdateGUI()
        {
            image.fillAmount = (float) currentHealth.Get() / maxHealth.Get();
        }
    }
}