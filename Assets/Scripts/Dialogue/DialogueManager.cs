using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dialogue { 
    public class DialogueManager : MonoBehaviour
    {
        [Tooltip("The dialogue box must have a field \"Name\" and a field \"Text\"")]
        [SerializeField] private DialogueBox dialogueBox;

        private Queue<Sentence> sentences = new();

        private Sentence currentSentence;

        private IEnumerator dialogueCo;
        private bool goNext = false;

        public DialogueBox DialogueBox { get => dialogueBox; set { dialogueBox = value; } }

        private void Awake()
        {
            dialogueBox.onNext += OnDialogueClose;
        }

        private void OnDestroy()
        {
            dialogueBox.onNext -= OnDialogueClose;
        }

        private void OnDialogueClose()
        {
            if (sentences.Count > 0)
            {
                goNext = true;
            }
            else
            {
                CloseDialogue();
            }

        }

        public void PlayDialogue(DialogueData dialogue)
        {
            EnqueueDialogue(dialogue);

            StopAllCoroutines();
            dialogueCo = DialogueCo();
            StartCoroutine(dialogueCo);
        }

        private void EnqueueDialogue(DialogueData dialogue)
        {
            sentences.Clear();
            foreach (Sentence sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        private void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                CloseDialogue();
                return;
            }
            currentSentence = sentences.Dequeue();
            dialogueBox.gameObject.SetActive(true);
            dialogueBox.TypeSentence(currentSentence.owner, currentSentence.text);
        }

        public void CloseDialogue()
        {
            // @todo If dialogue is "unique", save read status in gamedata.
            dialogueBox.gameObject.SetActive(false);
        }

        IEnumerator DialogueCo()
        {
            DisplayNextSentence();
            goNext = false;
            yield return new WaitUntil(() => goNext);

            if (sentences.Count > 0)
            {
                dialogueCo = DialogueCo();
                StartCoroutine(dialogueCo);
            }
            else
            {
                CloseDialogue();
            }
        }
    }
}