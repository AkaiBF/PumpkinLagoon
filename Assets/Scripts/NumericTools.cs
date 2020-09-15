using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumericTools
{
    public static bool InsideProbability(int probabilidad)
    {
        int rnd = Random.Range(0, 100);
        return rnd < probabilidad;
    }

    public static int Sign(int num)
    {
        if (num < 0) return -1;
        return 1;
    }
}
