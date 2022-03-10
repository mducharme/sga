using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Controls : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public bool IsMoving { get; private set; }

        public bool Jump { get; private set; }
        public bool IsJumping { get; private set; }
        public bool StartedJumping { get; private set; }
        public bool StoppedJumping { get; private set; }

        public void HandleInput()
        {
            bool wasJumping = IsJumping;

            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            IsMoving = (Horizontal != 0f || Vertical != 0f);

            Jump = Input.GetButtonDown("Jump");
            IsJumping = Input.GetButton("Jump");
            StartedJumping = !wasJumping && IsJumping;
            StoppedJumping = wasJumping && !IsJumping;
        }
    }
}