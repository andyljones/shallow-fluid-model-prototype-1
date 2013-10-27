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

    public void SetSurface(int centralVertexIndex, Boundary[] boundaries, float length)
    {
        SetVertex(centralVertexIndex, length);

        var boundingIndices = boundaries.SelectMany(boundary => boundary.VertexIndices);
        var distinctIndices = boundingIndices.Distinct().ToArray();

        for (int i = 0; i < distinctIndices.Length; i++)
        {
            int currentIndex = distinctIndices[i];
            int nextIndex = distinctIndices[MathMod(i+1, distinctIndices.Length)];
            
            SetVertex(currentIndex, length);
            SetTriangle(centralVertexIndex, currentIndex, nextIndex);
        }

        var lastIndex = distinctIndices[distinctIndices.Length - 1];
        SetVertex(lastIndex, length);
    }

    private void SetVertex(int vectorIndex, float length)
    {
        var currentVector = Vectors[vectorIndex];
        var newVector = length * currentVector.normalized;

        Vectors[vectorIndex] = newVector;
    }

    private void SetTriangle(int index1, int index2, int index3)
    {
        _triangles.Add(index1);
        _triangles.Add(index2);
        _triangles.Add(index3);
    }

    // This is an actual modulo operator, as opposed to the remainder operator % represents.
    private static int MathMod(int n, int m)
    {
        return ((n % m) + m) % m;
    }
}