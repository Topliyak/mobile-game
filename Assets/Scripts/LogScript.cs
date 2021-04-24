using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    Mesh mesh;
    int i = 0;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        print(mesh.vertices[i]);
        i = (i + 1) % mesh.vertices.Length;
    }
}
