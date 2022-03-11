using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    abstract public class BaseTrigger : MonoBehaviour
    {
        public enum TriggerMode
        {
            Auto,       // Start interaction automatically
            Interact    // Ex: "Press [X] to interact".
        }

        [SerializeField] private TriggerMode triggerMode;

        [SerializeField] private string triggerTag = "Player";

        [SerializeField] private float alertRadius;
        private bool playerIsInAlertRange;

        public delegate void OnTrigger();
        public OnTrigger onTrigger;

        public delegate void OnEnterAlertRange();
        public OnEnterAlertRange onEnterAlertRange;

        public delegate void OnExitAlertRange();
        public OnExitAlertRange onExitAlertRange;

        public void Trigger()
        {
            onTrigger?.Invoke();

            StopInteract();
            ExitAlertRange();
        }

        public void StartInteract()
        {
            // Press x to activate
            // onClick = Trigger()
            Player.PlayerController.instance.onInteract += Trigger;
        }

        public void StopInteract()
        {
            Player.PlayerController.instance.onInteract -= Trigger;
        }

        public void EnterAlertRange()
        {
            onEnterAlertRange?.Invoke();
        }

        public void ExitAlertRange()
        {
            onExitAlertRange?.Invoke();
        }

        private void EnterTrigger()
        {
            switch (triggerMode)
            {
                case TriggerMode.Auto:
                    Trigger();
                    break;
                case TriggerMode.Interact:
                    StartInteract();
                    break;
            }
        }

        private void ExitTrigger()
        {
            switch (triggerMode)
            {
                case TriggerMode.Auto:
                    break;
                case TriggerMode.Interact:
                    StopInteract();
                    break;
            }
        }

        private void Update()
        {
            Vector3 playerPos = Player.PlayerController.instance.transform.position;
            float distance = Vector3.Distance(playerPos, transform.position);
            if (playerIsInAlertRange == false && distance < alertRadius)
            {
                EnterAlertRange();
                playerIsInAlertRange = true;
            }
            else if (playerIsInAlertRange == true && distance > alertRadius)
            {
                ExitAlertRange();
                playerIsInAlertRange = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                EnterTrigger();
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                EnterTrigger();
            }
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                ExitTrigger();
            }
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                ExitTrigger();
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag(triggerTag))
            {
                EnterTrigger();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(triggerTag))
            {
                EnterTrigger();
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag(triggerTag))
            {
                ExitTrigger();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(triggerTag))
            {
                ExitTrigger();
            }
        }
    }
}