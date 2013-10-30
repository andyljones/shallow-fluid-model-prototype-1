using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

    private const int NumberOfLatitudes = 100;
    private const int NumberOfLongitudes = 50;
    private const float Radius = 6000f;
    private const float Height = 100f;

    private const float Timestep = 10f;
    private const int MaxStepsPerFrame = 1;
    private const float g = 0.01f;
    private const float angularVelocity = 0.001f;

    private PlanetRenderer<SurfaceElement, AtmosphericElement> _planetRenderer;
    private Simulator<AtmosphericElement, Conditions> _simulation;

    void Start ()
	{
	    var gridGen = new GridGenerator<GridElement>(NumberOfLatitudes, NumberOfLongitudes);
	    var surfaceGen = new SurfaceGenerator<GridElement, SurfaceElement>(Radius);
	    var atmosphereGen = new AtmosphereGenerator<SurfaceElement, AtmosphericElement>(Height);

	    var grid = gridGen.Grid();
	    var surface = surfaceGen.Surface(grid);
	    var atmosphere = atmosphereGen.Atmosphere(surface);

        _simulation = new Simulator<AtmosphericElement, Conditions>(Timestep, MaxStepsPerFrame, g, angularVelocity);
        _simulation.InitializeSimulator(atmosphere, new Conditions() { h = 10, V = new Vector3(0.01f, 0, 0) });

        _planetRenderer = new PlanetRenderer<SurfaceElement, AtmosphericElement>();
        _planetRenderer.InitializeScene(surface, atmosphere);
	}
	
	// Update is called once per frame
	void Update () {
        _simulation.StepSimulation();
	    _planetRenderer.UpdateScene();
	}
}
