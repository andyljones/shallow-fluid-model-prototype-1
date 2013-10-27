/*****************NOT UNDER TEST**************/

using System;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class PlanetRenderer<TSurfaceElement, TAtmosphereElement> : IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    public void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere)
    {
        SetSurfaceObject(GenerateSurfaceMesh(surface));
        //InitializeAtmosphere(atmosphere);
    }

    private Mesh GenerateSurfaceMesh(Surface<TSurfaceElement> surface)
    {
        var helper = new MeshHelper(surface.Vectors);

        foreach (var element in surface.Elements)
        {
            helper.SetSurface(element.VertexIndex, element.Boundaries, element.Radius);
        }

        return new Mesh {vertices = helper.Vectors, triangles = helper.Triangles, normals = helper.Normals};
    }

    private void SetSurfaceObject(Mesh surfaceMesh)
    {
        var surfaceObject = new GameObject("Surface");
        
        var surfaceRenderer = surfaceObject.AddComponent<MeshRenderer>();
        surfaceRenderer.material = (Material) Resources.Load("OceanWater", typeof (Material));

        var surfaceMeshFilter = surfaceObject.AddComponent<MeshFilter>();
        surfaceMeshFilter.mesh = surfaceMesh;
    }
}