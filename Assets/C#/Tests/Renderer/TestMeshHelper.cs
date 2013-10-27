﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class TestMeshHelper
{
    private MeshHelper _helper;
    private Boundary[] _boundaries;

    [TestInitialize]
    public void Create_Mesh_Helper_And_Friends()
    {
        var vectors = new[]
        {
            new Vector3(3, 4, 0), 
            new Vector3(1, 0, 0), 
            new Vector3(0, 1, 0), 
            new Vector3(0, 0, 1), 
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1)
        };

        _boundaries = new[]
        {
            new Boundary {NeighboursIndex = -1, VertexIndices = new[] {0, 1, 2}},
            new Boundary {NeighboursIndex = -1, VertexIndices = new[] {2, 3, 4}},
            new Boundary {NeighboursIndex = -1, VertexIndices = new[] {4, 5, 0}}
        };

        _helper = new MeshHelper(vectors);
    }

    [TestMethod]
    public void Set_Surface_Sets_Vertices_Correctly()
    {
        _helper.SetSurface(6, _boundaries, 10f);

        var expectedVectors = new[]
        {
            new Vector3(6, 8, 0), 
            new Vector3(10, 0, 0), 
            new Vector3(0, 10, 0), 
            new Vector3(0, 0, 10), 
            new Vector3(7.071f, 7.071f, 0),
            new Vector3(0, 7.071f, 7.071f),
            new Vector3(5.7735f, 5.7735f, 5.7735f)
        };

        var actualVectors = _helper.Vectors;
        
        Assert.AreEqual(expectedVectors.Length, actualVectors.Length);
        for (int i = 0; i < expectedVectors.Length; i++)
        {
            Assert.IsTrue(TestTools.ApproxEquals(expectedVectors[i], actualVectors[i], 0.01f));
        }
    }

    [TestMethod]
    public void Set_Surface_Sets_Triangles_Correctly()
    {
        _helper.SetSurface(6, _boundaries, 10f);

        var expectedTriangles = new[]
        {
            6, 0, 1,
            6, 1, 2,
            6, 2, 3,
            6, 3, 4,
            6, 4, 5,
            6, 5, 0
        };

        var actualTriangles = _helper.Triangles;

        CollectionAssert.AreEqual(expectedTriangles, actualTriangles);
    }

    [TestMethod]
    public void Set_Surface_Sets_Normals_Correctly()
    {
        _helper.SetSurface(6, _boundaries, 10f);

        var expectedNormals = new[]
        {
            new Vector3(3, 4, 0).normalized, 
            new Vector3(1, 0, 0).normalized, 
            new Vector3(0, 1, 0).normalized, 
            new Vector3(0, 0, 1).normalized, 
            new Vector3(1, 1, 0).normalized,
            new Vector3(0, 1, 1).normalized,
            new Vector3(1, 1, 1).normalized
        };

        var actualNormals = _helper.Normals;

        Assert.AreEqual(expectedNormals.Length, actualNormals.Length);
        for (int i = 0; i < expectedNormals.Length; i++)
        {
            Assert.IsTrue(TestTools.ApproxEquals(expectedNormals[i], actualNormals[i], 0.01f));
        }
    }
}