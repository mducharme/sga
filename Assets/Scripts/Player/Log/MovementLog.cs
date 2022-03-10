using System;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class MovementLog: Game.ISaveable
    {
        [SerializeField] private float distanceTraveled;
        [SerializeField] private int numJumps;
        [SerializeField] private int numMultiJumps;

        public float DistanceTraveled { get => distanceTraveled; private set { } }
        public float NumJumps { get => numJumps; private set { } }
        public float NumMultiJumps { get => numMultiJumps; private set { } }

        public void LogMovement(Vector3 movement)
        {
            distanceTraveled += MathF.Abs(movement.magnitude);
        }

        public void LogJump(int numJump)
        {
            if (numJump > 1)
            {
                numMultiJumps++;

            }
            else
            {
                numJumps++;
            }
        }

        #region Save
        [System.Serializable]
        public struct SaveData
        {
            public float distanceTraveled;
            public int numJumps;
            public int numMultiJumps;
            public int numDash;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.distanceTraveled = distanceTraveled;
            saveData.numJumps = numJumps;
            saveData.numMultiJumps = numMultiJumps;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            distanceTraveled = saveData.distanceTraveled;
            numJumps = saveData.numJumps;
            numMultiJumps = saveData.numMultiJumps;
        }
        #endregion
    }
}