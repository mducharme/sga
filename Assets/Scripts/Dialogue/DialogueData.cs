using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Dialogue", order = 0)]
    public class DialogueData : ScriptableObject
    {
        public string category;
        public string id;
        public List<Sentence> sentences = new();
    }
}