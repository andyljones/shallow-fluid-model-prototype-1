/*****************NOT UNDER TEST**************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

public class PlanetRenderer<TSurfaceElement, TAtmosphereElement> : IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    private Surface<TSurfaceElement> _surface;
    private Atmosphere<TAtmosphereElement> _atmosphere;

    private GameObject _surfaceObject;
    private GameObject _atmosphereObject;

    public void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere)
    {
        _surface = surface;
        _atmosphere = atmosphere;

        SetSurfaceObject();
        SetAtmosphereObject();
    }

    #region Surface initializatiion
    private void SetSurfaceObject()
    {
        _surfaceObject = new GameObject("Surface");
        _surfaceObject.AddComponent<MeshFilter>();
        _surfaceObject.AddComponent<MeshRenderer>();

        SetSurfaceMesh();
        SetSurfaceRenderer();
    }    
    
    private void SetSurfaceMesh()
    {
        var helper = new MeshHelper(_surface.Vectors);

        foreach (var element in _surface.Elements)
        {
            helper.SetPolygon(element.VertexIndex, element.Boundaries, element.Radius, UpdateTriangles: true);
        }

        var mesh = _surfaceObject.GetComponent<MeshFilter>().mesh;

        mesh.vertices = helper.Vectors;
        mesh.triangles = helper.Triangles;
        mesh.normals = helper.Normals;
    }

    private void SetSurfaceRenderer()
    {
        var surfaceRenderer = _surfaceObject.GetComponent<MeshRenderer>();
        surfaceRenderer.material = (Material)Resources.Load("OceanWater", typeof(Material));
    }
    #endregion

    #region Atmosphere initialization
    private void SetAtmosphereObject()
    {
        _atmosphereObject = new GameObject("Atmosphere");
        _atmosphereObject.AddComponent<MeshFilter>();
        _atmosphereObject.AddComponent<MeshRenderer>();

        SetAtmosphereMesh();
        SetAtmosphereRenderer();
    }    
    
    private void SetAtmosphereMesh()
    {
        var helper = new MeshHelper(_atmosphere.Vectors);

        foreach (var element in _atmosphere.Elements)
        {
            helper.SetPolygon(element.CentralVertexIndices[0], new[] { element.Boundaries[0] }, element.Radius, UpdateTriangles: true);
            helper.SetPolygon(element.CentralVertexIndices[2], new[] { element.Boundaries[1] }, element.Radius + element.Height, UpdateTriangles: true);
        }

        var mesh = _atmosphereObject.GetComponent<MeshFilter>().mesh;
        mesh.vertices = helper.Vectors;
        mesh.triangles = helper.Triangles;
        mesh.normals = helper.Normals;
    }

    private void SetAtmosphereRenderer()
    {
        var atmosphereRenderer = _atmosphereObject.GetComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material)Resources.Load("Sky", typeof(Material));
    }

    #endregion
}