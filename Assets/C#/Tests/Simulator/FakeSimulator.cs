public class FakeSimulator : Simulator<FakeAtmosphericElement, FakeConditions>
{
    public FakeSimulator(float timestep, int maxStepsPerFrame) : base(timestep, maxStepsPerFrame) {}

    public PersistantInformation GetPersistantInformation(int index)
    {
        return _information[index];
    }
}
