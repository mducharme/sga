using UnityEngine;

namespace Player
{
    public class GameLog : MonoBehaviour, Game.ISaveable
    {

        [SerializeField] private int totalXp;
        [SerializeField] private Log.MovementLog movementLog = new();
        [SerializeField] private Log.CombatLog combatLog = new();
        [SerializeField] private Log.MeleeWeaponsLog meleeWeaponsLog = new();
        [SerializeField] private Log.RangedWeaponsLog rangedWeaponsLog = new();
        [SerializeField] private Log.EnemiesLog enemiesLog = new();

        public Log.MovementLog Movement { get => movementLog; private set { } }
        public Log.CombatLog Combat { get => combatLog; private set { } }
        public Log.MeleeWeaponsLog MeleeWeapons { get => meleeWeaponsLog; private set { } }
        public Log.RangedWeaponsLog RangedWeapons { get => rangedWeaponsLog; private set { } }
        public Log.EnemiesLog Enemies { get => enemiesLog; private set { } }

        public void LogXp(int amount)
        {
            totalXp += amount;
        }

        public void LogMovement(Vector3 movement)
        {
            movementLog.LogMovement(movement);
        }

        public void LogJump(int jumpNum)
        {
            movementLog.LogJump(jumpNum);
        }

        public void LogDash()
        {
            movementLog.LogDash();
        }

        public void LogMeleeAttack(Combat.Weapon.MeleeData melee)
        {
            meleeWeaponsLog.LogAttack(melee);
        }

        public void LogRangedShoot(Combat.Weapon.RangedData ranged)
        {
            rangedWeaponsLog.LogShoot(ranged);
        }

        public void LogEnemyHit(Combat.Result result, Enemy.EnemyData enemy)
        {
            enemiesLog.LogEnemyHit(enemy, result.GetTotalDamage());
            combatLog.LogHit(result);
        }

        public void LogHitByEnemy(Combat.Result result, Enemy.EnemyData enemy)
        {
            int dmg = result.GetTotalDamage();
            enemiesLog.LogHitByEnemy(enemy, dmg);
            combatLog.LogHitReceived(result);
        }

        public void LogHit(Combat.Result result)
        {
            combatLog.LogHitReceived(result);
        }

        public void LogEnemyKilled(Enemy.EnemyData enemy)
        {
            LogXp(enemy.xp);

            enemiesLog.LogEnemyKilled(enemy);
            combatLog.LogEnemyKill();
            //roomsLog.LogEnemyKill();
        }

        public void LogKilledByEnemy(Enemy.EnemyData enemy)
        {
            enemiesLog.LogKilledByEnemy(enemy);
            LogDeath();
        }

        public void LogDeath()
        {
            combatLog.LogDeath();
            //roomsLog.LogDeath();
        }

        [System.Serializable]
        public struct SaveData
        {
            public int totalXp;
            public int totalCollectibles;

            public Log.CombatLog.SaveData combatLog;
            public Log.MovementLog.SaveData movementLog;
            public Log.MeleeWeaponsLog.SaveData meleeWeaponsLog;
            public Log.RangedWeaponsLog.SaveData rangedWeaponsLog;
            public Log.EnemiesLog.SaveData enemiesLog;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.totalXp = totalXp;
            saveData.combatLog = (Log.CombatLog.SaveData)combatLog.PrepareSaveData();
            saveData.movementLog = (Log.MovementLog.SaveData)movementLog.PrepareSaveData();
            saveData.meleeWeaponsLog = (Log.MeleeWeaponsLog.SaveData)meleeWeaponsLog.PrepareSaveData();
            saveData.rangedWeaponsLog = (Log.RangedWeaponsLog.SaveData)rangedWeaponsLog.PrepareSaveData();
            saveData.enemiesLog = (Log.EnemiesLog.SaveData)enemiesLog.PrepareSaveData();
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            totalXp = saveData.totalXp;
            combatLog.RestoreSaveData(saveData.combatLog);
            movementLog.RestoreSaveData(saveData.movementLog);
            meleeWeaponsLog.RestoreSaveData(saveData.meleeWeaponsLog);
            rangedWeaponsLog.RestoreSaveData(saveData.rangedWeaponsLog);
            enemiesLog.RestoreSaveData(saveData.enemiesLog);
        }
    }
}