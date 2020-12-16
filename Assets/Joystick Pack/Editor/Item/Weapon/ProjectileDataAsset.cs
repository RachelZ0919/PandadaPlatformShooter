using UnityEngine;
using UnityEditor;
using Utilities;
using GameLogic.Item.Weapon;

public class ProjectileDataAsset
{
    [MenuItem("Assets/Create/ProjectileData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ProjectileData>();
    }
}
