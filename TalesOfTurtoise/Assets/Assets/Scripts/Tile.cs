using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /*
     Ansatz aus dem YouTube Video von Garnet 08.23. unter https://www.youtube.com/watch?v=IDKWtzTRX3Q
     Tile Objekte und ihre Eigenschaften wie NAchbarn die möglich sind
     */
    //class with tile name, position, neighbors with y,y-,x,x-,z,z-
    
    
    // or with handmade neighbors
    public GameObject[] backNeighbor;
    public GameObject[] frontNeighbor;
    public GameObject[] bottomNeighbor;
    public GameObject[] topNeighbor;
    public GameObject[] leftNeighbor;
    public GameObject[] rightNeighbor;

}
