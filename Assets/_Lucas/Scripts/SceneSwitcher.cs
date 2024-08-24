using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LGDP.TowerDefense.Lucas.POC
{
    public class SceneSwitcher : MonoBehaviour
    {

        public static SceneSwitcher Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            Instance = this;
        }

        public void GoToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
       
    }
}
