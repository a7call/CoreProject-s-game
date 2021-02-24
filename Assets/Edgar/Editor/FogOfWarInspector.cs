using Edgar.Unity.Benchmarks;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(FogOfWar))]
    public class FogOfWarInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var fogOfWar = (FogOfWar) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth * 0.6f;
            EditorGUIUtility.fieldWidth = 0;

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.ColorMode)));

            if (fogOfWar.ColorMode == FogOfWarColorMode.CustomColor)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.FogColor)));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.TransitionMode)));

            if (fogOfWar.TransitionMode == FogOfWarTransitionMode.Custom)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.TileGranularity)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.FogSmoothness)));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.Mode)));

            if (fogOfWar.Mode == FogOfWarMode.Wave)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.WaveSpeed)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.WaveRevealThreshold)));
            }

            if (fogOfWar.Mode == FogOfWarMode.FadeIn)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.FadeInDuration)));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.RevealCorridors)));

            if (fogOfWar.RevealCorridors)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.RevealCorridorsTiles)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.RevealCorridorsGradually)));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FogOfWar.InitialFogTransparency)));

            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.fieldWidth = 0;

            if (GUILayout.Button("Reload"))
            {
                fogOfWar.Reload();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}