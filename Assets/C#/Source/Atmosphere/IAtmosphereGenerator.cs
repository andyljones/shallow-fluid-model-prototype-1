using UnityEngine;

public interface IAtmosphereGenerator<in TSurfaceElement, out TAtmosphericElement> 
    where TSurfaceElement: IUsableSurfaceElement
    where TAtmosphericElement: IGenerableAtmosphericElement
{
    void GenerateAtmosphere(TSurfaceElement[] surfaceElements, Vector3[] surfaceVertices);
    
    TAtmosphericElement[] AtmosphereElements();

    Vector3[] AtmosphereVertices();
}