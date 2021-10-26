using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BspTree
{
    public RectInt container;
    public Room room;
    public BspTree left;
    public BspTree right;

    public bool canBeDivided = true;
    public bool isLeaf()
    {
        return left == null && right == null;
    }
    public BspTree(RectInt container)
    {
        this.container = container;
    }

    internal static BspTree Split(int numberOfIterations, RectInt container, float minRoomSizeX, float minRoomSizeY)
    {
        var node = new BspTree(container);

        if (numberOfIterations == 0)
        {
            return node;
        }

        var splittedContainers = SplitContainer(container);

        splittedContainers.ForEach(c =>
        {
            if (c.height < minRoomSizeY || c.width < minRoomSizeX)
                node.canBeDivided = false;
        });

        if (node.canBeDivided)
        {

            node.left = Split(numberOfIterations - 1, splittedContainers[0], minRoomSizeX, minRoomSizeY);
            node.right = Split(numberOfIterations - 1, splittedContainers[1], minRoomSizeX, minRoomSizeY);

            return node;
        }

        return node;

    }

    public static void GenerateRoomsInsideContainersNode(List<BspTree> activeNodes, BspTree node, ref int numberOfRooms, int maxNumberOfRoom)
    {
        // should create rooms for leafs
        if (numberOfRooms >= maxNumberOfRoom)
            return;

        if (node.isLeaf())
        {
            numberOfRooms++;
            activeNodes.Add(node);
            //GameObject.Instantiate(room, node.container.center, Quaternion.identity);
        }
        else
        {
            if (node.left != null) GenerateRoomsInsideContainersNode(activeNodes, node.left, ref numberOfRooms, maxNumberOfRoom);
            if (node.right != null) GenerateRoomsInsideContainersNode(activeNodes, node.right, ref numberOfRooms, maxNumberOfRoom);
        }
    }



    private static List<RectInt> SplitContainer(RectInt container)
    {
        RectInt c1, c2;

        if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
        {
            // vertical
            c1 = new RectInt(container.x, container.y,
                container.width, (int)UnityEngine.Random.Range(container.height * 0.5f, container.height * 0.5f));
            c2 = new RectInt(container.x, container.y + c1.height,
                container.width, container.height - c1.height);

        }
        else
        {
            // horizontal
            c1 = new RectInt(container.x, container.y,
                (int)UnityEngine.Random.Range(container.width * 0.5f, container.width * 0.5f), container.height);
            c2 = new RectInt(container.x + c1.width, container.y,
                container.width - c1.width, container.height);
        }
        return new List<RectInt> { c1, c2 };
    }
}

public class DungeonGenerator : MonoBehaviour
{
    struct RoomStruct
    {
        public GameObject roomPrefab;
        public Door door;
    }

    [Header("LevelGraph")]
    public LevelGraph levelGraph;


    [Header("Room Parameters")]
    public int dungeonSize;
    [Range(1, 6)]
    public int numberOfIterations;


    [Header("Templates Prefabs")]
    public GameObject[] Rooms;
    public GameObject[] BossRooms;
    public GameObject[] EventRooms;

    [Header("Gizmos")]
    public bool shouldDebugDrawBsp;

    private int maxNumberOfRooms;
    private int numberOfRooms;
    private float minRoomSizeX = 0;
    private float minRoomSizeY = 0;

    private BspTree tree;

    private List<BspTree> activeNodes = new List<BspTree>();
    private List<RoomStruct> RoomConnections = new List<RoomStruct>();

    bool isFirstRoom = true;

    private void Awake()
    {
        maxNumberOfRooms = levelGraph.Rooms.Count;
    }

    public void GenerateDungeon()
    {
        SetMinContainerSize();
        GenerateContainer();
        GenerateRoomsInContainer();
        AssignRoom();
        SpawnRoom();
        BuildConnections();
    }

    private void SetMinContainerSize()
    {
        List<GameObject> rooms = new List<GameObject>();
        rooms.AddRange(Rooms);
        rooms.AddRange(BossRooms);
        rooms.AddRange(EventRooms);
        float minSizeX = 0;
        float minSizeY = 0;
        foreach(var room in rooms)
        {
            if (room.GetComponent<BoxCollider2D>() == null)
            {
                Debug.LogWarning(room.gameObject.name + "need a 2D BoxCollider");
                continue;
            }
                
            var sizeX = room.GetComponent<BoxCollider2D>().size.x;
            var sizeY = room.GetComponent<BoxCollider2D>().size.y;
          
            if (sizeX > minSizeX)
                minSizeX = sizeX;
            if (sizeY > minSizeY)
                minSizeY = sizeY;
        }

        this.minRoomSizeX = minSizeX;
        this.minRoomSizeY = minSizeY;
    }

    private void GenerateContainer()
    {
        tree = BspTree.Split(numberOfIterations, new RectInt(0, 0, dungeonSize, dungeonSize), minRoomSizeX, minRoomSizeY);
    }

    private void GenerateRoomsInContainer()
    {
        BspTree.GenerateRoomsInsideContainersNode(activeNodes, tree, ref numberOfRooms, maxNumberOfRooms);
    }

