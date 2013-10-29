public interface ISimulator<TAtmosphereElement, TConditions>
    where TAtmosphereElement : ISimulableAtmosphereElement
    where TConditions : ISimulableConditions
{
    void InitializeSimulator(Atmosphere<TAtmosphereElement> atmosphere, TConditions initialConditions);

    void Update();
}