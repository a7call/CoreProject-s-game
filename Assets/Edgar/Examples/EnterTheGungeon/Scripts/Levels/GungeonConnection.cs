﻿using UnityEngine;

namespace Edgar.Unity.Examples.EnterTheGungeon
{
    public class GungeonConnection : Connection
    {
        // Whether the corresponding corridor should be locked
        public bool IsLocked;

        public override ConnectionEditorStyle GetEditorStyle(bool isFocused)
        {
            var style = base.GetEditorStyle(isFocused);

            // Use red color when locked
            if (IsLocked)
            {
                style.LineColor = Color.red;
            }

            return style;
        }
    }
}