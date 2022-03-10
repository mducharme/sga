using System;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class MovementLog
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
    }
}