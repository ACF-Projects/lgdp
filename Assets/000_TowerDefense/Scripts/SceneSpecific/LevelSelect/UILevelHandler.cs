using NaughtyAttributes;
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
        public string LevelName;
        public string SceneNameToLoad;
        [Header("Locked Properties")]
        public bool IsLocked = true;
        [ShowIf("IsLocked")]
        public string LevelPassword;

        public void OnClick_TryStartLevel()
        {
            // If this level is locked, prompt for password
            if (IsLocked)
            {
                LockedModal lm = FindObjectOfType<LockedModal>();
                if (lm != null)
                {
                    lm.Init(this);  // Initializes the locked modal and show it
                    lm.Show();
                } 
                else
                {
                    Debug.LogError("Could not find locked modal in scene!", this);
                }
            }
            else
            {
                GoToScene();  // Or else, just load the scene
            }
        }

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
            TransitionManager.Instance.GoToScene(SceneNameToLoad);
        }

    }
}
