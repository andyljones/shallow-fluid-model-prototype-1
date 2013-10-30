public class FakeSimulator : Simulator<FakeAtmosphericElement, FakeConditions>
{
    public FakeSimulator(float timestep, int maxStepsPerFrame, float g) : base(timestep, maxStepsPerFrame, g, 0.01f) {}

    public PersistantInformation GetPersistantInformation(int index)
    {
        return _persistantInformation[index];
    }
}
