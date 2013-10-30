using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public class Simulator<TAtmosphereElement, TConditions> : ISimulator<TAtmosphereElement, TConditions>
    where TAtmosphereElement : class, ISimulableAtmosphereElement
    where TConditions : struct, ISimulableConditions
{
    private float _timestep;
    private int _maxSteps;
    private Atmosphere<TAtmosphereElement> _atmosphere;
    protected TConditions[] _oldConditions; // Protected rather than private for test purposes.
    protected TConditions[] _currentConditions; // Protected rather than private for test purposes.
    protected PersistantInformation[] _persistantInformation; // Protected rather than private for test purposes.
    private float _g;

    public Simulator(float timestep, int maxStepsPerFrame, float g)
    {
        _maxSteps = maxStepsPerFrame;
        _timestep = timestep;
        _g = g;
    }

    public void InitializeSimulator(Atmosphere<TAtmosphereElement> atmosphere, TConditions initialConditions)
    {
        int numberOfElements = atmosphere.Elements.Length;
        _atmosphere = atmosphere;
        _oldConditions = Enumerable.Repeat(initialConditions, numberOfElements).ToArray();
        _currentConditions = Enumerable.Repeat(initialConditions, numberOfElements).ToArray();
        _persistantInformation = new PersistantInformation[numberOfElements];

        var initializer = new SimulationInitializer<TAtmosphereElement, TConditions>(atmosphere.Elements, initialConditions);
        initializer.InitializeAtmosphericElements();
        _persistantInformation = initializer.Information;
    }

    public void StepSimulation()
    {
        for (int step = 0; step < _maxSteps; step++)
        {
            foreach (var element in _atmosphere.Elements)
            {
                StepSimulation(element);
            }

            _currentConditions.CopyTo(_oldConditions, 0);
        }

        foreach (var element in _atmosphere.Elements)
        {
            element.Conditions = _oldConditions[element.Index];
        }
    }

    //TODO: Needs to be tested.
    private void StepSimulation(TAtmosphereElement element)
    {
        var F = new Vector3(0, 0, 0);

        var oldConditions = _oldConditions[element.Index];
        var information = _persistantInformation[element.Index];
        var validBoundaries = element.Boundaries.Where(boundary => boundary.NeighboursIndex != -1).ToArray();

        for (int i = 0; i < validBoundaries.Length; i++)
        {
            var boundary = validBoundaries[i];

            F -= FluxThroughFace(boundary, oldConditions, information.NeighbourVectors[i].normalized);
            
        }

        var f = element.Direction.normalized.z*0.001f;

        var dhdt = F[0];
        var dudt = F[1];
        var dvdt = F[2];

        var h = oldConditions.h + dhdt*_timestep;
        var u = oldConditions.V.x + (dudt + f*oldConditions.V.y)*_timestep;
        var v = oldConditions.V.y + (dvdt - f*oldConditions.V.x)*_timestep;

        _currentConditions[element.Index] = new TConditions { h = h, V = new Vector3(u, v, 0) };

        if (element.Index == 40) Debug.Log(h + "," + u + "," + v);
    }

    private Vector3 FluxThroughFace(Boundary boundary, ISimulableConditions conditions, Vector3 neighbourDirection)
    {
        var neighbourConditions = _oldConditions[boundary.NeighboursIndex];

        var hFace = (neighbourConditions.h - conditions.h) / 2;
        var VFace = (conditions.V + neighbourConditions.V)/2;

        var hFlux = Vector3.Dot(VFace, neighbourDirection)*hFace;
        var uFlux = -_g*hFace*neighbourDirection.x/2;
        var vFlux = -_g*hFace*neighbourDirection.y/2;

        return new Vector3(hFlux, uFlux, vFlux);
    }

    private ISimulableConditions GetConditions(Boundary boundary)
    {
        return _oldConditions[_atmosphere.Elements[boundary.NeighboursIndex].Index];
    }


    public void Update()
    {
        var oldConditions = _oldConditions; // Local copy to prevent torn reads.

        foreach (var element in _atmosphere.Elements)
        {
            element.Conditions = oldConditions[element.Index];
        }
    }

    private float[] ConvertVector3(Vector3 vector)
    {
        return new [] {vector.x, vector.y, vector.z};
    }
}
