using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Utils : ScriptableObject
{
    public static char GetRandomLetter()
    {
        var num = Random.Range(0, 27);
        if (num == 26) return ' ';
        return (char) (num + 'a');
    }

    public static int StringEquality(string a, string b)
    {
        if (a.Length != b.Length) return -1;
        var res = a.Where((t, i) => t == b[i]).Count();
        return res;
    }

    public static long BinarySearch(ref long[] array, long v)
    {
        long esq = 0, dir = array.Length-1, r = -1;
        while (esq <= dir)
        {
            var mid = (esq + dir) / 2;
            if (v <= array[mid])
            {
                r = mid;
                dir = mid - 1;
            }
            else  esq = mid + 1;
        }
        return r;
    }
}