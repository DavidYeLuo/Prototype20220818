using UnityEngine;

namespace Drivers.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private int max;

        public int GetHealth()
        {
            return value;
        }

        public void SetHealth(int value)
        {
            _Check_And_Set_Health(value);
        }

        public void DecreaseHealth(int amount)
        {
            _Check_And_Set_Health(value-amount);
        }

        public void IncreaseHealth(int amount)
        {
            _Check_And_Set_Health(value+amount);
        }

        private void _Check_And_Set_Health(int amount)
        {
            value = amount;
            if (value < 0) value = 0;
            if (value > max) value = max;
        }
    }
}