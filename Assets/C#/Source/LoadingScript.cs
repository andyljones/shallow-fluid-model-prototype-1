using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

    private const int NumberOfLatitudes = 50;
    private const int NumberOfLongitudes = 50;
    private const float Radius = 6000f;
    private const float Height = 100f;

	void Start ()
	{
	    var gridGen = new GridGenerator<GridElement>(NumberOfLatitudes, NumberOfLongitudes);
	    var surfaceGen = new SurfaceGenerator<GridElement, SurfaceElement>(Radius);
	    var atmosphereGen = new AtmosphereGenerator<SurfaceElement, AtmosphericElement>(Height);

	    var grid = gridGen.Grid();
	    var surface = surfaceGen.Surface(grid);
	    var atmosphere = atmosphereGen.Atmosphere(surface);

        var planetRenderer = new PlanetRenderer<SurfaceElement, AtmosphericElement>();

        planetRenderer.InitializeScene(surface, atmosphere);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
