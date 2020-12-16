using UnityEngine;
using UnityEditor;
using Utilities;
using GameLogic.EntityStats;

public class StatDataAsset
{
    [MenuItem("Assets/Create/StatData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<StatData>();
    }
}
