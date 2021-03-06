﻿using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Examples.DeadCells
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Dead Cells/Input setup", fileName = "Dead Cells Input Setup")]
    public class DeadCellsInputSetupTask : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public DeadCellsRoomTemplatesConfig RoomTemplates;

        /// <summary>
        /// This is the main method of the input setup.
        /// It prepares the description of the level for the procedural generator.
        /// </summary>
        /// <returns></returns>
        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            // Go through individual rooms and add each room to the level description
            // Room templates are resolved based on their type
            foreach (var room in LevelGraph.Rooms.Cast<DeadCellsRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            // Go through individual connections and for each connection create a corridor room
            foreach (var connection in LevelGraph.Connections.Cast<DeadCellsConnection>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                corridorRoom.Type = DeadCellsRoomType.Corridor;
                levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.CorridorRoomTemplates.ToList());
            }

            return levelDescription;
        }
    }
}