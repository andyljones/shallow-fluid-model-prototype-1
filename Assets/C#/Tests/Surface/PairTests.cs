using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PairTests
{
    [TestMethod]
    public void EqualityTest()
    {
        var pair = new Pair<int>(1, 2);
        var samePair = new Pair<int>(1, 2);
        var subtypePair = new PAIndex(1, 2);

        Assert.IsTrue(pair.Equals(samePair));
        Assert.IsTrue(pair.Equals(subtypePair));
    }

    [TestMethod]
    public void InequalityTest()
    {
        var pair = new Pair<int>(1, 2);
        var differentPair = new Pair<int>(2, 1);
        var differentObject = new Pair<string>("a", "b");

        Assert.IsFalse(pair.Equals(differentObject));
        Assert.IsFalse(pair.Equals(differentPair));
    }

    [TestMethod]
    public void HashcodeTest()
    {
        var pair = new Pair<int>(1, 2);
        var samePair = new Pair<int>(1, 2);
        var differentPair = new Pair<int>(1, 1);

        Assert.IsTrue(pair.GetHashCode() == samePair.GetHashCode());
        Assert.IsFalse(pair.GetHashCode() == differentPair.GetHashCode());
    }
}