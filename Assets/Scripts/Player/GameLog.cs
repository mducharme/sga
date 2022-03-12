using UnityEngine;

namespace Player
{
    public class GameLog : MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private Log.MovementLog movementLog = new();
        [SerializeField] private Log.CombatLog combatLog = new();
        [SerializeField] private Log.MeleeWeaponsLog meleeWeaponsLog = new();
        [SerializeField] private Log.RangedWeaponsLog rangedWeaponsLog = new();

        public Log.MovementLog Movement { get => movementLog; private set { } }
        public Log.CombatLog Combat { get => combatLog; private set { } }
        public Log.MeleeWeaponsLog MeleeWeapons { get => meleeWeaponsLog; private set { } }
        public Log.RangedWeaponsLog RangedWeapons { get => rangedWeaponsLog; private set { } }

        public void LogMovement(Vector3 movement)
        {
            movementLog.LogMovement(movement);
        }

        public void LogJump(int jumpNum)
        {
            movementLog.LogJump(jumpNum);
        }

        public void LogMeleeAttack(Combat.Weapon.MeleeData melee)
        {
            meleeWeaponsLog.LogAttack(melee);
        }

        public void LogRangedShoot(Combat.Weapon.RangedData ranged)
        {
            rangedWeaponsLog.LogShoot(ranged);
        }

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
    }
}