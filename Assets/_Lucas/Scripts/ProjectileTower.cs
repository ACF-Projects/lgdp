using RushHour.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefense/ProjectileTowerData", fileName = "New ProjectileTowerData")]
public class ProjectileTowerData : TowerData
{

    [Header("Audio Assignments")]
    [SerializeField] private AudioClip _projectileShootSFX;

}