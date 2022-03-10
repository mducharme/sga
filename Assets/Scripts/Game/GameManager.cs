using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}