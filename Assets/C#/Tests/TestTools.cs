using UnityEngine;

public static class TestTools
{
    public static bool ApproxEquals(float lhs, float rhs, float tolerance)
    {
        return Mathf.Abs(lhs - rhs) < tolerance;
    }

    public static bool ApproxEquals(Vector3 lhs, Vector3 rhs, float tolerance)
    {
        return ApproxEquals(lhs.x, rhs.x, tolerance) &&
               ApproxEquals(lhs.y, rhs.y, tolerance) &&
               ApproxEquals(lhs.z, rhs.z, tolerance);
    }

    public static bool ApproxEquals(SimpleNode lhs, SimpleNode rhs, float tolerance)
    {
        bool areEqual = lhs.Index == rhs.Index &&
                        ApproxEquals(lhs.Direction.x, rhs.Direction.x, tolerance) &&
                        ApproxEquals(lhs.Direction.y, rhs.Direction.y, tolerance) &&
                        ApproxEquals(lhs.Direction.z, rhs.Direction.z, tolerance);

        return areEqual;
    }
}