using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Controls))]
    [RequireComponent(typeof(GameLog))]
    [RequireComponent(typeof(TopDownMovement))]
    public class PlayerController : MonoBehaviour, Game.ISaveable
    {
        static public PlayerController instance;

        private GameLog gameLog;
        private Controls controls;

        private TopDownMovement topDownMovement;

        public TopDownMovement Movement { get => topDownMovement; private set { } }

        public delegate void OnInteract();
        public OnInteract onInteract;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);

            topDownMovement = GetComponent<TopDownMovement>();

            topDownMovement.onMove += OnMove;
            topDownMovement.onJump += OnJump;

            gameLog = GetComponent<GameLog>();
            controls = GetComponent<Controls>();

        }

        private void OnDestroy()
        {
            if (topDownMovement != null)
            {
                topDownMovement.onMove -= OnMove;
                topDownMovement.onJump -= OnJump;
            }
        }

        private void Update()
        {
            controls.HandleInput();

            if (controls.Jump)
            {
                Movement.Jump();
            }

            if (controls.Interact && onInteract != null)
            {
                onInteract.Invoke();
                return;
            }
        }

        private void FixedUpdate()
        {
            if (controls.IsMoving)
            {
                Movement.Move(new Vector3(controls.Horizontal, 0f, controls.Vertical).normalized);
            }
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

        [System.Serializable]
        public struct SaveData
        {
            public GameLog.SaveData gameLog;

            public float[] position;
            public float[] rotation;
        };
        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.gameLog = (GameLog.SaveData)gameLog.PrepareSaveData();

            saveData.position = new float[3];
            saveData.position[0] = transform.position.x;
            saveData.position[1] = transform.position.y;
            saveData.position[2] = transform.position.z;

            saveData.rotation = new float[4];
            saveData.rotation[0] = transform.rotation.w;
            saveData.rotation[1] = transform.rotation.x;
            saveData.rotation[2] = transform.rotation.y;
            saveData.rotation[3] = transform.rotation.z;

            return saveData;
        }
        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;

            gameLog.RestoreSaveData(saveData.gameLog);

            Vector3 pos = new(saveData.position[0], saveData.position[1], saveData.position[2]);
            Quaternion rot = new(saveData.rotation[0], saveData.rotation[1], saveData.rotation[2], saveData.rotation[3]);
            transform.SetPositionAndRotation(pos, rot);
        }
    }
}
