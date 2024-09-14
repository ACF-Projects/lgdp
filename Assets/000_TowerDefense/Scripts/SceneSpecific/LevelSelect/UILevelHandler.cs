using RushHour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RushHour
{
    public class UILevelHandler : MonoBehaviour
    {

        [Header("Level Properties")]
        public string LevelSceneName;

        /// <summary>
        /// Go to a specified scene, finding a TransitionManager
        /// singleton if one exists.
        /// </summary>
        public void GoToScene()
        {
            if (TransitionManager.Instance == null)
            {
                Debug.LogError("No TransitionManager object found!", this);
                return;
            }
            TransitionManager.Instance.GoToScene(LevelSceneName);
        }

    }
}
