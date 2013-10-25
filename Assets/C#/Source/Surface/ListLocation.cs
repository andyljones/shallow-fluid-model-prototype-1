/// <summary>
/// Encapsulates a location in a generic list.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TList"></typeparam>
public class ListLoc<T, TList> where TList: System.Collections.Generic.IList<T>
{
    private readonly TList list;
    public int index { get; private set; } //TODO Add backing value and make readonly?

    /// <summary>
    /// Allows access to the encapsulated location 
    /// </summary>
    public T Value
    {
        get
        {
            return list[index];
        }
        set
        {
            list[index] = value;
        }
    }

    public ListLoc(TList list, int index)
    {
        this.list = list;
        this.index = index;
    }


}

/// <summary>
/// Syntactic sugar for the ListLocation
/// </summary>
/// <typeparam name="T"></typeparam>
public class ArrayLoc<T> : ListLoc<T, T[]>
{
    public ArrayLoc(T[] array, int index)
        : base(array, index)
    {
    }
}