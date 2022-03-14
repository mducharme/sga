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

        [SerializeField] private bool dash;
        [SerializeField] private bool isDashing;
        [SerializeField] private bool startedDashing;
        [SerializeField] private bool stoppedDashing;

        [SerializeField] private bool attack;
        [SerializeField] private bool isAttacking;
        [SerializeField] private bool startedAttacking;
        [SerializeField] private bool stoppedAttacking;

        [SerializeField] private bool shoot;
        [SerializeField] private bool isShooting;
        [SerializeField] private bool startedShooting;
        [SerializeField] private bool stoppedShooting;

        [SerializeField] private bool shield;
        [SerializeField] private bool isShielding;
        [SerializeField] private bool startedShielding;
        [SerializeField] private bool stoppedShielding;

        public float Horizontal { get => horizontal; private set { } }
        public float Vertical { get => vertical; private set { } }
        public bool IsMoving { get => isMoving; private set { } }

        public bool Interact { get => interact; private set { } }

        public bool Jump { get => jump; private set { } }
        public bool IsJumping { get => isJumping; private set { } }
        public bool StartedJumping { get => startedJumping; private set { } }
        public bool StoppedJumping { get => stoppedJumping; private set { } }

        public bool Dash { get => dash; private set { } }
        public bool IsDashing { get => isDashing; private set { } }
        public bool StartedDashing { get => startedDashing; private set { } }
        public bool StoppedDashing { get => stoppedDashing; private set { } }

        public bool Attack { get => attack; private set { } }
        public bool IsAttacking { get => isAttacking; private set { } }
        public bool StartedAttacking { get => startedAttacking; private set { } }
        public bool StoppedAttacking { get => stoppedAttacking; private set { } }

        public bool Shoot { get => shoot; private set { } }
        public bool IsShooting { get => isShooting; private set { } }
        public bool StartedShooting { get => startedShooting; private set { } }
        public bool StoppedShooting { get => stoppedShooting; private set { } }

        public bool Shield { get => shield; private set { } }
        public bool IsShielding { get => isShielding; private set { } }
        public bool StartedShielding { get => startedShielding; private set { } }
        public bool StoppedShielding { get => stoppedShielding; private set { } }

        public void HandleInput()
        {
            bool wasJumping = isJumping;
            bool wasDashing = isDashing;
            bool wasAttacking = isAttacking;
            bool wasShooting = isShooting;
            bool wasShielding = isShielding;

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            isMoving = (horizontal != 0f || vertical != 0f);

            interact = Input.GetButton("Attack");

            jump = Input.GetButtonDown("Jump");
            isJumping = Input.GetButton("Jump");
            startedJumping = !wasJumping && isJumping;
            stoppedJumping = wasJumping && !isJumping;

            dash = Input.GetAxis("Dash") > 0 || Input.GetButtonDown("Dash");
            isDashing = Input.GetAxis("Dash") > 0 || Input.GetButton("Dash");
            startedDashing = !wasDashing && isDashing;
            stoppedDashing = wasJumping && !isDashing;

            attack = Input.GetButtonDown("Attack");
            isAttacking = Input.GetButton("Attack");
            startedAttacking = !wasAttacking && isAttacking;
            stoppedAttacking = wasAttacking && !isAttacking;

            shoot = Input.GetButtonDown("Shoot");
            isShooting = Input.GetButton("Shoot");
            startedShooting = !wasShooting && isShooting;
            stoppedShooting = wasShooting && !isShooting;

            shield = Input.GetButtonDown("Shield");
            isShielding = Input.GetButton("Shield");
            startedShielding = !wasShielding && isShielding;
            stoppedShielding = wasShielding && !isShielding;
        }
    }
}