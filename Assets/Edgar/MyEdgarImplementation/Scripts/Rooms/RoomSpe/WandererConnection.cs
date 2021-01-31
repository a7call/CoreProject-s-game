
using UnityEngine;

namespace Edgar.Unity
{
    public class WandererConnection : Connection
    {
        // Start is called before the first frame update
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
