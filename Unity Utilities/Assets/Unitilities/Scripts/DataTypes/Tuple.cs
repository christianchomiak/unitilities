//Source 1: http://stackoverflow.com/questions/7120845/equivalent-of-tuple-net-4-for-net-framework-3-5
//Source 2: https://gist.github.com/michaelbartnett/5652076

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Tuple class of 2 generic elements
/// </summary>
/// <typeparam name="T1">First element type</typeparam>
/// <typeparam name="T2">Second element type</typeparam>
[System.Serializable]
public class Tuple<T1, T2>
{
    public T1 first;
    public T2 second;

    private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
    private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;

    public Tuple(T1 _first, T2 _second) //originally was _internal_
    {
        first = _first;
        second = _second;
    }


    /*public T Max<T>() where T : IComparable<T>
    {
        if (first > column)
        if (first.CompareTo(second) >= 0)
            return first;
        else
            return second;
    }*/


    public override string ToString()
    {
        return string.Format("<{0}, {1}>", first, second);
    }

    public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
    {
        if (Tuple<T1, T2>.IsNull(a) && !Tuple<T1, T2>.IsNull(b))
            return false;

        if (!Tuple<T1, T2>.IsNull(a) && Tuple<T1, T2>.IsNull(b))
            return false;

        if (Tuple<T1, T2>.IsNull(a) && Tuple<T1, T2>.IsNull(b))
            return true;

        return
            a.first.Equals(b.first) &&
            a.second.Equals(b.second);
    }

    public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
    {
        return !(a == b);
    }

    /*public override bool Equals(object o)
    {
        if (o.GetType() != typeof(Tuple<T1, T2>))
        {
            return false;
        }

        var other = (Tuple<T1, T2>)o;

        return this == other;
    }*/

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + first.GetHashCode();
        hash = hash * 23 + second.GetHashCode();
        return hash;
    }

    /*public override int GetHashCode()
    {
        var hc = 0;
        if (!object.ReferenceEquals(first, null))
            hc = Item1Comparer.GetHashCode(first);
        if (!object.ReferenceEquals(second, null))
            hc = (hc << 3) ^ Item2Comparer.GetHashCode(second);
        return hc;
    }*/

    public override bool Equals(object obj)
    {
        var other = obj as Tuple<T1, T2>;
        if (object.ReferenceEquals(other, null))
            return false;
        else
            return Item1Comparer.Equals(first, other.first) && Item2Comparer.Equals(second, other.second);
    }

    private static bool IsNull(object obj)
    {
        return object.ReferenceEquals(obj, null);
    }
}

/// <summary>
/// Tuple class of 3 generic elements
/// </summary>
/// <typeparam name="T1">First element type</typeparam>
/// <typeparam name="T2">Second element type</typeparam>
/// <typeparam name="T3">Third element type</typeparam>
[System.Serializable]
public class Tuple3<T1, T2, T3>
{
    public T1 first;
    public T2 second;
    public T3 third;


    private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
    private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
    private static readonly IEqualityComparer<T3> Item3Comparer = EqualityComparer<T3>.Default;


    public Tuple3(Tuple<T1, T2> _tuple2, T3 _third)
        : this(_tuple2.first, _tuple2.second, _third)
    { }

    public Tuple3(T1 _first, T2 _second, T3 _third) //originally was _internal_
    {
        first = _first;
        second = _second;
        third = _third;
    }

    public override string ToString()
    {
        return string.Format("Tuple3<{0}, {1}, {2}>", first, second, third);
    }

    public static bool operator ==(Tuple3<T1, T2, T3> a, Tuple3<T1, T2, T3> b)
    {
        if (Tuple3<T1, T2, T3>.IsNull(a) && !Tuple3<T1, T2, T3>.IsNull(b))
            return false;

        if (!Tuple3<T1, T2, T3>.IsNull(a) && Tuple3<T1, T2, T3>.IsNull(b))
            return false;

        if (Tuple3<T1, T2, T3>.IsNull(a) && Tuple3<T1, T2, T3>.IsNull(b))
            return true;

        return
            a.first.Equals(b.first) &&
            a.second.Equals(b.second) &&
            a.third.Equals(b.third);
    }

    public static bool operator !=(Tuple3<T1, T2, T3> a, Tuple3<T1, T2, T3> b)
    {
        return !(a == b);
    }

    /*public override bool Equals(object o)
    {
        if (o.GetType() != typeof(Tuple3<T1, T2, T3>))
        {
            return false;
        }

        var other = (Tuple3<T1, T2, T3>)o;

        return this == other;
    }*/

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + first.GetHashCode();
        hash = hash * 23 + second.GetHashCode();
        hash = hash * 23 + third.GetHashCode();
        return hash;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Tuple3<T1, T2, T3>;
        if (object.ReferenceEquals(other, null))
            return false;
        else
            return Item1Comparer.Equals(first, other.first) && Item2Comparer.Equals(second, other.second) && Item3Comparer.Equals(third, other.third);
    }

    private static bool IsNull(object obj)
    {
        return object.ReferenceEquals(obj, null);
    }
}

