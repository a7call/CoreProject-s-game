using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // let's leverage the default implementation for later tile assignment
        DrawDefaultInspector();

        DungeonGenerator myScript = (DungeonGenerator)target;
        if (GUILayout.Button("Generate Dungeon"))
        {
            if(Application.isPlaying)
                myScript.GenerateDungeon();
        }
    }

}