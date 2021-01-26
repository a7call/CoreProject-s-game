using System.Collections;
using UnityEngine;

namespace Edgar.Unity
{
    public abstract class DungeonGeneratorInputBase : ScriptableObject, IPipelineTask<DungeonGeneratorPayload>
    {
        public DungeonGeneratorPayload Payload { get; set; }

        public IEnumerator Process()
        {
            Payload.LevelDescription = GetLevelDescription();
            yield return null;
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}