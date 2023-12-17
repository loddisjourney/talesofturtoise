using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellT : MonoBehaviour
{
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
