/*****************NOT UNDER TEST**************/

using UnityEngine;

public class PlanetRenderer<TSurfaceElement, TAtmosphereElement> : IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    public void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere)
    {
        SetSurfaceObject(GenerateSurfaceMesh(surface));
        SetAtmosphereObject(GenerateAtmosphereMesh(atmosphere));
    }

    #region Surface Rendering
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
    #endregion

    #region Atmosphere Rendering
    private Mesh GenerateAtmosphereMesh(Atmosphere<TAtmosphereElement> atmosphere)
    {
        var helper = new MeshHelper(atmosphere.Vectors);

        foreach (var element in atmosphere.Elements)
        {
            helper.SetSurface(element.CentralVertexIndices[0], new[] { element.Boundaries[0] }, element.Radius);
            helper.SetSurface(element.CentralVertexIndices[2], new[] { element.Boundaries[1] }, element.Radius + element.Height);
        }

        return new Mesh { vertices = helper.Vectors, triangles = helper.Triangles, normals = helper.Normals };
    }

    private void SetAtmosphereObject(Mesh atmosphereMesh)
    {
        var atmosphereObject = new GameObject("Atmosphere");

        var atmosphereRenderer = atmosphereObject.AddComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material)Resources.Load("Sky", typeof(Material));

        var atmosphereMeshFilter = atmosphereObject.AddComponent<MeshFilter>();
        atmosphereMeshFilter.mesh = atmosphereMesh;
    }
    #endregion
}