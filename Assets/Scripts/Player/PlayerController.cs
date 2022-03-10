using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(TopDownMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameLog gameLog;

        private TopDownMovement topDownMovement;

        private void Awake()
        {
            topDownMovement = GetComponent<TopDownMovement>();

            topDownMovement.onMove += OnMove;
            topDownMovement.onJump += OnJump;
        }

        private void OnDestroy()
        {
            topDownMovement.onMove -= OnMove;
            topDownMovement.onJump -= OnJump;
        }

        /**
         * When the player is moving.
         */
        private void OnMove(Vector3 movement)
        {
            gameLog.LogMovement(movement);
        }

        /**
         * When the player is jumping.
         */
        private void OnJump(int jumpNum)
        {
            gameLog.LogJump(jumpNum);
        }
    }
}
