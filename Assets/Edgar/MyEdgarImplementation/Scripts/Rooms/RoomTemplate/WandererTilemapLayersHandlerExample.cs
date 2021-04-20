
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Edgar.Unity
{
    [CreateAssetMenu(menuName = "Edgar/Wanderer/WandererTilemapLayersHandler", fileName = "WandererTilemapLayersHandler")]
    public class WandererTilemapLayersHandlerExample : TilemapLayersHandlerBase
    {
   
        public override void InitializeTilemaps(GameObject gameObject)
        {
            // First make sure that you add the grid component
            var grid = gameObject.AddComponent<Grid>();

            #region Floor
            var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject, -1, "BackGround", TilemapRenderer.Mode.Chunk);
            AddCompositeCollider(floorTilemapObject, true);
            #endregion

            #region ForeGround Wall
            var ForeGroundWall = CreateTilemapGameObject("ForeGroundWall", gameObject, 0, "ForeGround", TilemapRenderer.Mode.Chunk);
            AddCompositeCollider(ForeGroundWall);
            #endregion

            #region BackGround Wall
            var BackGroundWall = CreateTilemapGameObject("BackGroundWall", gameObject, 100, "BackGround", TilemapRenderer.Mode.Chunk) ;
            AddCompositeCollider(BackGroundWall);
            #endregion

            #region Additionnal Layer 1 + children
            GameObject AdditionnalLayer1 = CreateTilemapGameObject("Additionnal Layer 1", gameObject, 0, "Default", TilemapRenderer.Mode.Chunk);
            CreateTilemapGameObject("Grid", AdditionnalLayer1, 50, "BackGround", TilemapRenderer.Mode.Chunk);
            CreateTilemapGameObject("Background", AdditionnalLayer1, -100, "BackGround", TilemapRenderer.Mode.Chunk);
            #region Neon
            var NeonLayer = CreateObjectContainer("Neon", AdditionnalLayer1);
            CreateObjectContainer("Neon Face", NeonLayer);
            CreateObjectContainer("Neon Left", NeonLayer);
            CreateObjectContainer("Neon Right", NeonLayer);
            #endregion
            #endregion

            #region Props
            CreateObjectContainer("Props", gameObject.transform.parent.gameObject);
            #endregion

            #region Lights
            var LightContainerLayer = CreateObjectContainer("LightContainer", gameObject.transform.parent.gameObject);

            #region NeonLight
            var NeonLightLayer = CreateObjectContainer("NeonLights", LightContainerLayer);
            CreateObjectContainer("NeonLights Face", NeonLightLayer);
            CreateObjectContainer("NeonLights Left", NeonLightLayer);
            CreateObjectContainer("NeonLights Right", NeonLightLayer);
            #endregion

            #region Freefrom
            CreateObjectContainer("Freeform LightContainer", LightContainerLayer);
            #endregion

            #endregion
        }
        /// <summary>
        /// Helper to create a tilemap layer
        /// </summary>
        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder, string sortingLayer, TilemapRenderer.Mode Mode)
        {
            // Create a new game object that will hold our tilemap layer
            var tilemapObject = new GameObject(name);
            // Make sure to correctly set the parent
            tilemapObject.transform.SetParent(parentObject.transform);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = sortingOrder;
            tilemapRenderer.mode = Mode;
           
            tilemapRenderer.sortingLayerName = sortingLayer;
            return tilemapObject;
        }

        protected GameObject CreateObjectContainer(string name, GameObject parentObject)
        {
            // Create a new game object that will hold our tilemap layer
            var ObjectContainer = new GameObject(name);
            // Make sure to correctly set the parent
            ObjectContainer.transform.SetParent(parentObject.transform);
            return ObjectContainer;
        }

        /// <summary>
        /// Helper to add a collider to a given tilemap game object.
        /// </summary>
        protected void AddCompositeCollider(GameObject tilemapGameObject, bool isTrigger = false)
        {
            var tilemapCollider2D = tilemapGameObject.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;
            var compositeCollider2d = tilemapGameObject.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = isTrigger;
            tilemapGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

}
