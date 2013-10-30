public class FakeSimulator : Simulator<FakeAtmosphericElement, FakeConditions>
{
    public FakeSimulator(float timestep, int maxStepsPerFrame, float g) : base(timestep, maxStepsPerFrame, g) {}

    public PersistantInformation GetPersistantInformation(int index)
    {
        return _persistantInformation[index];
    }
}
