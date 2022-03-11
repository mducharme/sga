using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(GameLog))]
    [RequireComponent(typeof(TopDownMovement))]
    public class PlayerController : MonoBehaviour, Game.ISaveable
    {
        static public PlayerController instance;

        private GameLog gameLog;

        private TopDownMovement topDownMovement;

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

        }

        private void OnDestroy()
        {
            if (topDownMovement != null)
            {
                topDownMovement.onMove -= OnMove;
                topDownMovement.onJump -= OnJump;
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

        #region Save
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
        #endregion
    }
}
