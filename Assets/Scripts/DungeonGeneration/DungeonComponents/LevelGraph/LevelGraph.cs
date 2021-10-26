using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// </summary>
[CreateAssetMenu(fileName = "LevelGraph", menuName = "Dungeon/Level graph")]
public class LevelGraph : ScriptableObject
{
    public LevelGraphEditorData EditorData = new LevelGraphEditorData();
    /// <summary>
    ///     Set of room templates that is used for room that do not have any room templates assigned.
    /// </summary>
    public List<GameObject> DefaultIndividualRoomTemplates = new List<GameObject>();


    /// <summary>
    ///     Set of room templates that are used for corridor rooms.
    /// </summary>
    public List<GameObject> CorridorIndividualRoomTemplates = new List<GameObject>();


    public string RoomType = typeof(Room).FullName;

    public string ConnectionType = typeof(Connection).FullName;
    /// <summary>
    ///     List of rooms in the graph.
    /// </summary>
    [HideInInspector]
    public List<RoomBase> Rooms = new List<RoomBase>();

    /// <summary>
    ///     List of connections in the graph.
    /// </summary>
    [HideInInspector]
    public List<ConnectionBase> Connections = new List<ConnectionBase>();

    public bool IsDirected = false;
}
