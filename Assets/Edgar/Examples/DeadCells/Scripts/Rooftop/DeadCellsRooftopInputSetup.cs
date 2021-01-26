using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Examples.DeadCells
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Dead Cells/Rooftop input setup", fileName = "Dead Cells Rooftop Input Setup")]
    public class DeadCellsRooftopInputSetup : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public DeadCellsRooftopRoomTemplatesConfig RoomTemplates;

        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            // Go through individual rooms and add each room to the level description
            foreach (var room in LevelGraph.Rooms.Cast<DeadCellsRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            foreach (var connection in LevelGraph.Connections.Cast<DeadCellsConnection>())
            {
                var from = (DeadCellsRoom) connection.From;
                var to = (DeadCellsRoom) connection.To;

                // If both rooms are outside, we do not need a corridor room
                if (from.Outside && to.Outside)
                {
                    levelDescription.AddConnection(connection);
                }
                // If at least one room is inside, we need a corridor room to properly connect the two rooms
                else
                {                    
                    var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                    corridorRoom.Type = DeadCellsRoomType.Corridor;

                    levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.InsideCorridorRoomTemplates.ToList());

                }
            }

            return levelDescription;
        }
    }
}