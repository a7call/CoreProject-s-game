using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Leaf
{
    public float sizeX;
    public float sizeY;

    public Vector2 positon;

    public Leaf childLeaf1;
    public Leaf childLeaf2;

    public bool isAbleToChildHorizontal = true;
    public bool isAbleToChildVertical = true;

    public Leaf(float _sizeX, float _sizeY, Vector2 _position)
    {
        this.sizeX = _sizeX;
        this.sizeY = _sizeY;
        this.positon = _position;
    }

}
[Serializable]
public class Dungeon
{
    int[,] dungeon;

    public List<Leaf> leaves = new List<Leaf>();

    public int sizeX;
    public int sizeY;

    Leaf masterLeaf;

    public Dungeon(int _sizeX, int _sizeY)
    {
        this.sizeX = _sizeX;
        this.sizeY = _sizeY;
        dungeon = new int[sizeX, sizeY];
        masterLeaf = new Leaf(sizeX, sizeY, Vector2.zero);
        leaves.Add(masterLeaf);
    }

    public void DrawDungeonGizmos(Vector3 pos)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, new Vector3(sizeX, sizeY, 0));
    }

    public void DivideDungeon(List<Leaf> leaves, float minLeafSizeX, float minLeafSizeY)
    {
        List<Leaf> currentLeaves = new List<Leaf>();

        currentLeaves.AddRange(leaves);

        foreach (var leaf in leaves)
        {
            if (leaf.childLeaf1 != null)
                continue;

            if (!isAbleToChild(leaf))
                continue;

            Vector2 direction = GetRandomDirection(leaf);

            if (direction == Vector2.right)
            {
                var randomYPoint = (int)GetRandomPoint(leaf.positon.y - leaf.sizeY / 2, leaf.positon.y + leaf.sizeY / 2);

                var childLeaf1 = new Leaf(leaf.sizeX, Mathf.Abs(leaf.positon.y - leaf.sizeY / 2 - randomYPoint), new Vector2(leaf.positon.x, (leaf.positon.y - (leaf.sizeY) / 2 + randomYPoint) / 2));
                var childLeaf2 = new Leaf(leaf.sizeX, Mathf.Abs(leaf.positon.y + leaf.sizeY / 2 - randomYPoint), new Vector2(leaf.positon.x, (leaf.positon.y + leaf.sizeY / 2 + randomYPoint) / 2));

                if (!CheckLeafHorizontalSize(new List<Leaf>() { childLeaf1, childLeaf2 }, minLeafSizeY))
                {
                    continue;
                }

                leaf.childLeaf1 = childLeaf1;
                leaf.childLeaf2 = childLeaf2;

                currentLeaves.AddRange(new List<Leaf>() { childLeaf1, childLeaf2 });
            }
            else if (direction == Vector2.up)
            {
                var randomXPoint = (int)GetRandomPoint(leaf.positon.x - leaf.sizeX / 2, leaf.positon.x + leaf.sizeX / 2);
                var childLeaf1 = new Leaf(Mathf.Abs(leaf.positon.x - leaf.sizeX / 2 - randomXPoint), leaf.sizeY, new Vector2((leaf.positon.x - (leaf.sizeX) / 2 + randomXPoint) / 2, leaf.positon.y));
                var childLeaf2 = new Leaf(Mathf.Abs(leaf.positon.x + leaf.sizeX / 2 - randomXPoint), leaf.sizeY, new Vector2((leaf.positon.x + leaf.sizeX / 2 + randomXPoint) / 2, leaf.positon.y));

                if (!CheckLeafVerticalSize(new List<Leaf>() { childLeaf1, childLeaf2 }, minLeafSizeX))
                    continue;

                leaf.childLeaf1 = childLeaf1;
                leaf.childLeaf2 = childLeaf2;

                currentLeaves.AddRange(new List<Leaf>() { childLeaf1, childLeaf2 });
            }
        }
        this.leaves = currentLeaves;
    }

    bool CheckLeafHorizontalSize(List<Leaf> leavesToCheck, float minLeafSizeY)
    {
        foreach (var leaf in leavesToCheck)
        {
            if (leaf.sizeY < minLeafSizeY)
            {
                leaf.isAbleToChildVertical = false;
                return false;
            }
        }
        return true;
    }
    bool CheckLeafVerticalSize(List<Leaf> leavesToCheck, float minLeafSizeX)
    {
        foreach (var leaf in leavesToCheck)
        {
            if (leaf.sizeX < minLeafSizeX)
            {
                leaf.isAbleToChildHorizontal = false;
                return false;
            }
        }
        return true;
    }
    bool isAbleToChild(Leaf leaf)
    {
        return leaf.isAbleToChildHorizontal || leaf.isAbleToChildVertical;
    }
    private Vector2 GetRandomDirection(Leaf leaf)
    {
        Vector2 direction = Vector2.zero;

        int randomInt = (int)GetRandomPoint(0, 2);
        switch (randomInt)
        {
            case 0:
                direction = Vector2.right;
                break;
            case 1:
                direction = Vector2.up;
                break;
        }

        if (!leaf.isAbleToChildVertical)
            direction = Vector2.right;


        if (!leaf.isAbleToChildHorizontal)
            leaf.isAbleToChildHorizontal = false;

        return direction;
    }

    private float GetRandomPoint(float minInclusive, float maxExclusive)
    {
        return UnityEngine.Random.Range(minInclusive, maxExclusive);
    }
}

public class BSP : MonoBehaviour
{
    public Dungeon dungeon;
    public int numberOfIteration;
    public Vector2 minLeafSize;
    public Vector2 dungeonSize;
    public int maxLeaves;

    void Awake()
    {
        dungeon = new Dungeon((int)dungeonSize.x, (int)dungeonSize.y);
        Generate();
    }
    public void Generate()
    {
        StartCoroutine(generateCO());
    }
    private IEnumerator generateCO()
    {
        int iteration = 0;

        while (iteration <= numberOfIteration)
        {
            if (dungeon.leaves.Count >= maxLeaves)
                break;

            iteration++;
            dungeon.DivideDungeon(dungeon.leaves, minLeafSize.x, minLeafSize.y);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnDrawGizmos()
    {
        if (dungeon != null)
        {
            dungeon.DrawDungeonGizmos(transform.position);
            Gizmos.color = new Color(UnityEngine.Random.Range(0, 256), UnityEngine.Random.Range(0, 256), UnityEngine.Random.Range(0, 256));
            foreach (Leaf leaf in dungeon.leaves)
            {
                Gizmos.color = new Color(UnityEngine.Random.Range(0, 256), UnityEngine.Random.Range(0, 256), UnityEngine.Random.Range(0, 256));
                Gizmos.DrawWireCube(leaf.positon, new Vector3(leaf.sizeX, leaf.sizeY, 0));
            }

        }


    }
}
