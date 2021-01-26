using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Edgar.Unity.Editor
{
    public static class PopupDatabasePro
    { 
        public static ScenePopup GetFogOfWarPopup()
        {
            const string sceneName = "FogOfWar";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see an example of a fog of war setup.");
            sb.AppendLine();
            sb.BeginAdditionalSteps();
            sb.AppendLine(" - To enable Fog of War in URP/LWRP, <b>add custom renderer feature</b> (see link below)");
            sb.EndAdditionalSteps();

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                scenePath: "/FogOfWarExample/",
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/fog-of-war#add-custom-renderer-feature"),
                        "How to add custom renderer feature for Fog of War"),
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/fog-of-war"), "Fog of War docs"),
                });
        }

        public static ScenePopup GetMinimapPopup()
        {
            const string sceneName = "Minimap";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see an example of a minimap setup.");
            sb.AppendLine();
            sb.BeginAdditionalSteps();
            sb.AppendLine(" - Create a layer called \"<b>Minimap</b>\".");
            sb.AppendLine(" - To enable Fog of War in URP/LWRP, <b>add custom renderer feature</b> (see link below)");
            sb.EndAdditionalSteps();

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                scenePath: $"/MinimapExample/",
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/fog-of-war#add-custom-renderer-feature"),
                        "How to add custom renderer feature for Fog of War"),
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/minimap"), "Minimap docs"),
                });
        }

        public static ScenePopup GetDeadCellsRooftopPopup()
        {
            return GetDeadCellsPopup("DeadCellsRooftop", "In this scene, you can see an example of a rooftop level that is inspired by the game Dead Cells.");
        }

        public static ScenePopup GetDeadCellsUndergroundPopup()
        {
            return GetDeadCellsPopup("DeadCellsUnderground", "In this scene, you can see an example of a underground level that is inspired by the game Dead Cells.");
        }

        private static ScenePopup GetDeadCellsPopup(string sceneName, string description)
        {
            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine(description);
            sb.AppendLine();
            sb.BeginAdditionalSteps();
            sb.AppendLine(" - Create a layer called \"<b>StaticEnvironment</b>\".");
            sb.AppendLine(" - Create a layer called \"<b>LevelMap</b>\".");
            sb.AppendLine(" - Enable \"<b>Auto Sync Transforms</b>\" in Physics2D settings for colliders to work properly");
            sb.EndAdditionalSteps();

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                scenePath: $"/DeadCells/",
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("examples/dead-cells"), "Dead Cells docs"),
                });
        }

        public static ScenePopup GetGungeonPopup()
        {
            const string sceneName = "EnterTheGungeon";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see an example of a level that is inspired by the game Enter the Gungeon.");
            sb.AppendLine();
            sb.BeginAdditionalSteps();
            sb.AppendLine(" - To enable Fog of War in URP/LWRP, <b>add custom renderer feature</b> (see link below)");
            sb.EndAdditionalSteps();

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/fog-of-war#add-custom-renderer-feature"),
                        "How to add custom renderer feature for Fog of War"),
                    new PopupLink(PopupHelpers.GetDocsUrl("examples/enter-the-gungeon"), "Enter the Gungeon docs"),
                });
        }

        public static ScenePopup GetIsometric1Popup()
        {
            const string sceneName = "Isometric1";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see an example of an isometric level generator.");

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("examples/isometric-1"), "Isometric 1 docs"),
                });
        }
    }
}