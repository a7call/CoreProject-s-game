
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
            // If we want a different cell size, we can configure that here
            // grid.cellSize = new Vector3(1, 2, 1);
            // And now we create individual tilemap layers
            var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject, 0);
            AddCompositeCollider(floorTilemapObject, true);
            var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 1);
            AddCompositeCollider(wallsTilemapObject);
            var wallsTilemapDown =  CreateTilemapGameObject("WallsDown", gameObject, 25);
            AddCompositeCollider(wallsTilemapDown);
            var CollideObject = CreateTilemapGameObject("CollideObjects", gameObject, 3);
            AddCompositeCollider(CollideObject);
            CreateTilemapGameObject("Additionnal Layer 1", gameObject, 1);
            CreateTilemapGameObject("Additionnal Layer 2", gameObject, 3);
        }
        /// <summary>
        /// Helper to create a tilemap layer
        /// </summary>
        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
        {
            // Create a new game object that will hold our tilemap layer
            var tilemapObject = new GameObject(name);
            // Make sure to correctly set the parent
            tilemapObject.transform.SetParent(parentObject.transform);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = sortingOrder;
            return tilemapObject;
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
