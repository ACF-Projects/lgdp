using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lucas
{
    public class TutorialManager : MonoBehaviour
    {

        public static bool HasPlayedTutorialBefore = false;

        private void Start()
        {
            // If we haven't played the tutorial before, play it and set flag to true
            if (!HasPlayedTutorialBefore)
            {
                HasPlayedTutorialBefore = true;
                PlayTutorial();
            }
        }

        private void PlayTutorial()
        {

        }

    }
}