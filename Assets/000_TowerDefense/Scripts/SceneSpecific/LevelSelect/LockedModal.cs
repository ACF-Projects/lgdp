using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RushHour
{
    public class LockedModal : MonoBehaviour
    {

        [Header("Object Assignments")]
        [SerializeField] private GameObject _modalObject;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TMP_InputField _passwordInput;

        private UILevelHandler _storedLevel;

        public void Show() => _modalObject.SetActive(true);
        public void Hide() => _modalObject.SetActive(false);

        private void Awake()
        {
            _passwordInput.onValueChanged.AddListener((string text) =>
            {
                if (text.Length == 4)
                {
                    OnSubmit_SubmitPassword();
                }
            });
        }

        /// <summary>
        /// Given a UILevelHandler present on a level object, initializes
        /// the locked modal with the proper name / password.
        /// </summary>
        public void Init(UILevelHandler level)
        {
            _levelText.text = level.LevelName;
            _storedLevel = level;
        }

        /// <summary>
        /// Submits the password stored in the input field.
        /// </summary>
        public void OnSubmit_SubmitPassword()
        {
            string password = _passwordInput.text;
            // If we type a wrong password, reject and reset input
            if (password != _storedLevel.LevelPassword)
            {
                Debug.Log("Wrong password!");
                _passwordInput.text = "";
                return;
            }
            Debug.Log("Password accepted!");
            _storedLevel.GoToScene();  // Go to the scene pointed at by level
        }

    }
}
