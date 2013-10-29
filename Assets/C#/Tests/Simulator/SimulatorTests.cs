using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class SimulatorTests
{
    private Atmosphere<FakeAtmosphericElement> _atmosphere;
    private FakeSimulator _simulator;
    private FakeConditions _conditions;

    [TestInitialize]
    public void Initialize_4_Longitude_North_Pole_Area()
    {
        var northPole = new FakeAtmosphericElement()
        {
            Index = 0,
            Boundaries = new[]
            {
                new Boundary {NeighboursIndex = 1, VertexIndices = new[] {8, 1, 2}},
                new Boundary {NeighboursIndex = 2, VertexIndices = new[] {2, 3, 4}},
                new Boundary {NeighboursIndex = 3, VertexIndices = new[] {4, 5, 6}},
                new Boundary {NeighboursIndex = 4, VertexIndices = new[] {6, 7, 8}}
            },
            Radius = 6000,
            Direction = new Vector3(0, 0, 1)
        };

        var element0E = new FakeAtmosphericElement()
        {
            Index = 1,
            Boundaries = new Boundary[0],
            Radius = 6000,
            Direction = new Vector3(1, 0, 0)
        };

        var element90E = new FakeAtmosphericElement()
        {
            Index = 2,
            Boundaries = new Boundary[0],
            Radius = 6000,
            Direction = new Vector3(0, 1, 0)
        };

        var element180W = new FakeAtmosphericElement()
        {
            Index = 3,
            Boundaries = new Boundary[0],
            Radius = 6000,
            Direction = new Vector3(-1, 0, 0)
        };

        var element90W = new FakeAtmosphericElement()
        {
            Index = 4,
            Boundaries = new Boundary[0],
            Radius = 6000,
            Direction = new Vector3(0, -1, 0)
        };

        var boundaryDirections = new[]
        {
            new Vector3(0, 0, 1).normalized, // North pole
            new Vector3(1, 0, 1).normalized, // 45N0E
            new Vector3(1, 1, 1).normalized, // 45N45E
            new Vector3(0, 1, 1).normalized, // 45N90E
            new Vector3(-1, 1, 1).normalized, // 45N135E
            new Vector3(-1, 0, 1).normalized, // 45N180W
            new Vector3(-1, -1, 1).normalized, // 45N135W
            new Vector3(0, -1, 1).normalized, // 45N90W
            new Vector3(1, -1, 1).normalized, // 45N45W
        };

        var atmosphereElements = new[] {northPole, element0E, element90E, element180W, element90W};

        _atmosphere = new Atmosphere<FakeAtmosphericElement>(atmosphereElements, boundaryDirections);
        _conditions = new FakeConditions {h = 10, V = new Vector3(5, -3)};
        _simulator = new FakeSimulator(0.1f, 10);

        _simulator.InitializeSimulator(_atmosphere, _conditions);
    }

    [TestMethod]
    public void Persistant_Infomation_of_0E90N_Is_Initialized_Correctly()
    {
        var expectedNeighbourVectors = new Vector3[]
        {
            new Vector3(0, -6000f, 0),
            new Vector3(-6000f, 0, 0),
            new Vector3(0, 6000f, 0),
            new Vector3(6000f, 0, 0)
        };

        var expectedInformation = new PersistantInformation
            (
            x: new Vector3(0, -1, 0),
            y: new Vector3(-1, 0, 0),
            z: new Vector3(0, 0, 1)
            ) 
            {NeighbourVectors = expectedNeighbourVectors};

        var actualInformation = _simulator.GetPersistantInformation(0);

        Assert.AreEqual(expectedInformation, actualInformation);
    }

    [TestMethod]
    public void Persistant_Infomation_of_0E0N_Is_Initialized_Correctly()
    {       
        var expectedInformation = new PersistantInformation
        (
            x: new Vector3(0, -1, 0),
            y: new Vector3(0, 0, 1),
            z: new Vector3(1, 0, 0)
        );

        var actualInformation = _simulator.GetPersistantInformation(1);

        Assert.AreEqual(expectedInformation, actualInformation);
    }

    [TestMethod]
    public void Persistant_Infomation_of_90E0N_Is_Initialized_Correctly()
    {
        var expectedInformation = new PersistantInformation
        (
            x: new Vector3(1, 0, 0),
            y: new Vector3(0, 0, 1),
            z: new Vector3(0, 1, 0)
        );

        var actualInformation = _simulator.GetPersistantInformation(2);

        Assert.AreEqual(expectedInformation, actualInformation);
    }
}