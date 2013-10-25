using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ListLocationTests
{
    [TestMethod]
    public void CheckContentsProperty()
    {
        var list = new string[] { "a", "b", "c" };
        var listLocation = new ListLoc<string, string[]>(list, 1);

        // Check the getter works.
        Assert.AreEqual("b", listLocation.Value);

        // Check the setter works.
        listLocation.Value = "z";
        Assert.AreEqual("z", list[1]);
    }
}
