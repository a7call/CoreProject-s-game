using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using System;
using Wanderer.Utils;
using System.Linq;

public enum RoomState
{
    Cleared,
    UnCleared,
    CurrentlyUsed,
}
public class WandererRoom : RoomBase
{

    public double ChanceToSpawn
    {
        get;
        private set;
    }

    public RoomType Type;

    public RoomState roomState;

    public event Action onSwitchRoomState;
    public void SetRoomState(RoomState state)
    {
        roomState = state;
        if (onSwitchRoomState != null)
            onSwitchRoomState();
    }

    public bool ShouldSpawnMonstersTwice
    {
        get;
        set;
    }

    public bool ShouldSpawnMonsters
    {
        get;
       private set;
    }

    public int MaxDifficulty
    {
        get;
        private set;
    }

    public void SetRoomDifficulty(RoomType type)
    {

        switch (type)
        {
            default:
                MaxDifficulty = 0;
                break;
            case RoomType.Large:
                MaxDifficulty = 15;
                break;
            case RoomType.Medium:
                MaxDifficulty = 10;
                break;
            case RoomType.Small:
                MaxDifficulty = 5;
                break;
        }
    }
    
    public void isAllowedToSpawnMonstersTwice()
    {
        ShouldSpawnMonstersTwice = Utils.RandomBool((float)ChanceToSpawn * 100);
    }
    public void isAllowedToSpawnMonsters(RoomType type)
    {
        ShouldSpawnMonsters = (type == RoomType.Large || type == RoomType.Medium || type == RoomType.Small);
    }

    public void SetChanceToSpawn(RoomType type)
    {
        switch (type)
        {
            default:
                ChanceToSpawn = 0;
                break;
            case RoomType.Large:
                ChanceToSpawn = 0.5;
                break;
            case RoomType.Medium:
                ChanceToSpawn = 0.25;
                break;
            case RoomType.Small:
                ChanceToSpawn = 0.1;
                break;
        }
    }

   

   


    #region MonsterManagement

    private int maxNumberOfActiveMonsters;
    public List<GameObject> monsters = new List<GameObject>();
    public Spawner spawner;
    private List<Tuple<GameObject, Vector3>> activeMonsters = new List<Tuple<GameObject, Vector3>>();
    public List<Tuple<GameObject, Vector3>> ActiveMonsters
    {
        get
        {
            return activeMonsters;
        }
    }
    public Collider2D FloorCollider;
   
    //EnemyBase Spawn
    public void RoomRandomSpawnSetUp()
    {
        if (!ShouldSpawnMonsters)
            return;

        var currentDifficulty = 0;
        spawner = new Spawner(monsters);

        while (currentDifficulty < MaxDifficulty)
        {
            int index = Wanderer.Utils.Utils.RandomObjectInCollection(monsters.Count);

            var position = RandomPointInBounds(FloorCollider.bounds, 1f);

            if (!IsPointWithinCollider(FloorCollider, position))
            {
                continue;
            }

            if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
            {
                continue;
            }
            var monsterScr = monsters[index].GetComponent<IMonster>();
            var monsterDifficulty = monsterScr.Datas.Difficulty;

            if (currentDifficulty + monsterDifficulty > MaxDifficulty)
                continue;

            currentDifficulty += monsterDifficulty;
            activeMonsters.Add(Tuple.Create(monsters[index], position));

        }
        maxNumberOfActiveMonsters = activeMonsters.Count();

    }

    #endregion

    public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
    {
        return collider.OverlapPoint(point);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
    {
        return new Vector3(
           UnityEngine.Random.Range(bounds.min.x + margin, bounds.max.x - margin),
            UnityEngine.Random.Range(bounds.min.y + margin, bounds.max.y - margin),
            UnityEngine.Random.Range(bounds.min.z + margin, bounds.max.z - margin)
        );
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
