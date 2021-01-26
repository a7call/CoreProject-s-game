using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples.DeadCells
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Dead Cells/Rooftop post process", fileName = "Dead Cells Rooftop Post Process")]
    public class DeadCellsRooftopPostProcessTask : DungeonGeneratorPostProcessBase
    {
        public bool AddWalls = true;

        public int WallDepth = 100;

        public TileBase WallTile;
        
        private Tilemap wallsTilemap;

        private Tilemap backgroundTilemap;

        public override void RegisterCallbacks(PriorityCallbacks<DungeonGeneratorPostProcessCallback> callbacks)
        {
            if (AddWalls)
            {
                callbacks.RegisterCallbackAfter(PostProcessPriorities.InitializeSharedTilemaps, AddWallsUnderRooms);
            }
        }

        private void AddWallsUnderRooms(GeneratedLevel level, LevelDescription levelDescription)
        {
            // Store the "Walls" and "Background" tilemaps
            var tilemaps = level.GetSharedTilemaps();
            wallsTilemap = tilemaps.Single(x => x.name == "Walls");
            backgroundTilemap = tilemaps.Single(x => x.name == "Background");

            // Add walls under outside rooms
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var room = (DeadCellsRoom) roomInstance.Room;

                if (room.Outside)
                {
                    AddWallsUnderRoom(roomInstance);
                }
            }
        }

        private void AddWallsUnderRoom(RoomInstance roomInstance)
        {
            // Get the room template and all the used tiles
            var roomTemplate = roomInstance.RoomTemplateInstance;
            var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplate);
            var usedTiles = RoomTemplatesLoader.GetUsedTiles(tilemaps).ToList();
            var roomTemplateWalls = tilemaps.Single(x => x.name == "Walls");

            // Find the minimum y coordinate of all the tiles and use it to find the bottom layer of tiles
            var minY = usedTiles.Min(x => x.y);
            var bottomLayerTiles = usedTiles.Where(x => x.y == minY).ToList();

            foreach (var pos in bottomLayerTiles)
            {
                var tilemap = backgroundTilemap;

                // Use the walls tilemap only if the collider is really needed
                // That means we only use it if the tile is the border tile of a tower
                var leftTilePos = pos + Vector3Int.left;
                var rightTilePos = pos + Vector3Int.right;
                if (roomTemplateWalls.GetTile(pos) != null && !(bottomLayerTiles.Contains(leftTilePos) && bottomLayerTiles.Contains(rightTilePos)))
                {
                    tilemap = wallsTilemap;
                }

                // Add tiles under this position
                for (int i = 1; i <= WallDepth; i++)
                {
                    var wallPosition = roomInstance.Position + pos + Vector3Int.down * i;
                    tilemap.SetTile(wallPosition, WallTile);
                }
            }
        }
    }
}