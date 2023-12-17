using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileT : MonoBehaviour
{
    /*
    Script aus dem YouTube Video von Garnet 08.23. unter https://www.youtube.com/watch?v=IDKWtzTRX3Q
    Tile Objekte und ihre Eigenschaften wie NAchbarn die möglich sind

    angepasst and 3D Grid auf xz Achse
    */

    public TileT[] backNeighbours;
    public TileT[] rightNeighbours;
    public TileT[] frontNeighbours;
    public TileT[] leftNeighbours;
}
