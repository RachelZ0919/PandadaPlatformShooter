using UnityEngine;
using UnityEditor;

namespace GameLogic.EntityBehavior
{
    [CustomEditor(typeof(MovingBehavior))]
    public class MovingBehaviorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}

