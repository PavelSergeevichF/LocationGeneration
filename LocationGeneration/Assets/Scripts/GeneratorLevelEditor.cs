using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateLevelView))]
public class GeneratorLevelEditor : Editor
{
    private GeneratorLevelController _generatorLevelController;
    private void OnEnable()
    {
        var generateLevelView = (GenerateLevelView)target;
        _generatorLevelController = new GeneratorLevelController(generateLevelView);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var tileMap = serializedObject.FindProperty("_tileMapGround");
        EditorGUILayout.PropertyField(tileMap);
        if (GUI.Button(new Rect(10, 0, 60, 50), "Generate"))
            _generatorLevelController.GenerateLevel();
        if (GUI.Button(new Rect(10, 55, 60, 50), "Clear"))
            _generatorLevelController.ClearTileMap();

        GUILayout.Space(100);

        serializedObject.ApplyModifiedProperties();
    }
}
