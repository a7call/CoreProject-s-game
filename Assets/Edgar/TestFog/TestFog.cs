using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFog : MonoBehaviour
{
	public GameObject m_fogOfWarPlane;
	public Transform m_player;
	public LayerMask m_fogLayer;
	public float m_radius = 5f;
	private float m_radiusSqr { get { return m_radius * m_radius; } }

	private Sprite m_mesh;
	private Vector2[] m_vertices;
	private Color m_colors;

	// Use this for initialization
	void Start()
	{
		Initialize();
		print("tes");

	}

	// Update is called once per frame
	void Update()
	{
		Ray r = new Ray(transform.position, m_player.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
		{
			for (int i = 0; i < m_vertices.Length; i++)
			{
				Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
				float dist = Vector3.SqrMagnitude(v - hit.point);
				if (dist < m_radiusSqr)
				{
					float alpha = Mathf.Min(m_colors.a, dist / m_radiusSqr);
					m_colors.a = alpha;
				}
			}
			UpdateColor();
		}
	}

	void Initialize()
	{
		m_mesh = m_fogOfWarPlane.GetComponent<SpriteRenderer>().sprite;
		m_vertices = m_mesh.vertices;
		m_colors = new Color();
		
			m_colors = Color.black;
		
		UpdateColor();
	}

	void UpdateColor()
	{
		m_fogOfWarPlane.GetComponent<SpriteRenderer>().color = m_colors;
	}
}
