using System.Collections;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : Gameplay.BaseTrigger
    {
        [SerializeField] private DialogueData dialogue;

        private void Awake()
        {
            onTrigger += OpenDialogue;
        }

        private void OnDestroy()
        {
            onTrigger -= OpenDialogue;
        }

        public void OpenDialogue()
        {
            FindObjectOfType<DialogueManager>().PlayDialogue(dialogue);
        }

    }
}