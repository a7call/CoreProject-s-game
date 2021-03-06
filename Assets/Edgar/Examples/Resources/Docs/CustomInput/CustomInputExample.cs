﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Examples.Resources.CustomInput
{
    // [CreateAssetMenu(menuName = "Dungeon generator/Examples/Docs/My custom input task", fileName = "MyCustomInputSetup")]
    public class CustomInputExample : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;
        public bool UseCorridors;

        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            // Go through rooms in the level graph and add them to the level description
            foreach (var room in LevelGraph.Rooms)
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            // Go through connections in the level graph
            foreach (var connection in LevelGraph.Connections)
            {
                // If corridors are enabled, add corridor connection
                if (UseCorridors)
                {
                    // Create a room for the corridor
                    var corridorRoom = ScriptableObject.CreateInstance<Room>();
                    corridorRoom.Name = "Corridor";


                    levelDescription.AddCorridorConnection(connection, corridorRoom, GetCorridorRoomTemplates());
                }
                // Else connect the rooms directly
                else
                {
                    levelDescription.AddConnection(connection);
                }
            }

            return levelDescription;
        }

        /// <summary>
        /// Gets room templates for a given room.
        /// </summary>
        private List<GameObject> GetRoomTemplates(RoomBase room)
        {
            // Get room templates from a given room
            var roomTemplates = room.GetRoomTemplates();

            // If the list is empty, we use the defaults room templates from the level graph
            if (roomTemplates == null || roomTemplates.Count == 0)
            {
                var defaultRoomTemplates = LevelGraph.DefaultIndividualRoomTemplates;
                var defaultRoomTemplatesFromSets = LevelGraph.DefaultRoomTemplateSets.SelectMany(x => x.RoomTemplates);

                // Combine individual room templates with room templates from room template sets
                return defaultRoomTemplates.Union(defaultRoomTemplatesFromSets).ToList();
            }

            return roomTemplates;
        }

        /// <summary>
        /// Gets corridor room templates.
        /// </summary>
        private List<GameObject> GetCorridorRoomTemplates()
        {
            var defaultRoomTemplates = LevelGraph.CorridorIndividualRoomTemplates;
            var defaultRoomTemplatesFromSets = LevelGraph.CorridorRoomTemplateSets.SelectMany(x => x.RoomTemplates);

            return defaultRoomTemplates.Union(defaultRoomTemplatesFromSets).ToList();
        }
    }
}