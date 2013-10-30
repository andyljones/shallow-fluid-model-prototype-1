/*****************NOT UNDER TEST**************/

using UnityEngine;

public class AtmosphereRenderer<TAtmosphereElement>
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    private Atmosphere<TAtmosphereElement> _atmosphere;

    private GameObject _atmosphereObject;
    private LineRenderer[] _lineRenderers;

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
        InitializeWind();
    }

    public void UpdateAtmosphereObject()
    {
        UpdateAtmosphereMesh();
        //UpdateAtmosphereRenderer();
        UpdateWind();
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

    private void InitializeWind()
    {
        _lineRenderers = new LineRenderer[_atmosphere.Elements.Length];

        foreach (var element in _atmosphere.Elements)
        {
            var lineRenderer = new GameObject("Wind Arrow").AddComponent<LineRenderer>();

            lineRenderer.material = (Material) Resources.Load("WindArrows", typeof (Material));
            lineRenderer.SetWidth(10f, 10f);
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, (element.Radius + element.Height) * element.Direction);
            _lineRenderers[element.Index] = lineRenderer;
        }
    }

    private void UpdateWind()
    {
        var globalZ = new Vector3(0, 0, 1);

        foreach (var element in _atmosphere.Elements)
        {
            var lineRenderer = _lineRenderers[element.Index];

            var localZ = element.Direction.normalized;
            var localX = Vector3.Cross(localZ, globalZ).normalized; // Points east. I think.
            var localY = Vector3.Cross(localX, localZ).normalized;

            var origin = (element.Radius + element.Height)*element.Direction;
            var endpoint = origin + 100*(localX*element.Conditions.V.x + localY*element.Conditions.V.y).normalized;
            
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, endpoint);
        }
    }

    private void UpdateAtmosphereRenderer()
    {
        var atmosphereRenderer = _atmosphereObject.GetComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material) Resources.Load("Sky", typeof (Material));
    }
}