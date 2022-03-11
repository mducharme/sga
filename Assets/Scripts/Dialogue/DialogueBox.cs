using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField] private float typeDelay = 0.015f;

        [SerializeField] private TextMeshProUGUI displayText;
        [SerializeField] private TextMeshProUGUI ownerName;

        private IEnumerator typingCo;

        private bool isTyping;

        private string targetText;

        public delegate void OnNext();
        public OnNext onNext;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
            if (Input.anyKeyDown)
            {
                if (isTyping)
                {
                    FinishSentence();
                }
                else 
                {
                    onNext?.Invoke();
                }
            }
        }

        public void TypeSentence(string owner, string sentence)
        {
            targetText = sentence;

            displayText.text = "";
            ownerName.text = owner;

            typingCo = TypeSentenceCo();
            StartCoroutine(typingCo);
        }

        IEnumerator TypeSentenceCo()
        {
            isTyping = true;
            foreach (char letter in targetText.ToCharArray())
            {
                displayText.text += letter;
                yield return new WaitForSeconds(typeDelay);
            }
            FinishSentence();
        }

        void FinishSentence()
        {
            StopCoroutine(typingCo);
            displayText.text = targetText;
            isTyping = false;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}