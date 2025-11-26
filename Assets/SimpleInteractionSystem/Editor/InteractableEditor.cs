using UnityEditor;

namespace SimpleInteractionSystem
{
    [CustomEditor(typeof(Interactable))]
    public class Interactable : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This script requires a Collider or Collider2D.", MessageType.Info);
            DrawDefaultInspector();
        }
    }
}
