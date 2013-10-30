/*****************NOT UNDER TEST**************/

using System;
using UnityEngine;

public class AtmosphereRenderer<TAtmosphereElement>
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    private Atmosphere<TAtmosphereElement> _atmosphere;

    private GameObject _atmosphereObject;
    private LineRenderer[] _windRenderers;
    private LineRenderer[] _elementBoundaries;

    private float _maxWindMagnitude = 1;

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
        InitializeElementBoundaries();
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
        _windRenderers = new LineRenderer[_atmosphere.Elements.Length];
        var windRenderHolder = new GameObject("Wind Renderer Holder");
        windRenderHolder.transform.parent = GameObject.Find("Atmosphere").transform;

        foreach (var element in _atmosphere.Elements)
        {
            var lineRenderer = new GameObject("Wind Arrow").AddComponent<LineRenderer>();
            lineRenderer.transform.parent = windRenderHolder.transform;

            lineRenderer.material = (Material) Resources.Load("WindArrows", typeof (Material));
            lineRenderer.SetWidth(20f, 10f);
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, (element.Radius + element.Height) * element.Direction);
            _windRenderers[element.Index] = lineRenderer;
        }
    }

    private void UpdateWind()
    {
        var globalZ = new Vector3(0, 0, 1);

        float maxWindMagnitude = 0;
        float scaleFactor = 50f/_maxWindMagnitude;


        foreach (var element in _atmosphere.Elements)
        {
            var lineRenderer = _windRenderers[element.Index];

            var localZ = element.Direction.normalized;
            var localX = Vector3.Cross(localZ, globalZ).normalized; // Points east. I think.
            var localY = Vector3.Cross(localX, localZ).normalized;

            var origin = (element.Radius + element.Height)*element.Direction;
            var endpoint0 = origin - scaleFactor*(localX*element.Conditions.V.x + localY*element.Conditions.V.y);
            var endpoint1 = origin + scaleFactor*(localX*element.Conditions.V.x + localY*element.Conditions.V.y);
            
            lineRenderer.SetPosition(0, endpoint0);
            lineRenderer.SetPosition(1, endpoint1);

            maxWindMagnitude = Mathf.Max(maxWindMagnitude, element.Conditions.V.magnitude);
        }

        _maxWindMagnitude = maxWindMagnitude;

        Debug.Log(maxWindMagnitude);
    }


    //TODO: Refactor
    private void InitializeElementBoundaries()
    {
        _elementBoundaries = new LineRenderer[_atmosphere.Elements.Length];
        var elementBoundaryHolder = new GameObject("Element Boundary Holder");
        elementBoundaryHolder.transform.parent = GameObject.Find("Atmosphere").transform;

        foreach (var element in _atmosphere.Elements)
        {
            var lineRenderer = new GameObject("Cell Boundaries").AddComponent<LineRenderer>();
            lineRenderer.transform.parent = elementBoundaryHolder.transform;

            lineRenderer.material = (Material)Resources.Load("Boundaries", typeof(Material));
            lineRenderer.SetWidth(10f, 5f);
            lineRenderer.SetVertexCount(element.Boundaries[1].VertexIndices.Length);

            for(int i = 0; i< element.Boundaries[1].VertexIndices.Length; i++)
            {
                lineRenderer.SetPosition(i, _atmosphere.Vectors[element.Boundaries[1].VertexIndices[i]]);
            }

            _elementBoundaries[element.Index] = lineRenderer;
        }
    }

    private void UpdateAtmosphereRenderer()
    {
        var atmosphereRenderer = _atmosphereObject.GetComponent<MeshRenderer>();
        atmosphereRenderer.material = (Material) Resources.Load("Sky", typeof (Material));
    }
}