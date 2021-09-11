using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
	int poolKey;
	Queue<ObjectInstance> objectInstances;

	public Pool(int _poolKey, Queue<ObjectInstance> _objectInstances)
    {
		poolKey = _poolKey;
		objectInstances = _objectInstances;
    }
}
public class PoolManager : Singleton<PoolManager>
{
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();
	public Pool CreatePool(GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID();
		Queue<ObjectInstance> objectInstances = new Queue<ObjectInstance>();

		if (!poolDictionary.ContainsKey(poolKey))
		{
			poolDictionary.Add(poolKey, objectInstances);

			GameObject poolHolder = new GameObject(prefab.name + " pool");
			poolHolder.transform.parent = transform;

			for (int i = 0; i < poolSize; i++)
			{
				ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
				poolDictionary[poolKey].Enqueue(newObject);
				newObject.SetParent(poolHolder.transform);
			}
		}

		Pool pool = new Pool(poolKey, objectInstances);

		return pool;
	}

	public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int poolKey = prefab.GetInstanceID();

		if (poolDictionary.ContainsKey(poolKey))
		{
			ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
			poolDictionary[poolKey].Enqueue(objectToReuse);

			objectToReuse.Reuse(position, rotation);
			return objectToReuse.gameObject;
		}
		return null;
	}
}

public class ObjectInstance
{

	public GameObject gameObject;
	Transform transform;

	bool hasPoolObjectComponent;
	IPoolObject IPoolObject;

	public ObjectInstance(GameObject objectInstance)
	{
		gameObject = objectInstance;
		transform = gameObject.transform;
		gameObject.SetActive(false);

		if (gameObject.GetComponent<IPoolObject>() != null)
		{
			hasPoolObjectComponent = true;
			IPoolObject = gameObject.GetComponent<IPoolObject>();
		}
	}

	public void Reuse(Vector3 position, Quaternion rotation)
	{
		if (gameObject.activeSelf)
			gameObject.SetActive(false);

		gameObject.SetActive(true);
		transform.position = position;
		transform.rotation = rotation;

		if (hasPoolObjectComponent)
		{
			IPoolObject.OnObjectReuse();
		}
	}

	public void SetParent(Transform parent)
	{
		transform.parent = parent;
	}
}
