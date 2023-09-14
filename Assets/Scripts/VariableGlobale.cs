using UnityEngine;
using System.Collections.Generic;
public class VariableGlobale
{
    public static int energie;
    public static Vector2Int coordFantome;
    public static Vector2Int coordFantomeDestination;
    public static Vector2Int coordTrappe;
    public static Vector2Int coordCle;

    public static Dictionary<Vector2Int, GameObject> dicoPiege;

    public static bool cleTrouve;

    public static bool pause;
    public static bool end;
    public static int tourCount;
    public static int tourRestant;
    public static bool tourFantome;

    public static bool cartActive;

    public static int nbTourMine;

}
