using UnityEngine;

namespace Combat
{
    [System.Serializable]
    public class Health : Game.ISaveable
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        public int MaxHealth { get => maxHealth; private set { } }
        public int CurrentHealth { get => currentHealth; private set { } }

        public delegate void OnChange(int delta, int value);
        public OnChange onChange;

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void Heal(int amount)
        {
            int prevHealth = currentHealth;
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            onChange?.Invoke(currentHealth - prevHealth, currentHealth);
        }

        public void HealAll()
        {
            int prevHealth = currentHealth;
            currentHealth = maxHealth;

            onChange?.Invoke(currentHealth - prevHealth, currentHealth);
        }

        public void Damage(int amount)
        {
            int prevHealth = currentHealth;
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            onChange?.Invoke(currentHealth - prevHealth, currentHealth);
        }

        public void IncreaseMaxHealth(int amount)
        {
            maxHealth += amount;
            HealAll();
        }

        public bool IsAlive()
        {
            return currentHealth > 0;
        }

        [System.Serializable]
        public struct SaveData
        {
            public int maxHealth;
            public int currentHealth;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.maxHealth = maxHealth;
            saveData.currentHealth = currentHealth;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            maxHealth = saveData.maxHealth;
            currentHealth = saveData.currentHealth;
        }
    }
}