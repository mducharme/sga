using UnityEngine;

namespace Player
{
    public class GameLog : MonoBehaviour
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
    }
}