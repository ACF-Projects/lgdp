using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushHour
{
    public class UITitlePerson : MonoBehaviour
    {

        [Header("Possible Sprites")]
        [SerializeField] private List<Sprite> _personSprites;
        [Header("Object Assignments")]
        [SerializeField] private Image _personImage;
        
        /// <summary>
        /// Randomizes the look of this sprite to be one of the possible
        /// sprites in `_personSprites`.
        /// </summary>
        public void Init()
        {
            if (_personSprites.Count == 0)
            {
                Debug.LogError("No available sprites found in UITitlePerson!", this);
                return;
            }
            _personImage.sprite = _personSprites[Random.Range(0, _personSprites.Count)];
        }

    }
}
