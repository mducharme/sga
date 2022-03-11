using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Sentence
    {
        [TextArea(3, 8)]
        public string text;
        public string owner;
        public float duration = 2.5f;

        [Tooltip("If there are any choices, they will be displayed instead of the default \"continue\" option.")]
        public List<Choice> choices;

        [Tooltip("May be used by the UI to convey extra information about the current mood of the conversation.")]
        public Emotion emotion;

        public enum Emotion
        {
            Default,
            Happy,
            Angry,
            Sad,
            Surprised,
            Curious
        }
    }
}