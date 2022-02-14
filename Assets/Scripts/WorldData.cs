using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WorldData
{
    private static readonly WorldData instance = new WorldData();
    private static GameObject[] hidingSpots;

    static WorldData()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
    }

    private WorldData() { }

    public static WorldData Instance
    {
        get { return instance; }
    }
  

    public GameObject[] GetHidingSpots()
    {
        return hidingSpots;
    }
}
