using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    /*
     * Genereirt ein dreidemensionales Grid als Basis für die WaveFunctionCollapse
     */
    [SerializeField] int width = 10; 
    [SerializeField] int length = 10;
    [SerializeField] int height = 3;
    [SerializeField] GameObject gridCell;
    private GameObject currentCell;
    public List<GameObject> gridList; // liste der grid objekte, um auf die cell infos zu kommen

    public LevelState levelState;

    public WaveFunction waveFunction;
    void Awake()
    {

        levelState = LevelState.GenerateGrid;
        //Maybe a List??
    }

    public void GenerateGrid(int width, int length, int height)
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < length; x++)
            {
                for(int z = 0; z < width; z++)
                {
                    //Create a Grid Cell at x y z position and use this as identifier + set as child of this object
                    currentCell = Instantiate(gridCell, new Vector3(x, y, z), Quaternion.identity);
                    currentCell.name = $"Cell {x} {y} {z}";
                    currentCell.transform.parent = this.transform;
                    gridList.Add(currentCell);
                    Debug.Log(gridList.Count);
                    levelState = LevelState.PlaceFirstTile;
                }
            }
        }      
    }

    private void Update()
    {
        switch (levelState)
        {
            case LevelState.GenerateGrid:
                GenerateGrid(width, length, height);
                break;
            case LevelState.PlaceFirstTile:
                waveFunction.PlaceTheFirstTile();
                break;
            case LevelState.WaveFunctionCollapse:
                break;
            case LevelState.Game:
                break;
            default:
                break;
        }
    }

    public enum LevelState
    {
        GenerateGrid,
        PlaceFirstTile,
        WaveFunctionCollapse,
        Game
    }
}
