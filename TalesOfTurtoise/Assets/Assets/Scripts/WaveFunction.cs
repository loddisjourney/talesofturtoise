using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LevelGrid;



public class WaveFunction : MonoBehaviour
{
    /*
    Ansatz aus dem YouTube Video von Garnet 08.23. unter https://www.youtube.com/watch?v=IDKWtzTRX3Q
    Code wurde an das 3D Grid aus LevelGrid.cs angepasst
    */

     // Verbessern
    [SerializeField] int width = 10;
    [SerializeField] int length = 10;
    [SerializeField] int height = 3;

    public GameObject[] tileObjects; // alle Tiles
    public List<Cell> gridComponents;
    public Cell cellObj;
    List<Cell> isCell;

    int interations = 0;

    public GameObject GridObj;

    public LevelGrid gridScript;

    private void Start()
    {
        gridComponents = new List<Cell>();
    }


    public void PlaceTheFirstTile()
    {
        // a random First Tile at bottom on a random Place x, z

        int randX = UnityEngine.Random.Range(0, length);
        int randZ = UnityEngine.Random.Range(0, width);
        int firstTile = 0;
         do
         {
            firstTile = UnityEngine.Random.Range(0, tileObjects.Length);
         } while (tileObjects[firstTile].name == "air");
         Instantiate(tileObjects[firstTile], new Vector3(randX, 0, randZ), Quaternion.identity);
        GameObject.Find(($"Cell {randX} 0 {randZ}")).GetComponent<Cell>().collapsed = true;
        
        gridScript.levelState = LevelState.WaveFunctionCollapse;
    }

    //check valid neighbors
    //erstelle eine liste, definiere / frage nögliche neighboirs ab / gebe cell mögliche tiles optionen => liste aus den cells
    //check neighbors 6x
    //wähle den mit niedrigsten möglichkeiten -> verursacht wenigsten fehler besetzung
    //platziere aus option random objekt und setze cell auf collapsed



}
