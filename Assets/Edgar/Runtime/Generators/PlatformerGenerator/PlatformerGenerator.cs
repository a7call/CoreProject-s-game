namespace Edgar.Unity
{
    public class PlatformerGenerator : DungeonGeneratorBase
    {
        protected override IPipelineTask<DungeonGeneratorPayload> GetPostProcessingTask()
        {
            return new PostProcessTask<DungeonGeneratorPayload>(PostProcessConfig, () => new PlatformerTilemapLayersHandler(), CustomPostProcessTasks);
        }

        protected override IPipelineTask<DungeonGeneratorPayload> GetGeneratorTask()
        {
            return new PlatformerGeneratorTask<DungeonGeneratorPayload>(GeneratorConfig);
        }
    }
}