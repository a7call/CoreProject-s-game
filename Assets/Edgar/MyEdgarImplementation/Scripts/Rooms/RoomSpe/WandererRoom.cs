using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;


public class WandererRoom : RoomBase
{
    public RoomType Type;

    private int difficultyAllowed = 0;
    public int DifficultyAllowed
    {
        get
        {
            return difficultyAllowed;
        }
        private set
        {
            SetRoomDifficulty(Type);
        }
    }

    public void SetRoomDifficulty(RoomType type)
    {

        switch (type)
        {
            default:
                difficultyAllowed = 0;
                break;
            case RoomType.Large:
                difficultyAllowed = 15;
                break;
            case RoomType.Medium:
                difficultyAllowed = 10;
                break;
            case RoomType.Small:
                difficultyAllowed = 5;
                break;
        }
    }


    public override List<GameObject> GetRoomTemplates()
    {
        // We do not need any room templates here because they are resolved based on the type of the room.
        return null;
    }

    public override string GetDisplayName()
    {
        // Use the type of the room as its display name.
        return Type.ToString();
    }

    public override RoomEditorStyle GetEditorStyle(bool isFocused)
    {
        var style = base.GetEditorStyle(isFocused);

        var backgroundColor = style.BackgroundColor;

        // Use different colors for different types of rooms
        switch (Type)
        {
            case RoomType.Spawn:
                backgroundColor = new Color(38 / 256f, 115 / 256f, 38 / 256f);
                break;

            case RoomType.Boss:
                backgroundColor = new Color(128 / 256f, 0 / 256f, 0 / 256f);
                break;

            case RoomType.Shop:
                backgroundColor = new Color(102 / 256f, 0 / 256f, 204 / 256f);
                break;

            case RoomType.Reward:
                backgroundColor = new Color(102 / 256f, 0 / 256f, 204 / 256f);
                break;
        }

        style.BackgroundColor = backgroundColor;

        // Darken the color when focused
        if (isFocused)
        {
            style.BackgroundColor = Color.Lerp(style.BackgroundColor, Color.black, 0.7f);
        }

        return style;
    }
}
