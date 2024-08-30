using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace RushHour.Data
{
    public abstract class TowerData : ScriptableObject
    {

        [field: Header("Tower Properties")]
        [field: SerializeField] public float Cost { get; protected set; }
        [field: SerializeField] public float SalaryPerHour { get; protected set; }
        [field: SerializeField] public float EffectRadius { get; protected set; }

        [field: Header("Other Tower Data")]
        [field: SerializeField] public string Name { get; protected set; }
        /// <summary>
        /// Tower Icon for UI -- should be square
        /// </summary>
        [field:SerializeField, ShowAssetPreview] public Sprite UIIcon { get; protected set; }
        [field: SerializeField, ShowAssetPreview] public Sprite TowerSprite { get; protected set; }
        [field: SerializeField] public Sprite EffectPreviewSprite { get; protected set; }
        [field: SerializeField] public Vector2 SpriteScale { get; protected set; }
        [field: SerializeField, ResizableTextArea] public string Description { get; protected set; }
        [Header("Audio Data (Optional)")]
        [field: SerializeField] public AudioClip PlacementSFX;  // Sound to play after placing
        [field: SerializeField] public float PlacementSFXVolume = 1;

        public GameObject towerPrefab;

        /// <summary>
        /// Calculate the radius of this sprite by using the assigned Icon and SpriteScale. (uses width)
        /// Divide by two because it's the radius, not the whole width.
        /// </summary>
        public float SpriteRadius => TowerSprite.textureRect.width / TowerSprite.pixelsPerUnit * SpriteScale.x / 2;

    }
}
