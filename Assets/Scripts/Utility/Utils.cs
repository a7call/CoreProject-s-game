using UnityEngine;


namespace Wanderer.Utils
{
    public class Utils
    {
        public static Vector3 GetWorldPositionWithZ(Vector3 screenPos, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPos);
            return worldPosition;
        }
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0;
            return vec;
        }

        public static bool RandomBool(float trueChance)
        {
            return Random.Range(0f, 100f) <= trueChance;
        }

        public static bool RandomBool()
        {
            return RandomBool(50f);
        }
        public static float RandomizeParams(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static int RandomObjectInCollection(int collectionLenght)
        {
           return Random.Range(0, collectionLenght - 1);  
        }
    }
}

