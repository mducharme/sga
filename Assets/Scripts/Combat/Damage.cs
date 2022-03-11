namespace Combat
{
    [System.Serializable]
    public class Damage : Game.ISaveable
    {
        public DamageCategory type;
        public int amount;

        public Damage(DamageCategory type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }

        [System.Serializable]
        public struct SaveData
        {
            public string type;
            public int amount;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.type = type.name;
            saveData.amount = amount;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            type = (DamageCategory)System.Enum.Parse(typeof(DamageCategory), saveData.type);
            amount = saveData.amount;
        }


    }
}