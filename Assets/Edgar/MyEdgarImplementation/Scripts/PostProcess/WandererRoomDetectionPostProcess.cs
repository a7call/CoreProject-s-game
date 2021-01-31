using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples
{
    [CreateAssetMenu(menuName = "Edgar/Wanderer/Current room detection/Post-process", fileName = "CurrentRoomDetectionPostProcess")]
    public class WandererRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                // Find floor tilemap layer
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
                var floor = tilemaps.Single(x => x.name == "Floor").gameObject;
                // Add floor collider

                // Add the room manager component
                var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();
                roomManager.RoomInstance = roomInstance;

                AddFloorCollider(floor);
                // Add current room detection handler
                floor.AddComponent<WandererCurrentRoomDetectionTriggerhandler>();
            }
        }
        protected void AddFloorCollider(GameObject floor)
        {
            var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;
            var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = true;
            compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;
            floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}

