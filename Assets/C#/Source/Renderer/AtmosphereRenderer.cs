/*****************NOT UNDER TEST**************/

using UnityEngine;

public class AtmosphereRenderer<TAtmosphereElement>
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    private Atmosphere<TAtmosphereElement> _atmosphere;

    private GameObject _atmosphereObject;

    public AtmosphereRenderer(Atmosphere<TAtmosphereElement> atmosphere)
    {
        _atmosphere = atmosphere;
    }

    public void SetAtmosphereObject()
    {
        _atmosphereObject = new GameObject("Atmosphere");
        _atmosphereObject.AddComponent<MeshFilter>();
        _atmosphereObject.AddComponent<MeshRenderer>();

        UpdateAtmosphereMesh();
        UpdateAtmosphereRenderer();
    }

    public void UpdateAtmosphereObject()
    {
        UpdateAtmosphereMesh();
        //UpdateAtmosphereRenderer();
    }

    private void UpdateAtmosphereMesh()
    {
        var helper = new MeshHelper(_atmosphere.Vectors);

        foreach (var element in _atmosphere.Elements)
        {
            var lowerBoundaryCentralVertex = element.CentralVertexIndices[0];
            var lowerBoundary = new[] {element.Boundaries[0]};
            var lowerBoundaryRadius = element.Radius;
            helper.SetPolygon(lowerBoundaryCentralVertex, lowerBoundary, lowerBoundaryRadius);

            var upperBoundaryCentralVertex = element.CentralVertexIndices[2];
            var upperBoundary = new[] { element.Boundaries[1] };
            var upperBoundaryRadius = element.Radius + element.Height;
            helper.SetPolygon(upperBoundaryCentralVertex, upperBoundary, upperBoundaryRadius);
        }

        var mesh = _atmosphereObject.GetComponent<MeshFilter>().mesh;
        mesh.vertices = helper.Vectors;
        mesh.normals = helper.Normals;
        mesh.triangles = helper.Triangles;
    }

    private void UpdateAtmosphereRenderer()
    {
        var atmosphereRenderer = _atmosphereObject.GetComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material) Resources.Load("Sky", typeof (Material));
    }
}