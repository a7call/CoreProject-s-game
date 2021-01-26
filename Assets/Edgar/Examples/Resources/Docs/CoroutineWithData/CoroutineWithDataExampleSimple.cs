using UnityEngine;

namespace Edgar.Unity.Examples.Resources.CoroutineWithData
{
    public class CoroutineWithDataExampleSimple : MonoBehaviour
    {
        public void Start()
        {
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
            StartCoroutine(generator.GenerateCoroutine());
        }
    }
}
