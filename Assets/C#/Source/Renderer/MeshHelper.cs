using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshHelper
{
    public Vector3[] Vectors { get; private set; }
    public Vector3[] Normals { get; private set; }

    public int[] Triangles
    {
        get
        {
            return _triangleBuffer.ToArray();
        }
    }
    private List<int> _triangleBuffer; 

    public MeshHelper(Vector3[] vectors)
    {
        Vectors = vectors;
        Normals = new Vector3[vectors.Length]; 
        _triangleBuffer = new List<int>();
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
        var normalizedVector = currentVector.normalized;
        var newVector = length * normalizedVector;

        Normals[vectorIndex] = normalizedVector;
        Vectors[vectorIndex] = newVector;
    }

    private void SetTriangle(int index1, int index2, int index3)
    {
        _triangleBuffer.Add(index1);
        _triangleBuffer.Add(index2);
        _triangleBuffer.Add(index3);
    }

    // This is an actual modulo operator, as opposed to the remainder operator % represents.
    private static int MathMod(int n, int m)
    {
        return ((n % m) + m) % m;
    }
}