using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edgar.Unity
{
    public class PlatformerRoomTemplateInitializer : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = new PlatformerTilemapLayersHandler();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Edgar/Platformer room template")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<PlatformerRoomTemplateInitializer>();
        }
#endif
    }
}