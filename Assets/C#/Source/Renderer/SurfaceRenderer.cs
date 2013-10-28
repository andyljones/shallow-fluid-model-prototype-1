/*****************NOT UNDER TEST**************/

using UnityEngine;

//TODO: Break this into a surface renderer class and an atmospheric rendering class
public class SurfaceRenderer<TSurfaceElement>
    where TSurfaceElement : IRenderableSurfaceElement
{
    private Surface<TSurfaceElement> _surface;

    private GameObject _surfaceObject;

    public SurfaceRenderer(Surface<TSurfaceElement> surface)
    {
        _surface = surface;
    }

    public void SetSurfaceObject()
    {
        _surfaceObject = new GameObject("Surface");
        _surfaceObject.AddComponent<MeshFilter>();
        _surfaceObject.AddComponent<MeshRenderer>();

        UpdateSurfaceMesh();
        UpdateSurfaceRenderer();
    }

    public void UpdateSurfaceObject()
    {
        UpdateSurfaceMesh();
        UpdateSurfaceRenderer();
    }

    private void UpdateSurfaceMesh()
    {
        var helper = new MeshHelper(_surface.Vectors);

        foreach (var element in _surface.Elements)
        {
            var boundaryCentralVertex = element.VertexIndex;
            var boundary = element.Boundaries;
            var boundaryRadius = element.Radius;
            helper.SetPolygon(boundaryCentralVertex, boundary, boundaryRadius);
        }

        var mesh = _surfaceObject.GetComponent<MeshFilter>().mesh;

        mesh.vertices = helper.Vectors;
        mesh.normals = helper.Normals;
        mesh.triangles = helper.Triangles;
    }

    private void UpdateSurfaceRenderer()
    {
        var surfaceRenderer = _surfaceObject.GetComponent<MeshRenderer>();
        surfaceRenderer.material = (Material) Resources.Load("OceanWater", typeof (Material));
    }
}