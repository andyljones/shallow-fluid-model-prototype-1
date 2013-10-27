using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshHelper
{
    public Vector3[] Vectors { get; private set; }

    public int[] Triangles
    {
        get
        {
            return _triangles.ToArray();
        }
    }
    private List<int> _triangles; 
    
    public MeshHelper(Vector3[] vectors)
    {
        Vectors = vectors;
        _triangles = new List<int>();
    }

    public void SetVertex(int vectorIndex, float length)
    {
        var currentVector = Vectors[vectorIndex];
        var newVector = length*currentVector.normalized;

        Vectors[vectorIndex] = newVector;
    }

    public void SetSurface(int centralVertexIndex, Boundary[] boundaries, float length)
    {
        SetVertex(centralVertexIndex, length);

        var boundingIndices = boundaries.SelectMany(boundary => boundary.VertexIndices);
        var distinctIndices = boundingIndices.Distinct().ToArray();

        for (int i = 0; i < distinctIndices.Length - 1; i++)
        {
            int currentIndex = distinctIndices[i];
            SetVertex(currentIndex, length);
            //int nextIndex = distinctIndices[i];
            //SetTriangle()
        }

        var lastIndex = distinctIndices[distinctIndices.Length - 1];
        SetVertex(lastIndex, length);
    }
}