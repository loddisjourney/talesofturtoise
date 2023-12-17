using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellT : MonoBehaviour
{
    /*
    Script aus dem YouTube Video von Garnet 08.23. unter https://www.youtube.com/watch?v=IDKWtzTRX3Q
    Grid Zellen mit möglichen TileOptionen die im default alle beinhalten aus der WaveFunction Componente und jeweils angepasst werden sobal eine neue Cell belegt wird
    Sowie eines "belegt" Status
    */

    public bool collapsed;
    public TileT[] tileOptions;

    public void CreateCell(bool collapseState, TileT[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(TileT[] tiles)
    {
        tileOptions = tiles;
    }
}
