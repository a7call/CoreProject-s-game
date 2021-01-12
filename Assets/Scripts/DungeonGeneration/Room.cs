using UnityEngine;
[System.Serializable]
public class Room
{
	public bool down, up, left, right;
	public Vector2 gridPos;
	public int type;

	public Room(Vector2 _gridPos, int _type)
	{
		gridPos = _gridPos;
		type = _type;
	}
}
