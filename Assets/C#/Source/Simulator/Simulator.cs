using System;
using System.Linq;
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
        foreach (var element in _atmosphere.Elements)
        {
            StepSimulation(element);
        }
    }

    private void StepSimulation(TAtmosphereElement element)
    {
        var conditions = _oldConditions[element.Index];
        var persistant = _persistantInformation[element.Index];

        float h1 = conditions.h;
        float u1 = conditions.V.x;
        float v1 = conditions.V.y;

        var numberOfNeighbours = element.Boundaries.Length;

        var diffhu = new Matrix(numberOfNeighbours, 1);
        var diffhv = new Matrix(numberOfNeighbours, 1);
        var diffhuv = new Matrix(numberOfNeighbours, 1);
        var diffhu2PlusHalfgh2 = new Matrix(numberOfNeighbours, 1);
        var diffhv2PlusHalfgh2 = new Matrix(numberOfNeighbours, 1);

        var A = new Matrix(numberOfNeighbours, 2);

        for (int i = 0; i < numberOfNeighbours; i++)
        {
            var neighbourConditions = GetConditions(element.Boundaries[i]);

            float h2 = neighbourConditions.h;
            float u2 = neighbourConditions.V.x;
            float v2 = neighbourConditions.V.y;

            diffhu[i,1] = h2*u2 - h1*u1;
            diffhv[i,1] = h2*v2 - h2*v2;
            diffhuv[i,1] = h2*u2*v2 - h2*u2*v2;
            diffhu2PlusHalfgh2[i,1] = h2*u2*u2 + 0.5*_g*h2*h2 - h1*u1*u1 + 0.5*_g*h1*h1;
            diffhv2PlusHalfgh2[i,1] = h2*v2*v2 + 0.5*_g*h2*h2 - h1*v1*v1 + 0.5*_g*h1*h1;

            A[i, 1] = persistant.NeighbourVectors[i].x;
            A[i, 2] = persistant.NeighbourVectors[i].y;
        }

        var dhu = A.Solve(diffhu);
        var dhudx = dhu[1, 1];

        var dhv = A.Solve(diffhv);
        var dhvdx = dhv[1, 1];

        var dhuv = A.Solve(diffhuv);
        var dhuvdx = dhuv[1, 1];
        var dhuvdy = dhuv[2, 1];

        var dhu2PlusHalfgh2 = A.Solve(diffhu2PlusHalfgh2);
        var dhu2PlusHalfgh2dx = dhu2PlusHalfgh2[1, 1];

        var dhv2PlusHalfgh2 = A.Solve(diffhv2PlusHalfgh2);
        var dhv2PlusHalfgh2dy = dhv2PlusHalfgh2[2, 1];

        var newh1 = (float) (h1 - dhudx - dhvdx);
        var newhu1 = (float) (u1 - dhu2PlusHalfgh2dx - dhuvdy);
        var newhv1 = (float) (v1 - dhv2PlusHalfgh2dy - dhuvdx);

        var newu1 = newhu1/newh1;
        var newv1 = newhv1/newh1;

        _currentConditions[element.Index] = new TConditions {h = newh1, V = new Vector3(newu1, newv1, 0)};
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
