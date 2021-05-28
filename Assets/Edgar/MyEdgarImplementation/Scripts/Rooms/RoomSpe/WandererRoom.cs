using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using System;
using Wanderer.Utils;

public class WandererRoom : RoomBase
{
    private double chanceToSpawn;
    public double ChanceToSpawn
    {
        get
        {
            return chanceToSpawn;
        }
    }

    public RoomType Type;

    private bool shouldSpawnMonstersTwice = false;

    public bool ShouldSpawnMonsterTwice
    {
        get
        {
            return shouldSpawnMonstersTwice;
        }
        set
        {
            shouldSpawnMonstersTwice = value;
        }
    }

    private bool shouldSpawnMonsters = false;

    public bool ShouldSpawnMonsters
    {
        get
        {
            return shouldSpawnMonsters;
        }
    }

    private int difficultyAllowed = 0;
    public int DifficultyAllowed
    {
        get
        {
            return difficultyAllowed;
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
    
    public void isAllowedToSpawnMonstersTwice()
    {
        shouldSpawnMonstersTwice = Utils.RandomBool((float)chanceToSpawn * 100);
    }
    public void isAllowedToSpawnMonsters(RoomType type)
    {
        shouldSpawnMonsters = (type == RoomType.Large || type == RoomType.Medium || type == RoomType.Small);
    }

    public void SetChanceToSpawn(RoomType type)
    {
        switch (type)
        {
            default:
                chanceToSpawn = 0;
                break;
            case RoomType.Large:
                chanceToSpawn = 0.5;
                break;
            case RoomType.Medium:
                chanceToSpawn = 0.25;
                break;
            case RoomType.Small:
                chanceToSpawn = 0.1;
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
