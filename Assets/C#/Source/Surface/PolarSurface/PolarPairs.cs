/// <summary>
/// Syntactic sugar for using a Pair<T> type to store a polar-azimuthal pair
/// </summary>
/// <typeparam name="T"></typeparam>
class PolAziPair<T> : Pair<T>
{
    protected PolAziPair(T pol, T azi)
        : base(pol, azi)
    {
    }

    public T pol
    {
        get
        {
            return base.first;
        }
    }

    public T azi
    {
        get
        {
            return base.second;
        }
    }
}

/// <summary>
/// Syntactic sugar for storing polar-azimuthal coordinates in a Pair<float> type.
/// </summary>
//TODO: Normalize any polar/azimuth stored in here. Would this avoid the need for wrapping elsewhere?
class PACoord : PolAziPair<float>
{
    public PACoord(float pol, float azi)
        : base(pol, azi)
    {
    }
}

/// <summary>
/// Syntactic sugar for storing polar-azimuthal indexes in a Pair<int> type.
/// </summary>
class PAIndex : PolAziPair<int>
{
    public PAIndex(int pol, int azi)
        : base(pol, azi)
    {
    }
}