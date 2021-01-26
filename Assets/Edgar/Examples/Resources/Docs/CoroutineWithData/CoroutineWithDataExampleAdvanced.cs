using System.Collections;
using UnityEngine;

namespace Edgar.Unity.Examples.Resources.CoroutineWithData
{
    public class CoroutineWithDataExampleAdvanced : MonoBehaviour
    {
        public void Start()
        {
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
            StartCoroutine(GeneratorCoroutine(generator));
        }

        private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
        {
            // Start the smart coroutine
            // StartCoroutineWithData is a custom extension method of MonoBehaviour, be sure to use the ProceduralLevelGenerator.Unity.Pro namespace
            var generatorCoroutine = this.StartSmartCoroutine(generator.GenerateCoroutine());

            // Wait until the smart coroutine is completed
            // Make sure to yield return the Coroutine property and not the generator coroutine itself!!
            yield return generatorCoroutine.Coroutine;

            // Check if the coroutine was successful
            if (generatorCoroutine.IsSuccessful)
            {
                Debug.Log("Level generated!");
            }
            // If there were any errors, we can access the Exception object
            // Or we can also rethrow the exception by calling generatorCoroutine.ThrowIfNotSuccessful();
            else
            {
                Debug.LogError("There was an error when generating the level!");
                Debug.LogError(generatorCoroutine.Exception.Message);
            }
        }
    }
}