using UnityEngine;

namespace Player
{
    public class GameLog : MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private Log.MovementLog movementLog = new();
        [SerializeField] private Log.CombatLog combatLog = new();

        public void LogMovement(Vector3 movement)
        {
            movementLog.LogMovement(movement);
        }

        public void LogJump(int jumpNum)
        {
            movementLog.LogJump(jumpNum);
        }

        #region Save
        [System.Serializable]
        public struct SaveData
        {
            public int totalXp;
            public int totalCollectibles;

            public Log.CombatLog.SaveData combatLog;
            public Log.MovementLog.SaveData movementLog;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.combatLog = (Log.CombatLog.SaveData)combatLog.PrepareSaveData();
            saveData.movementLog = (Log.MovementLog.SaveData)movementLog.PrepareSaveData();
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            combatLog.RestoreSaveData(saveData.combatLog);
            movementLog.RestoreSaveData(saveData.movementLog);
        }
        #endregion
    }
}