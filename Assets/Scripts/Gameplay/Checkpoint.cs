using UnityEngine;

namespace Gameplay
{
    public class Checkpoint : MonoBehaviour
    {
        public void ActivateCheckpoint()
        {
            FindObjectOfType<Game.SaveManager>().QuickSave();
        }

        #region Collision Handlers
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ActivateCheckpoint();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ActivateCheckpoint();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                ActivateCheckpoint();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                ActivateCheckpoint();
            }
        }
        #endregion
    }
}