using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Controls : MonoBehaviour
    {
        [SerializeField] private float horizontal;
        [SerializeField] private float vertical;
        [SerializeField] private bool isMoving;

        [SerializeField] private bool interact;

        [SerializeField] private bool jump;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool startedJumping;
        [SerializeField] private bool stoppedJumping;

        public float Horizontal { get => horizontal; private set { } }
        public float Vertical { get => vertical; private set { } }
        public bool IsMoving { get => isMoving; private set { } }

        public bool Interact { get => interact; private set { } }

        public bool Jump { get => jump; private set { } }
        public bool IsJumping { get => isJumping; private set { } }
        public bool StartedJumping { get => startedJumping; private set { } }
        public bool StoppedJumping { get => stoppedJumping; private set { } }

        public void HandleInput()
        {
            bool wasJumping = isJumping;

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            isMoving = (horizontal != 0f || vertical != 0f);

            interact = Input.GetButton("Fire1");

            jump = Input.GetButtonDown("Jump");
            isJumping = Input.GetButton("Jump");
            startedJumping = !wasJumping && isJumping;
            stoppedJumping = wasJumping && !isJumping;
        }
    }
}