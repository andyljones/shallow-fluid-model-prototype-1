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

    public void UpdateScene()
    {
        UpdateSurfaceObject();
        UpdateAtmosphereObject();
    }

    #region Surface initializatiion
    private void SetSurfaceObject()
    {
        _surfaceObject = new GameObject("Surface");
        _surfaceObject.AddComponent<MeshFilter>();
        _surfaceObject.AddComponent<MeshRenderer>();

        UpdateSurfaceMesh();
        UpdateSurfaceRenderer();
    }

    private void UpdateSurfaceObject()
    {
        UpdateSurfaceMesh();
        //UpdateSurfaceRenderer();
    }
    
    private void UpdateSurfaceMesh(bool UpdateTriangles = false)
    {
        var helper = new MeshHelper(_surface.Vectors);

        foreach (var element in _surface.Elements)
        {
            helper.SetPolygon(element.VertexIndex, element.Boundaries, element.Radius, UpdateTriangles);
        }

        var mesh = _surfaceObject.GetComponent<MeshFilter>().mesh;

        mesh.vertices = helper.Vectors;
        mesh.normals = helper.Normals;

        if (UpdateTriangles)
        {
            mesh.triangles = helper.Triangles;
        }
    }

    private void UpdateSurfaceRenderer()
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

        UpdateAtmosphereMesh(UpdateTriangles: true);
        UpdateAtmosphereRenderer();
    }

    private void UpdateAtmosphereObject()
    {
        UpdateAtmosphereMesh();
        //UpdateAtmosphereRenderer();
    }
    
    private void UpdateAtmosphereMesh(bool UpdateTriangles = false)
    {
        var helper = new MeshHelper(_atmosphere.Vectors);

        foreach (var element in _atmosphere.Elements)
        {
            helper.SetPolygon(element.CentralVertexIndices[0], new[] { element.Boundaries[0] }, element.Radius, UpdateTriangles);
            helper.SetPolygon(element.CentralVertexIndices[2], new[] { element.Boundaries[1] }, element.Radius + element.Height, UpdateTriangles);
        }

        var mesh = _atmosphereObject.GetComponent<MeshFilter>().mesh;
        mesh.vertices = helper.Vectors;
        mesh.normals = helper.Normals;

        if (UpdateTriangles)
        {
            mesh.triangles = helper.Triangles;
        }
    }

    private void UpdateAtmosphereRenderer()
    {
        var atmosphereRenderer = _atmosphereObject.GetComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material)Resources.Load("Sky", typeof(Material));
    }

    #endregion
}