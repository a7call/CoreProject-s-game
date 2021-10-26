using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomState
{
    Cleared,
    UnCleared,
    Active
}
public class Room : RoomBase
{
    public RoomType Type;

    public RoomState currentState;

    /// <summary>
    ///     Room templates assigned to the room.
    /// </summary>
    public List<GameObject> IndividualRoomTemplates = new List<GameObject>();
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
            case RoomType.Boss:
                backgroundColor = new Color(128 / 256f, 0 / 256f, 0 / 256f);
                break;

            case RoomType.Event:
                backgroundColor = new Color(102 / 256f, 0 / 256f, 204 / 256f);
                break;

            case RoomType.Spawn:
                backgroundColor = new Color(50 / 256f, 0 / 256f, 128 / 256f);
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

    public event Action onSwitchRoomState;
    public void SetRoomState(RoomState state)
    {
        currentState = state;
        if (onSwitchRoomState != null)
            onSwitchRoomState();
    }
}
