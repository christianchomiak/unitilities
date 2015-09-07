/// <summary>
/// Tuples v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Structure that holds a certain amount of elements.
/// Similar to a Vector2/3/4 but generic.
/// 
/// Generic tuples can be created using Tuple<>, Tuple3<> and Tuple4<>.
/// However, to be able to show its contents in the Unity Inspector, a class must
/// not be generic. So, custom tuples like Tuple3I are provided to work with common
/// types of tuples. They can be copied to created new custom tuples (e.g. GameObject & Transform).
/// 
/// Also notice that each element of a tuple can have a different type than the rest.
/// 
/// This class was created based on a research that included these sources:
///     * Source 1: http://stackoverflow.com/questions/7120845/equivalent-of-tuple-net-4-for-net-framework-3-5
///     * Source 2: https://gist.github.com/michaelbartnett/5652076
/// </summary>

using System.Collections.Generic;

namespace Unitilities.Tuples
{

    #region Core

    /// <summary>
    /// Tuple class of 2 generic elements
    /// </summary>
    /// <typeparam name="T1">First element type</typeparam>
    /// <typeparam name="T2">Second element type</typeparam> 
    [System.Serializable]
    public class Tuple<T1, T2>
    {
        #region Variables

        public T1 first;
        public T2 second;

        private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;

        #endregion

        #region Constructors

        public Tuple(T1 _first, T2 _second) //originally was _internal_
        {
            first = _first;
            second = _second;
        }

        #endregion

        #region Public Functions

        public override string ToString()
        {
            return string.Format("<{0}, {1}>", first, second);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + first.GetHashCode();
            hash = hash * 23 + second.GetHashCode();
            return hash;
        }


        public override bool Equals(object obj)
        {
            var other = obj as Tuple<T1, T2>;
            if (object.ReferenceEquals(other, null))
                return false;
            else
                return Item1Comparer.Equals(first, other.first) && Item2Comparer.Equals(second, other.second);
        }

        #endregion

        #region Private Functions

        private static bool IsNull(object obj)
        {
            return object.ReferenceEquals(obj, null);
        }

        #endregion

        #region Operators

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

        #endregion
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
        #region Variables

        public T1 first;
        public T2 second;
        public T3 third;


        private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
        private static readonly IEqualityComparer<T3> Item3Comparer = EqualityComparer<T3>.Default;

        #endregion

        #region Constructors

        public Tuple3(Tuple<T1, T2> _tuple2, T3 _third)
            : this(_tuple2.first, _tuple2.second, _third)
        { }

        public Tuple3(T1 _first, T2 _second, T3 _third) //originally was _internal_
        {
            first = _first;
            second = _second;
            third = _third;
        }

        #endregion

        #region Public Functions

        public override string ToString()
        {
            return string.Format("Tuple3<{0}, {1}, {2}>", first, second, third);
        }

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

        #endregion

        #region Private Functions

        private static bool IsNull(object obj)
        {
            return object.ReferenceEquals(obj, null);
        }

        #endregion

        #region Operators

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

        #endregion

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
        #region Variables

        public T1 first;
        public T2 second;
        public T3 third;
        public T4 fourth;

        private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
        private static readonly IEqualityComparer<T3> Item3Comparer = EqualityComparer<T3>.Default;
        private static readonly IEqualityComparer<T4> Item4Comparer = EqualityComparer<T4>.Default;

        #endregion

        #region Constructors

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

        #endregion

        #region Public Functions

        public override string ToString()
        {
            return string.Format("Tuple4<{0}, {1}, {2}, {3}>", first, second, third, fourth);
        }

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

        #endregion

        #region Private Functions

        private static bool IsNull(object obj)
        {
            return object.ReferenceEquals(obj, null);
        }

        #endregion

        #region Operators

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

        #endregion
    }

    #endregion


    #region Custom Tuples

    //Removed as it's basically the same as using Vector2
    /// <summary>
    /// Tuple class of 2 float
    /// </summary>
    /// <typeparam name="First">First float</typeparam>
    /// <typeparam name="Second">Second float</typeparam>
    /*[System.Serializable]
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

        public static TupleF operator +(TupleF a, TupleF b)
        {
            return new TupleF(a.first + b.first, a.second + b.second);
        }

        public static TupleF operator -(TupleF a, TupleF b)
        {
            return new TupleF(a.first - b.first, a.second - b.second);
        }

        public static TupleF operator *(TupleF a, TupleF b)
        {
            return new TupleF(a.first * b.first, a.second * b.second);
        }

        public static TupleF operator /(TupleF a, TupleF b)
        {
            return new TupleF(a.first / b.first, a.second / b.second);
        }

        public static implicit operator UnityEngine.Vector2(TupleF t)
        {
            return new UnityEngine.Vector2(t.first, t.second);
        }
    }*/

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

        public static TupleI one
        {
            get { return new TupleI(1, 1); }
        }

        public TupleI(int a, int b)
            : base(a, b)
        {
        }

        public static TupleI operator +(TupleI a, TupleI b)
        {
            return new TupleI(a.first + b.first, a.second + b.second);
        }

        public static TupleI operator -(TupleI a, TupleI b)
        {
            return new TupleI(a.first - b.first, a.second - b.second);
        }

        public static TupleI operator *(TupleI a, TupleI b)
        {
            return new TupleI(a.first * b.first, a.second * b.second);
        }

        public static TupleI operator /(TupleI a, TupleI b)
        {
            return new TupleI(a.first / b.first, a.second / b.second);
        }

        public static implicit operator UnityEngine.Vector2(TupleI t)
        {
            return new UnityEngine.Vector2(t.first, t.second);
        }

        public static implicit operator TupleI(UnityEngine.Vector2 v)
        {
            return new TupleI((int) v.x, (int) v.y);
        }
    }

    /// <summary>
    /// Tuple class of 3 int
    /// </summary>
    /// <typeparam name="First">First int</typeparam>
    /// <typeparam name="Second">Second int</typeparam>
    /// <typeparam name="Third">Third int</typeparam>
    [System.Serializable]
    public class Tuple3I : Tuple3<int, int, int>
    {
        public static Tuple3I zero
        {
            get { return new Tuple3I(0, 0, 0); }
        }

        public static Tuple3I one
        {
            get { return new Tuple3I(1, 1, 1); }
        }
        
        public Tuple3I(int a, int b, int c)
            : base(a, b, c)
        {
        }

        public static Tuple3I operator +(Tuple3I a, Tuple3I b)
        {
            return new Tuple3I(a.first + b.first, a.second + b.second, a.third + b.third);
        }

        public static Tuple3I operator -(Tuple3I a, Tuple3I b)
        {
            return new Tuple3I(a.first - b.first, a.second - b.second, a.third - b.third);
        }

        public static Tuple3I operator *(Tuple3I a, Tuple3I b)
        {
            return new Tuple3I(a.first * b.first, a.second * b.second, a.third * b.third);
        }

        public static Tuple3I operator /(Tuple3I a, Tuple3I b)
        {
            return new Tuple3I(a.first / b.first, a.second / b.second, a.third / b.third);
        }

        public static implicit operator UnityEngine.Vector3(Tuple3I t)
        {
            return new UnityEngine.Vector3(t.first, t.second, t.third);
        }

        public static implicit operator Tuple3I(UnityEngine.Vector3 v)
        {
            return new Tuple3I((int) v.x, (int) v.y, (int) v.z);
        }
    }

    #endregion

}