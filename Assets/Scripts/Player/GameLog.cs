using UnityEngine;

namespace Player
{
    public class GameLog : MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private Log.MovementLog movementLog = new();

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

            public Log.MovementLog.SaveData movementLog;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.movementLog = (Log.MovementLog.SaveData)movementLog.PrepareSaveData();
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            movementLog.RestoreSaveData(saveData.movementLog);
        }
        #endregion
    }
}