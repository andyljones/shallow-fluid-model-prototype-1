using UnityEngine;

public interface IAtmosphereGenerator<TSurfaceElement, TAtmosphericElement> 
    where TSurfaceElement: IUsableSurfaceElement
    where TAtmosphericElement: IGenerableAtmosphericElement
{
    Atmosphere<TAtmosphericElement> Atmosphere(Surface<TSurfaceElement> surface);
}