using UnityEngine;

public interface IAtmosphereGenerator<in TSurfaceElement, out TAtmosphericElement> 
    where TSurfaceElement: IUsableSurfaceElement
    where TAtmosphericElement: IGenerableAtmosphericElement
{
    TAtmosphericElement[][] AtmosphereElements(TSurfaceElement[] surface);

    Vector3[][] BoundaryPoints(TSurfaceElement[] surface);
}