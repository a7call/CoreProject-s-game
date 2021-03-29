using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Creates an input for the generator from a given level graph.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class FixedLevelGraphInputTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload
    {
        private readonly FixedLevelGraphConfig config;

        public FixedLevelGraphInputTask(FixedLevelGraphConfig config)
        {
            this.config = config;
        }

        public override IEnumerator Process()
        {
            if (config.LevelGraph == null)
            {
                throw new ArgumentException("LevelGraph must not be null.");
            }

            if (config.LevelGraph.Rooms.Count == 0)
            {
                throw new ArgumentException("LevelGraph must contain at least one room.");
            }

            var levelDescription = new LevelDescription();

            // Setup individual rooms
            foreach (var room in config.LevelGraph.Rooms)
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            var typeOfRooms = config.LevelGraph.Rooms.First().GetType();

            // Add passages
            foreach (var connection in config.LevelGraph.Connections)
            {
                if (config.UseCorridors)
                {
                    var corridorRoom = (RoomBase) ScriptableObject.CreateInstance(typeOfRooms);

                    if (corridorRoom is Room basicRoom)
                    {
                        basicRoom.Name = "Corridor";
                    }
                    
                    levelDescription.AddCorridorConnection(connection, corridorRoom,
                        GetRoomTemplates(config.LevelGraph.CorridorRoomTemplateSets, config.LevelGraph.CorridorIndividualRoomTemplates));
                }
                else
                {
                    levelDescription.AddConnection(connection);
                }
            }

            CheckIfDirected(levelDescription);
                
            Payload.LevelDescription = levelDescription;

            yield return null;
        }

        private void CheckIfDirected(LevelDescription levelDescription)
        {
            if (config.LevelGraph.IsDirected)
            {
                return;
            }

            foreach (var roomTemplate in levelDescription.GetPrefabToRoomTemplateMapping().Values)
            {
                if (roomTemplate.Doors is ManualDoorModeGrid2D doorMode)
                {
                    if (doorMode.Doors.Any(x => x.Type != GraphBasedGenerator.Common.Doors.DoorType.Undirected))
                    {
                        throw new ArgumentException(
                            $"LevelGraph.IsDirected must be enabled when using entrance-only or exit-only doors.");
                    }
                }
            }
        }

        private List<GameObject> GetRoomTemplates(List<RoomTemplatesSet> roomTemplatesSets, List<GameObject> individualRoomTemplates)
        {
            return individualRoomTemplates
                .Where(x => x != null)
                .Union(roomTemplatesSets
                    .Where(x => x != null)
                    .SelectMany(x => x.RoomTemplates))
                .Distinct()
                .ToList();
        }

        /// <summary>
        ///     Setups room shapes for a given room.
        /// </summary>
        /// <param name="room"></param>
        protected List<GameObject> GetRoomTemplates(RoomBase room)
        {
            var roomTemplates = room.GetRoomTemplates();

            if (roomTemplates == null || roomTemplates.Count == 0)
            {
                return GetRoomTemplates(config.LevelGraph.DefaultRoomTemplateSets, config.LevelGraph.DefaultIndividualRoomTemplates);
            }

            return roomTemplates;
        }
    }
}