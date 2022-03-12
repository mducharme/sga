using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Dialogue", order = 0)]
    public class DialogueData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Dialogues/";

        public string category;
        public string id;
        public List<Sentence> sentences = new();

        static public DialogueData LoadFromResourceName(string name)
        {
            return (DialogueData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}