using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveFunctionT : MonoBehaviour
{
    public int dimensions;
    public TileT[] tileObjects;
    public List<CellT> gridComponents;
    public CellT cellObj;

    int iterations = 0;

    public GameObject gridComponent;

    void Awake()
    {
        gridComponents = new List<CellT>();
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int z = 0; z < dimensions; z++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                CellT newCell = Instantiate(cellObj, new Vector3(x,0, z), Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                gridComponents.Add(newCell);
                //set newCell as child of grid component
                newCell.transform.parent = gridComponent.transform;
            }
        }

        StartCoroutine(CheckEntropy());
    }


    IEnumerator CheckEntropy()
    {
        List<CellT> tempGrid = new List<CellT>(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<CellT> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        CellT cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        TileT selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
        cellToCollapse.tileOptions = new TileT[] { selectedTile };

        TileT foundTile = cellToCollapse.tileOptions[0];

        TileT placedLevelTile = Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);
        //set placed LevelTile as child of this level component
        placedLevelTile.transform.parent = this.transform;

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        List<CellT> newGenerationCell = new List<CellT>(gridComponents);

        for (int z = 0; z < dimensions; z++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                var index = x + z * dimensions;
                if (gridComponents[index].collapsed)
                {
                    Debug.Log("called");
                    newGenerationCell[index] = gridComponents[index];
                }
                else
                {
                    List<TileT> options = new List<TileT>();
                    foreach (TileT t in tileObjects)
                    {
                        options.Add(t);
                    }

                    //update above wird zu rechts
                    if (z > 0)
                    {
                        CellT right = gridComponents[x + (z - 1) * dimensions];
                        List<TileT> validOptions = new List<TileT>();

                        foreach (TileT possibleOptions in right.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].rightNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //update up back
                    if (x < dimensions - 1)
                    {
                        CellT back = gridComponents[x + 1 + z * dimensions];
                        List<TileT> validOptions = new List<TileT>();

                        foreach (TileT possibleOptions in back.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].backNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look down wird zu links
                    if (z < dimensions - 1)
                    {
                        CellT left = gridComponents[x + (z + 1) * dimensions];
                        List<TileT> validOptions = new List<TileT>();

                        foreach (TileT possibleOptions in left.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look left front
                    if (x > 0)
                    {
                        CellT front = gridComponents[x - 1 + z * dimensions];
                        List<TileT> validOptions = new List<TileT>();

                        foreach (TileT possibleOptions in front.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].frontNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    TileT[] newTileList = new TileT[options.Count];

                    for (int i = 0; i < options.Count; i++)
                    {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if (iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }

    }

    void CheckValidity(List<TileT> optionList, List<TileT> validOption)
    {
        for (int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }
}
