using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	void Start ()
	{
	    var gridGen = new GridGenerator<GridElement>(5, 8);
	    var surfaceGen = new SurfaceGenerator<GridElement, SurfaceElement>(5f);
	    var atmosphereGen = new AtmosphereGenerator<SurfaceElement, AtmosphericElement>(0.5f);

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