    private void AssignRoom()
    {
        for (int i = 0; i <= levelGraph.Rooms.Count - 1; i++)
        {
            activeNodes[i].room = (Room)levelGraph.Rooms[i];
            activeNodes[i].room.ConnectedRooms = levelGraph.Rooms[i].ConnectedRooms;
            activeNodes[i].room.RealPosition = activeNodes[i].container.center;
        }
    }

    void SpawnRoom()
    {
        foreach(var node in activeNodes)
        {
            var room = GetRoom(node);

            if (room.roomPrefab == null)
                Debug.LogError("NO CORRESPONDING ROOMPREFABS FOUND");

            var roomInstancited = Instantiate(room.roomPrefab, node.container.center, Quaternion.identity);         
            node.room.roomObject = roomInstancited.GetComponent<RoomObject>();
            room.roomPrefab = roomInstancited;
            RoomConnections.Add(room);
        }
    }
    private void BuildConnections()
    {
        foreach(var connections in RoomConnections)
        {
            if(connections.door != null)
                connections.door.pointToSpawn = connections.roomPrefab.GetComponent<RoomObject>().spawnPoint;
        }
    }

 
    private RoomStruct GetRoom(BspTree node)
    {
        RoomStruct room = new RoomStruct();
        bool isFound = false;
        int iterations = 0;
       
        do
        {
            iterations++;

            GameObject roomPrefab = GetPrefabType(node.room.Type);

            if (roomPrefab.GetComponent<RoomObject>().doors.Count != node.room.roomToConnect.Count)
            {
                roomPrefab = null;
                continue;
            }

            if (isFirstRoom)
            {
                isFirstRoom = false;
                room = new RoomStruct { roomPrefab = roomPrefab };
                return room;
            }

            isFound = CheckPossibleConnections(node.room.ConnectedRooms, ref room, roomPrefab);

        }
        while (!isFound && iterations < 100);

        return room;
    }

    bool CheckPossibleConnections(List<RoomBase> ConnectedRooms, ref RoomStruct room, GameObject roomPrefab)
    {
        List<Door> connectedDoors = new List<Door>();
        Door door = null;

        foreach (var connectedRoom in ConnectedRooms)
        {
            if (connectedRoom.roomObject == null)
                continue;

            door = connectedRoom.roomObject.doors.FirstOrDefault(d => d.roomConnectionType == roomPrefab.GetComponent<RoomObject>().roomConnectionType && !d.isAssigned);

            if (door != null && !connectedDoors.Contains(door))
                connectedDoors.Add(door);

        }

        if (connectedDoors.Count == ConnectedRooms.Count)
        {
            if (door != null)
                door.isAssigned = true;

            room = new RoomStruct { roomPrefab = roomPrefab, door = door };
            return true;
        }

        else
            room = new RoomStruct { roomPrefab = null, door = null };

        return false;

    }

    private GameObject GetPrefabType(RoomType type)
    {
        switch (type)
        {
            default:
                return null;

            case RoomType.Room:
                return Rooms[UnityEngine.Random.Range(0, Rooms.Length)];

            case RoomType.Boss:
                return BossRooms[UnityEngine.Random.Range(0, BossRooms.Length)];

            case RoomType.Event:
                return EventRooms[UnityEngine.Random.Range(0, EventRooms.Length)];
        }
    }

    #region Gizmos

    void OnDrawGizmos()
    {
        AttemptDebugDrawBsp();
    }

    private void OnDrawGizmosSelected()
    {
        AttemptDebugDrawBsp();
    }

    void AttemptDebugDrawBsp()
    {
        if (shouldDebugDrawBsp)
        {
            DebugDrawBsp();
        }
    }

    public void DebugDrawBsp()
    {
        if (tree == null) return; // hasn't been generated yet

        DebugDrawBspNode(tree); // recursive call
    }

    public void DebugDrawBspNode(BspTree node)
    {
        // DrawCorridor(node);
        // Container
        Gizmos.color = Color.green;
        // top
        Gizmos.DrawLine(new Vector3(node.container.x, node.container.y, 0), new Vector3Int(node.container.xMax, node.container.y, 0));
        // right
        Gizmos.DrawLine(new Vector3(node.container.xMax, node.container.y, 0), new Vector3Int(node.container.xMax, node.container.yMax, 0));
        // bottom
        Gizmos.DrawLine(new Vector3(node.container.x, node.container.yMax, 0), new Vector3Int(node.container.xMax, node.container.yMax, 0));
        // left
        Gizmos.DrawLine(new Vector3(node.container.x, node.container.y, 0), new Vector3Int(node.container.x, node.container.yMax, 0));

        // children
        if (node.left != null) DebugDrawBspNode(node.left);
        if (node.right != null) DebugDrawBspNode(node.right);

        DrawCorridor(node);
    }

    void DrawCorridor(BspTree node)
    {
        if (node.room == null)
            return;

        foreach (var c in node.room.ConnectedRooms)
        {
            Gizmos.DrawLine(node.container.center, c.RealPosition);
        }

        if (node.left != null) DrawCorridor(node.left);
        if (node.right != null) DrawCorridor(node.right);
    }

    #endregion
}
