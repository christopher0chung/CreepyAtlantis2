using UnityEngine;
using System.Collections;
using System;

public class waterDeformDemo : MonoBehaviour {

	MeshFilter mf; // used by Unity as a "model picker"
	public Vector3[] unmodifiedMesh;
	public float amplitude = .25f;
	public float frequency = 1;
	// Use this for initialization
	void Start () {
		mf = GetComponent<MeshFilter>();

		// optimizes a bit
		mf.mesh.MarkDynamic();

		// save a copy before distorting it
		unmodifiedMesh = mf.mesh.vertices.Clone() as Vector3[];

        Vector3[] vertices = unmodifiedMesh.Clone() as Vector3[];

        System.Array.Sort(vertices, Vector3Compare);

    }

    private int Vector3Compare(Vector3 value1, Vector3 value2)
    {
        if (value1.z < value2.z)
        {
            return -1;
        }
        else if (value1.z == value2.z)
        {
            if (value1.x < value2.x)
            {
                return -1;
            }
            else if (value1.x == value2.x)
            {
                if (value1.y < value2.y)
                {
                    return -1;
                }
                else if (value1.y == value2.y)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }

    // Update is called once per frame
    void Update () {
		// Start with blank copy
		Vector3[] vertices = unmodifiedMesh.Clone() as Vector3[];

		for(int i = 0; i < vertices.Length; i++){
			vertices[i] += Vector3.up * (Mathf.Sin(Time.time * frequency + i) * amplitude);
		}

		// put the vertices back into the mesh
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateNormals();

		// Determines if this is within culling plane
		mf.mesh.RecalculateBounds();

		// put the new mesh into the MeshCollider
		GetComponent<MeshCollider>().sharedMesh = mf.mesh;
	}
}
