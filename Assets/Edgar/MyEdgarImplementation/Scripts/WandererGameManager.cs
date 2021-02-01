using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity.Examples
{
    public class WandererGameManager : GameManagerBase<WandererGameManager>
    {
        // Current active room
        private RoomInstance currentRoom;

        // The room that will be active after the player leaves the current room
        private RoomInstance nextCurrentRoom;

        private long generatorElapsedMilliseconds;

        // To make sure that we do not start the generator multiple times
        private bool isGenerating;

        // Shared instance of the random numbers generator
        public Random Random;

        [Range(1, 2)]
        public int Stage = 1;

        public LevelGraph CurrentLevelGraph;

        public void Update()
        {
            if (Input.GetKey(KeyCode.G) && !isGenerating)
            {
                LoadNextLevel();
            }

            if (Input.GetKey(KeyCode.H) && !isGenerating)
            {
                Stage = Stage == 1 ? 2 : 1;
                LoadNextLevel();
            }
        }

        public override void LoadNextLevel()
        {
            isGenerating = true;

            // Show loading screen
            ShowLoadingScreen("Enter the Gungeon", $"Stage {Stage}");

            // Find the generator runner
            var generator = GameObject.Find("DUNGEONGEN").GetComponent<DungeonGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }

        /// <summary>
        /// Coroutine that generates the level.
        /// We need to yield return before the generator starts because we want to show the loading screen
        /// and it cannot happen in the same frame.
        /// It is also sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
        private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            // Configure the generator with the current stage number
            var inputTask = (WandererInputSetUp)generator.CustomInputTask;
            inputTask.Stage = Stage;

            var generatorCoroutine = this.StartSmartCoroutine(generator.GenerateCoroutine());

            yield return generatorCoroutine.Coroutine;

            yield return null;

            stopwatch.Stop();

            isGenerating = false;

            generatorCoroutine.ThrowIfNotSuccessful();

            generatorElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            RefreshLevelInfo();
            HideLoadingScreen();
        }

        private void RefreshLevelInfo()
        {
            var info = $"Generated in {generatorElapsedMilliseconds / 1000d:F}s\n";
            info += $"Stage: {Stage}, Level graph: {CurrentLevelGraph.name}\n";
            info += $"Room type: {(currentRoom?.Room as WandererRoom)?.Type}, Room template: {currentRoom?.RoomTemplatePrefab.name}";

            SetLevelInfo(info);
        }

        public void OnRoomEnter(RoomInstance roomInstance)
        {
            nextCurrentRoom = roomInstance;

            if (currentRoom == null)
            {
                currentRoom = nextCurrentRoom;
                nextCurrentRoom = null;
                RefreshLevelInfo();
            }
        }

        public void OnRoomLeave(RoomInstance roomInstance)
        {
            currentRoom = nextCurrentRoom;
            nextCurrentRoom = null;
            RefreshLevelInfo();
        }
    }
}


