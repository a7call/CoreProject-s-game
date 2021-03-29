using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(Doors))]
	public class DoorsInspector : UnityEditor.Editor
    {
        private Mode currentMode;

        private Vector3Int firstPoint;

		private bool hasFirstTile;

		private bool hasSecondTile;

        public void OnEnable()
		{
			hasFirstTile = false;
			hasSecondTile = false;
            currentMode = Mode.Idle;

			SceneView.RepaintAll();
        }

		public void OnSceneGUI()
		{
			var doors = target as Doors;
			
			switch (doors.SelectedMode)
			{
				case Doors.DoorMode.Manual:
					HandleManualMode();
					break;

				case Doors.DoorMode.Simple:
					HandleSimpleMode();
					break;
			}
        }

		private void HandleSimpleMode()
		{
			var doors = target as Doors;
			var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

			try
			{
				var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(doors.gameObject);

                if (polygon == null)
                {
                    return;
                }

				foreach (var line in polygon.GetLines())
				{
					if (line.Length - 2 * doors.DistanceFromCorners < doors.DoorLength - 1)
						continue;

					var doorLine = line.Shrink(doors.DistanceFromCorners);
                    var from = doorLine.From;
                    var to = doorLine.To;
                    var color = doors.Socket != null ? doors.Socket.GetColor() : Color.red;

                    GridUtils.DrawRectangleOutline(grid, from.ToUnityIntVector3(), to.ToUnityIntVector3(), color, new Vector2(0.1f, 0.1f));
				}
			}
			catch (ArgumentException)
			{

			}
		}

		private void HandleManualMode()
		{
			var doors = target as Doors;
            var grid = doors.gameObject.GetComponentInChildren<Grid>();

            // Draw all doors
            for (var i = 0; i < doors.DoorsList.Count; i++)
            {
                var door = doors.DoorsList[i];
                var color = door.Socket != null ? door.Socket.GetColor() : Color.red;
                var label = $"{i}";

                if (door.Direction != DoorDirection.Undirected)
                {
                    label += door.Direction == DoorDirection.Entrance ? " - In" : " - Out";
                }

                GridUtils.DrawRectangleOutline(grid, door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3(), color, new Vector2(0.1f, 0.1f), true, label);
            }

            switch (currentMode)
            {
                case Mode.AddDoors:
                    HandleAddDoors();
                    break;

                case Mode.DeleteDoors:
                    HandleDeleteDoors();
                    break;
            }
		}

        private void HandleDeleteDoors()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
            var e = Event.current;

            var tilePosition = GetCurrentTilePosition();

            // Make sure that the current active object in the inspector is not deselected
            Selection.activeGameObject = gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(controlId);

            if (e.type == EventType.MouseUp)
            {
                for (int i = doors.DoorsList.Count - 1; i >= 0; i--)
                {
                    var door = doors.DoorsList[i];
                    var orthogonalLine = new OrthogonalLine(door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3());

                    if (orthogonalLine.Contains(tilePosition) != -1)
                    {
                        Undo.RecordObject(target, "Deleted door position");
                        doors.DoorsList.RemoveAt(i);
                        EditorUtility.SetDirty(target);
                    }
                }

                Event.current.Use();
            }

            // Mouse down must be also used, otherwise there were some bugs after removing doors
            if (e.type == EventType.MouseDown)
            {
                Event.current.Use();
            }
        }

        private void HandleAddDoors()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
			var e = Event.current;
            var grid = gameObject.GetComponentInChildren<Grid>();


            // Make sure that the current active object in the inspector is not deselected
            Selection.activeGameObject = gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
			HandleUtility.AddDefaultControl(controlId);

            // Compute tile position below the mouse cursor
            var tilePosition = GetCurrentTilePosition();

			switch (e.type)
			{
				case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        firstPoint = tilePosition;
                        hasFirstTile = true;
                        hasSecondTile = false;
                        e.Use();
                    }
                    
					break;

				case EventType.MouseUp:
                    if (e.button == 0)
                    {
                        if (hasFirstTile)
                        {
                            hasSecondTile = true;
                        }
                        e.Use();
                    }
                    
					break;
			}

            // If we have the first tile, we can show how would the door look like if we released the mouse button
			if (hasFirstTile)
			{
				var from = firstPoint;
				var to = tilePosition;

				if (from.x != to.x && from.y != to.y)
				{
					to.x = from.x;
				}

                GridUtils.DrawRectangleOutline(grid, from, to, Color.red, new Vector2(0.1f, 0.1f));
                
                // If we also have the second tile, we can complete the door
				if (hasSecondTile)
				{
					hasFirstTile = false;
					hasSecondTile = false;

					var newDoorInfo = new Door()
					{
						From = from,
						To = to,
                        Socket = doors.DefaultSocket,
                        Direction = doors.DefaultDirection,
					};

					if (!doors.DoorsList.Contains(newDoorInfo))
					{
						Undo.RecordObject(target, "Added door position");

                        doors.DoorsList.Add(newDoorInfo);

						EditorUtility.SetDirty(target);
					}
				}

				SceneView.RepaintAll();
			}
        }

        private Vector3Int GetCurrentTilePosition()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

            var mousePosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            mousePosition.z = 0;
            var tilePosition = grid.WorldToCell(mousePosition);
            tilePosition.z = 0;

            return tilePosition;
        }

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var doors = target as Doors;
			
			var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid((int) doors.SelectedMode, new[] { "Simple mode", "Manual mode"}, 2);

            EditorGUILayout.Space();

			if (doors.SelectedMode == Doors.DoorMode.Simple)
			{
                HandleSimpleModeInspector();
			}

			if (doors.SelectedMode == Doors.DoorMode.Manual)
			{
                HandleManualModeInspector();
            }

			serializedObject.ApplyModifiedProperties();  
		}

        private void HandleSimpleModeInspector()
        {
            EditorGUILayout.IntSlider(serializedObject.FindProperty(nameof(Doors.DoorLength)), 1, 10, "Door length");
            EditorGUILayout.IntSlider(serializedObject.FindProperty(nameof(Doors.DistanceFromCorners)), 0, 10, "Corner distance");
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Doors.Socket)));
        }

        private void HandleManualModeInspector()
        {
            serializedObject.Update();

            var doors = target as Doors;

            var addDoorsNew = GUILayout.Toggle(currentMode == Mode.AddDoors, "Add door positions", GUI.skin.button);
            var deleteDoorsNew = GUILayout.Toggle(currentMode == Mode.DeleteDoors, "Delete door positions", GUI.skin.button);

            if (addDoorsNew && currentMode != Mode.AddDoors)
            {
                currentMode = Mode.AddDoors;
            } 
            else if (deleteDoorsNew && currentMode != Mode.DeleteDoors)
            {
                currentMode = Mode.DeleteDoors;
            }
            else if (!addDoorsNew && !deleteDoorsNew)
            {
                currentMode = Mode.Idle;
            }

            if (GUILayout.Button("Delete all door positions"))
            {
                Undo.RecordObject(target, "Delete all door positions");

                doors.DoorsList.Clear();

                EditorUtility.SetDirty(target);
            }

            try
            {
                var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(doors.gameObject);
                var doorPositions = doors.GetDoorMode().GetDoors(polygon);

                if (doorPositions.Count != doors.DoorsList.Count)
                {
                    EditorGUILayout.HelpBox(
                        "There seems to be a door of length 1 that is at the corner of the outline, which is currently not supported. Either use outline override to change the outline or remove the door position.",
                        MessageType.Error);
                }
            }
            catch (ArgumentException)
            {

            }

            EditorGUILayout.HelpBox("The visualization of manual doors works differently than that of simple doors. If you want to add, for example, 5 doors of length 1, you have to manually add all 5 doors - not a single door with length 5.", MessageType.Warning);

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Doors.DefaultSocket)), new GUIContent("Default socket for new doors"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Doors.DefaultDirection)), new GUIContent("Default direction for new doors"));
            ShowList(serializedObject.FindProperty(nameof(Doors.DoorsList)));

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowList(SerializedProperty list)
        {
            EditorGUILayout.PropertyField(list);
            // EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);

                for (int i = 0; i < list.arraySize; i++)
                {
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent($"Door {i}"));

                    GUILayout.BeginHorizontal();

                    //if (GUILayout.Button("Highlight", EditorStyles.miniButton))
                    //{

                    //}

                    if (GUILayout.Button("Remove", EditorStyles.miniButton))
                    {
                        var oldSize = list.arraySize;
                        list.DeleteArrayElementAtIndex(i);
                        if (list.arraySize == oldSize)
                        {
                            list.DeleteArrayElementAtIndex(i);
                        }
                    }

                    GUILayout.EndHorizontal();

                    GUILayout.EndVertical();
                }

                GUILayout.EndVertical();
            }
            // EditorGUI.indentLevel -= 1;
        }

        private enum Mode
        {
            Idle, AddDoors, DeleteDoors
        }
    }
}