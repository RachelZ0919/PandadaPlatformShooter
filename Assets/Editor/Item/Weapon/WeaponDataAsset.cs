using UnityEngine;
using UnityEditor;
using Utilities;
using GameLogic.Item.Weapon;

public class WeaponDataAsset
{
    [MenuItem("Assets/Create/WeaponData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<WeaponData>();
    }
}
