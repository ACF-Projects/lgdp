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
        /// Go to a specified scene, finding a SceneSwitcher
        /// singleton if one exists.
        /// </summary>
        public void GoToScene()
        {
            if (SceneSwitcher.Instance == null)
            {
                Debug.LogError("No SceneSwitcher object found!", this);
                return;
            }
            SceneSwitcher.Instance.GoToScene(LevelSceneName);
        }

    }
}