/// <summary>
/// Tuple class of 4 generic elements
/// </summary>
/// <typeparam name="T1">First element type</typeparam>
/// <typeparam name="T2">Second element type</typeparam>
/// <typeparam name="T3">Third element type</typeparam>
/// <typeparam name="T4">Fourth element type</typeparam>
[System.Serializable]
public class Tuple4<T1, T2, T3, T4>
{
    public T1 first;
    public T2 second;
    public T3 third;
    public T4 fourth;

    private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
    private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
    private static readonly IEqualityComparer<T3> Item3Comparer = EqualityComparer<T3>.Default;
    private static readonly IEqualityComparer<T4> Item4Comparer = EqualityComparer<T4>.Default;

    public Tuple4(Tuple<T1, T2> _tuple2, T3 _third, T4 _fourth)
        : this(_tuple2.first, _tuple2.second, _third, _fourth)
    { }

    public Tuple4(Tuple3<T1, T2, T3> _tuple3, T4 _fourth)
        : this(_tuple3.first, _tuple3.second, _tuple3.third, _fourth)
    { }

    public Tuple4(T1 _first, T2 _second, T3 _third, T4 _fourth) //originally was _internal_
    {
        first = _first;
        second = _second;
        third = _third;
        fourth = _fourth;
    }

    public override string ToString()
    {
        return string.Format("Tuple4<{0}, {1}, {2}, {3}>", first, second, third, fourth);
    }

    public static bool operator ==(Tuple4<T1, T2, T3, T4> a, Tuple4<T1, T2, T3, T4> b)
    {
        if (Tuple4<T1, T2, T3, T4>.IsNull(a) && !Tuple4<T1, T2, T3, T4>.IsNull(b))
            return false;

        if (!Tuple4<T1, T2, T3, T4>.IsNull(a) && Tuple4<T1, T2, T3, T4>.IsNull(b))
            return false;

        if (Tuple4<T1, T2, T3, T4>.IsNull(a) && Tuple4<T1, T2, T3, T4>.IsNull(b))
            return true;

        return
            a.first.Equals(b.first) &&
            a.second.Equals(b.second) &&
            a.third.Equals(b.third) &&
            a.fourth.Equals(b.fourth);
    }

    public static bool operator !=(Tuple4<T1, T2, T3, T4> a, Tuple4<T1, T2, T3, T4> b)
    {
        return !(a == b);
    }

    /*public override bool Equals(object o)
    {
        if (o.GetType() != typeof(Tuple4<T1, T2, T3, T4>))
        {
            return false;
        }

        var other = (Tuple4<T1, T2, T3, T4>)o;

        return this == other;
    }*/

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + first.GetHashCode();
        hash = hash * 23 + second.GetHashCode();
        hash = hash * 23 + third.GetHashCode();
        hash = hash * 23 + fourth.GetHashCode();
        return hash;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Tuple4<T1, T2, T3, T4>;
        if (object.ReferenceEquals(other, null))
            return false;
        else
            return Item1Comparer.Equals(first, other.first) && Item2Comparer.Equals(second, other.second) && Item3Comparer.Equals(third, other.third) && Item4Comparer.Equals(fourth, other.fourth);
    }

    private static bool IsNull(object obj)
    {
        return object.ReferenceEquals(obj, null);
    }
}



public static class Tuple
{
    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
    {
        var tuple = new Tuple<T1, T2>(first, second);
        return tuple;
    }

    public static Tuple3<T1, T2, T3> New<T1, T2, T3>(T1 first, T2 second, T3 third)
    {
        var tuple = new Tuple3<T1, T2, T3>(first, second, third);
        return tuple;
    }

    public static Tuple4<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth)
    {
        var tuple = new Tuple4<T1, T2, T3, T4>(first, second, third, fourth);
        return tuple;
    }
}


/// <summary>
/// Tuple class of 2 float
/// </summary>
/// <typeparam name="First">First float</typeparam>
/// <typeparam name="Second">Second float</typeparam>
[System.Serializable]
public class TupleF : Tuple<float, float>
{
    public static TupleF zero
    {
        get { return new TupleF(0f, 0f); }
    }

    public TupleF(float a, float b)
        : base(a, b)
    {
    }
}

/// <summary>
/// Tuple class of 2 int
/// </summary>
/// <typeparam name="First">First int</typeparam>
/// <typeparam name="Second">Second int</typeparam>
[System.Serializable]
public class TupleI : Tuple<int, int>
{
    public static TupleI zero
    {
        get { return new TupleI(0, 0); }
    }

    public TupleI(int a, int b)
        : base(a, b)
    {
    }
}

/// <summary>
/// Tuple class of 3 int
/// </summary>
/// <typeparam name="First">First float</typeparam>
/// <typeparam name="Second">Second float</typeparam>
/// <typeparam name="Third">Third int</typeparam>
[System.Serializable]
public class Tuple3I : Tuple3<int, int, int>
{
    public static Tuple3I zero
    {
        get { return new Tuple3I(0, 0, 0); }
    }

    public Tuple3I(int a, int b, int c)
        : base(a, b, c)
    {
    }
}