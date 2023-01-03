using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drivers.Health
{
    /// <summary>
    /// Unity Component that represents health.
    /// It can gain or lose health.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Tooltip("Current Health")]
        [SerializeField] private int value;
        [Tooltip("Max Health or Health Capacity")]
        [SerializeField] private int max;

        [Header("Optional")] 
        [SerializeField] private IntReference healthValueReference;
        [SerializeField] private IntReference maxHealthValueReference;

        private void Start()
        {
            // Updates the reference
            if (healthValueReference == null) return;
            healthValueReference.Set(value);
            if (maxHealthValueReference == null) return;
            maxHealthValueReference.Set(value);
        }

        /// <summary>
        /// Accessor for the current health value.
        /// </summary>
        /// <returns>This object's current health value</returns>
        public int GetHealth()
        {
            return value;
        }

        /// <summary>
        /// Sets the current health value directly.
        /// </summary>
        /// <remarks>Health is locked between 0 to max health.</remarks>
        /// <param name="value">desired value for current health</param>
        public void SetHealth(int value)
        {
            _Check_And_Set_Health(value);
        }

        /// <summary>
        /// Lowers this object's health by an amount.
        /// </summary>
        /// <remarks>The lowest value for health is 0</remarks>
        /// <param name="amount">desired amount</param>
        public void DecreaseHealth(int amount)
        {
            _Check_And_Set_Health(value-amount);
        }

        /// <summary>
        /// This object gains the desired amount of health.
        /// </summary>
        /// <param name="amount">desired amount</param>
        /// <remarks>Current health can't reach higher than max health</remarks>
        public void IncreaseHealth(int amount)
        {
            _Check_And_Set_Health(value+amount);
        }

        /// <summary>
        /// Helper function that set bounds for what health value can be. <br/>
        /// Bounds: 0 <= current health <= max health
        /// </summary>
        /// <param name="amount"></param>
        private void _Check_And_Set_Health(int amount)
        {
            value = amount;
            if (value < 0) value = 0;
            if (value > max) value = max;
            
            if (healthValueReference == null) return;
            healthValueReference.Set(value);
        }
    }
}