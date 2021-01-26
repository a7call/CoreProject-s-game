using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Examples.Scripts
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Platformer 1/Post-process", fileName = "Platformer1PostProcess")]
    public class Platformer1PostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            RemoveWallsFromDoors(level);
        }

        private void RemoveWallsFromDoors(GeneratedLevel level)
        {
            // Get the tilemap that we want to delete tiles from
            var walls = level.GetSharedTilemaps().Single(x => x.name == "Walls");

            // Go through individual rooms
            foreach (var roomInstance in level.GetRoomInstances())
            {
                // Go through individual doors
                foreach (var doorInstance in roomInstance.Doors)
                {
                    // Remove all the wall tiles from door positions
                    foreach (var point in doorInstance.DoorLine.GetPoints())
                    {
                        walls.SetTile(point, null);
                    }
                }
            }
        }
    }
}
