﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Edgar.Unity.Editor
{
    [InitializeOnLoad]
    public static class PopupManager
    {
        private static readonly List<ScenePopup> scenePopups = new List<ScenePopup>();

        static PopupManager()
        {
            Startup();
        }

        private static void Startup()
        {
            EditorSceneManager.sceneOpened += SceneOpenedCallback;

            scenePopups.Add(PopupDatabase.GetExample1Popup());
            scenePopups.Add(PopupDatabase.GetExample2Popup());
            scenePopups.Add(PopupDatabase.GetCurrentRoomDetectionPopup());

            AddProPopups();
        }

        private static void AddProPopups()
        {
            scenePopups.Add(PopupDatabasePro.GetFogOfWarPopup());
            scenePopups.Add(PopupDatabasePro.GetMinimapPopup());
            scenePopups.Add(PopupDatabasePro.GetDoorSocketsPopup());
            scenePopups.Add(PopupDatabasePro.GetDirectedLevelGraphsPopup());
            scenePopups.Add(PopupDatabasePro.GetMinimap2Popup());
            scenePopups.Add(PopupDatabasePro.GetDeadCellsRooftopPopup());
            scenePopups.Add(PopupDatabasePro.GetDeadCellsUndergroundPopup());
            scenePopups.Add(PopupDatabasePro.GetGungeonPopup());
            scenePopups.Add(PopupDatabasePro.GetIsometric1Popup());
        }

        private static void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
        {
            PopupWindow.CloseLastPopup();

            var disabledPopupsText = EditorPrefs.GetString(EditorConstants.DisabledPopupsEditorPrefsKey, "");
            var disabledPopups = disabledPopupsText.Split(',');

            foreach (var popup in scenePopups)
            {
                if (!disabledPopups.Contains(popup.Id) && scene.name == popup.SceneName)
                {
                    if (popup.ScenePath == null || scene.path.Contains(popup.ScenePath))
                    {
                        PopupWindow.Open(popup);
                        return;
                    }
                }
            }
        }

        public static void DisablePopup(IPopup popup)
        {
            var disabledPopups = EditorPrefs.GetString(EditorConstants.DisabledPopupsEditorPrefsKey, "");

            if (!string.IsNullOrEmpty(disabledPopups))
            {
                disabledPopups += ",";
            }

            disabledPopups += popup.Id;

            EditorPrefs.SetString(EditorConstants.DisabledPopupsEditorPrefsKey, disabledPopups);
        }

        [MenuItem("Edit/Edgar - Enable all popups again")]
        private static void EnableAllPopups()
        {
            EditorPrefs.SetString(EditorConstants.DisabledPopupsEditorPrefsKey, "");
        }
    }
}