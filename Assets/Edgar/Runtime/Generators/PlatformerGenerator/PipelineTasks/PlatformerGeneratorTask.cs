using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    public class PlatformerGeneratorTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        private readonly DungeonGeneratorConfig config;

        public PlatformerGeneratorTask(DungeonGeneratorConfig config)
        {
            this.config = config;
        }

        public override IEnumerator Process()
        {
            var levelDescription = Payload.LevelDescription;

            if (config.Timeout <= 0)
            {
                throw new ArgumentException($"{nameof(config.Timeout)} must be greater than 0", nameof(config.Timeout));
            }

            var rootGameObject = config.RootGameObject;

            if (rootGameObject == null)
            {
                rootGameObject = GameObject.Find("Generated Level");

                if (rootGameObject == null)
                {
                    rootGameObject = new GameObject("Generated Level");
                }
            }

            foreach (var child in rootGameObject.transform.Cast<Transform>().ToList()) {
                child.transform.parent = null;
                PostProcessUtils.Destroy(child.gameObject);
            }

            var levelDescriptionGrid2D = levelDescription.GetLevelDescription();
            levelDescriptionGrid2D.MinimumRoomDistance = 1;
            levelDescriptionGrid2D.RoomTemplateRepeatModeOverride = GeneratorUtils.GetRepeatMode(config.RepeatModeOverride);

            var configuration = new GraphBasedGeneratorConfiguration<RoomBase>()
            {
                EarlyStopIfTimeExceeded = TimeSpan.FromMilliseconds(config.Timeout),
            };

            // We create the instance of the dungeon generator and inject the correct Random instance
            var generator = new GraphBasedGeneratorGrid2D<RoomBase>(levelDescriptionGrid2D, configuration);
            generator.InjectRandomGenerator(Payload.Random);

            // Run the generator in a different class so that the computation is not blocking
            LayoutGrid2D<RoomBase> layout = null;
            var task = Task.Run((Action) (() => layout = generator.GenerateLayout()));

            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (layout == null)
            {
                throw new InvalidOperationException("Timeout was reached when generating level");
            }

            var generatedLevel = GeneratorUtils.TransformLayout(layout, levelDescription, rootGameObject);
            var stats = new GeneratorStats()
            {
                Iterations = generator.IterationsCount,
                TimeTotal = generator.TimeTotal,
            };

            Debug.Log($"Layout generated in {stats.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{stats.Iterations} iterations needed, {stats.Iterations / (stats.TimeTotal / 1000d):0} iterations per second");

            ((IGraphBasedGeneratorPayload) Payload).GeneratedLevel = generatedLevel;
            Payload.GeneratorStats = stats;

            yield return null;
        }
    }
}