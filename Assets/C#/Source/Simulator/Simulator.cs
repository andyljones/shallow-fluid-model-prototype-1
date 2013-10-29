using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

    public Simulator(float timestep, int maxStepsPerFrame)
    {
        _maxSteps = maxStepsPerFrame;
        _timestep = timestep;
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



    public void Update()
    {
        var oldConditions = _oldConditions; // Local copy to prevent torn reads.

        foreach (var element in _atmosphere.Elements)
        {
            element.Conditions = oldConditions[element.Index];
        }
    }
}
