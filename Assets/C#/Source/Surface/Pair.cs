/// <summary>
/// Simple pair type because Unity doesn't support NET 4.0 and it's Tuple<T> type.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pair<T>
{
    public T first { get; protected set; }
    public T second { get; protected set; }

    private int hashcode;
    private bool hashcodeHasBeenCached = false;

    public Pair(T first, T second)
    {
        this.first = first;
        this.second = second;
    }

    public Pair()
    {
        this.first = default(T);
        this.second = default(T);
    }

    #region System.Object overrides
    public override string ToString()
    {
        return System.String.Format("({0}, {1})", this.first, this.second);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        Pair<T> objAsPair = obj as Pair<T>;
        if ((System.Object) objAsPair == null)
        {
            return false;
        }
        else
        {
            return Equals(objAsPair);
        }
    }

    public bool Equals(Pair<T> obj)
    {
        if (this.GetHashCode() != obj.GetHashCode())
        {
            return false;
        }
        
        if (this.first.Equals(obj.first) && this.second.Equals(obj.second))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        if (hashcodeHasBeenCached == true)
        {
            return hashcode;
        }
        else
        {
            // Fields are readonly and the constructor requires a parameter for each,
            // so null fields shouldn't be a problem. But we'll check anyway.
            if (first.Equals(null))
            {
                hashcode = second.GetHashCode();
            }
            else if (second.Equals(null))
            {
                hashcode = first.GetHashCode();
            }
            else if (first.Equals(second))
            {
                hashcode = first.GetHashCode();
            }
            else
            {
                hashcode = first.GetHashCode() ^ second.GetHashCode();
            }
            hashcodeHasBeenCached = true;
            return hashcode;
        }
    }
    #endregion
}