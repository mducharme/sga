using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Controls))]
    [RequireComponent(typeof(Rigidbody))]
    public class TopDownMovement : MonoBehaviour
    {
        [Tooltip("Move speed in meters/second")]
        [SerializeField] private float moveSpeed = 10f;

        [SerializeField] private float jumpForce = 25f;
        [SerializeField] private int numJumps = 1;

        [SerializeField] protected float gravityVelocityThreshold = 1f;
        [SerializeField] protected float gravityModifier = 150f;

        private Controls controls;
        private Rigidbody body;
        private CapsuleCollider groundCollider;
        private bool isGrounded;
        private int currentJumpNum;

        public delegate void OnMove(Vector3 movement);
        public OnMove onMove;

        public delegate void OnJump(int jumpNum);
        public OnJump onJump;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            groundCollider = GetComponent<CapsuleCollider>();
            controls = GetComponent<Controls>();
        }

        private void Update()
        {
            controls.HandleInput();

            if (controls.Jump)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            if (controls.IsMoving)
            {
                Move();
            }

            CheckGrounded();

            if (body.velocity.y <= gravityVelocityThreshold)
            {
                body.AddForce(gravityModifier * body.mass * Vector3.down);
            }

            if (isGrounded)
            {
                currentJumpNum = 0;
            }
        }

        public void Move()
        {
            Vector3 movement = moveSpeed * Time.fixedDeltaTime * new Vector3(controls.Horizontal, 0f, controls.Vertical).normalized;
            body.rotation = Quaternion.LookRotation(movement);
            body.position += movement;
            onMove?.Invoke(movement);
        }

        public void Jump()
        {
            int numJumpsMax = numJumps;
            if (isGrounded == true || currentJumpNum < (numJumpsMax - 1))
            {
                currentJumpNum++;
                onJump?.Invoke(currentJumpNum);
                body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
                body.AddForce(jumpForce * body.mass * Vector3.up, ForceMode.Impulse);

            }
        }

        private void CheckGrounded()
        {
            isGrounded = false;
            float capsuleHeight = Mathf.Max(groundCollider.radius * 2f, groundCollider.height * 2f);
            Vector3 capsuleBottom = transform.TransformPoint(groundCollider.center - Vector3.up * capsuleHeight / 2f);
            float radius = transform.TransformVector(groundCollider.radius, 0f, 0f).magnitude;

            Ray ray = new(capsuleBottom + transform.up * 0.01f, -transform.up);
            if (Physics.Raycast(ray, out RaycastHit hit, radius * 5f))
            {
                float normalAngle = Vector3.Angle(hit.normal, transform.up);
                if (normalAngle < 45f)
                {
                    float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + 0.02f;
                    if (hit.distance < maxDist)
                    {
                        isGrounded = true;
                    }
                }
            }
        }
    }
}