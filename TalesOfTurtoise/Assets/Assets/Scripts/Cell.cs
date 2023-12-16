using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    /*
    Ansatz aus dem YouTube Video von Garnet 08.23. unter https://www.youtube.com/watch?v=IDKWtzTRX3Q
    Prüft ob ein Tile bereits platziert ist (collapsed)
    Update Tile Optionen, wenn Grid ein vollständiges Tile platziert hat -> default komplet besetzt mit allen Tiles
    */

    public bool collapsed;
    public Tile[] tileOptions;

    public void CreateCell(bool collapsedState, Tile[] tiles)
    {
        collapsed = collapsedState;
        tileOptions = tiles;
    }

    public void RecreateCell(Tile[] tiles)
    {
        tileOptions = tiles;
    }
}
