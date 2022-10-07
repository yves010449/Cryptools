using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileSet;
    Dictionary<int, GameObject> tileGroups;

    [SerializeField]
    public PlayerMovement player;

    public GameObject prefabPlains;
    public GameObject prefabForest;

    public int renderDistance = 3;
    public int renderOffset = 4;

    //List<List<int>> noiseGrid = new List<List<int>>();
    //List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    List<int> generatedTilesX = new List<int>();
    List<int> generatedTilesY = new List<int>();

    public float magnification = 7f;
    public float xOffset = 0;
    public float yOffset = 0;

    void Start()
    {
        //renderOffset = renderDistance / 2;
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            GenerateMap();
        }
    }

    void CreateTileset()
    {
        tileSet = new Dictionary<int, GameObject>();
        tileSet.Add(0, prefabPlains);
        tileSet.Add(1, prefabForest);
    }

    void CreateTileGroups()
    {
        tileGroups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefabPair in tileSet)
        {
            GameObject tileGroup = new GameObject(prefabPair.Value.name);
            tileGroup.transform.parent = gameObject.transform;
            tileGroup.transform.localPosition = new Vector3(0, 0, 0);
            tileGroups.Add(prefabPair.Key, tileGroup);
        }
    }

    bool test = true;
    void GenerateMap()
    {
        int playerPositionX = Mathf.RoundToInt(player.transform.position.x)/renderOffset;
        int playerPositionY = Mathf.RoundToInt(player.transform.position.y)/renderOffset;

        for (int x = playerPositionX - (renderDistance - 1); x < playerPositionX + renderDistance; x++)
        {
            //noiseGrid.Add(new List<int>());
            //tileGrid.Add(new List<GameObject>());
            for (int y = playerPositionY - (renderDistance - 1); y < playerPositionY + renderDistance; y++)
            {
                if (CheckIfGenerated(x, y))
                    continue;
                int tileId = GetIdUsingPerlin(x, y);
                //noiseGrid[x].Add(tileId);
                generatedTilesX.Add(x);
                generatedTilesY.Add(y);
                CreateTileGroups(tileId, x, y);
            }
        }
    }

    private bool CheckIfGenerated(int x, int y)
    {
        for (int i = 0; i < generatedTilesX.Count; i++)
        {
            if (generatedTilesX[i] == x && generatedTilesY[i] == y)
                return true;
        }
        return false;
    }

    //void UpdateMap()
    //{
    //    bool idExist = false;
    //    int tileId = GetIdUsingPerlin(0, 0);
    //    for (int i = 0; i < noiseGrid.Count; i++)
    //    {
    //        for (int j = 0; j < noiseGrid[i].Count; j++)
    //        {
    //            if (noiseGrid[i][j] == tileId)
    //                idExist = true;
    //        }
    //    }
    //    if (idExist == false)
    //    {

    //    }
    //}

    int GetIdUsingPerlin(int x, int y)
    {
        float rawPerlin = Mathf.PerlinNoise(
            (x - xOffset) / magnification,
            (y - yOffset) / magnification
            );
        float clampPerlin = Mathf.Clamp01(rawPerlin);
        float scaledPerlin = clampPerlin * tileSet.Count;
        if (scaledPerlin == tileSet.Count)
            scaledPerlin = tileSet.Count - 1;
        return Mathf.FloorToInt(scaledPerlin);
    }

    private void CreateTileGroups(int tileId, int x, int y)
    {
        //int playerPositionX = Mathf.RoundToInt(player.transform.position.x);
        //int playerPositionY = Mathf.RoundToInt(player.transform.position.y);

        GameObject tilePrefab = tileSet[tileId];
        GameObject tileGroup = tileGroups[tileId];
        GameObject tile = Instantiate(tilePrefab, tileGroup.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x * renderOffset, y * renderOffset, 0);

        //tileGrid[x].Add(tile);

        //for (int i = playerPositionX - 1; i < playerPositionX + 1; i++)
        //{
        //    for (int j = playerPositionY - 1; j < playerPositionY + 1; j++)
        //    {

        //    }
        //}

        //Debug.Log(playerPositionX + " & " + playerPositionY);

        //tile.transform.localPosition = new Vector3(x * 4, y * 4, 0);
        //tile.transform.localPosition = new Vector3((x - renderOffset) * 4, (y - renderOffset) * 4, 0);
        //tile.transform.localPosition = new Vector3((x - (renderOffset + playerPositionX)) * 4, (y - (renderOffset + playerPositionY)) * 4, 0);
        //tile.transform.localPosition = new Vector3((x - playerPositionX) * 4, (y - playerPositionY) * 4, 0);
        //tile.transform.localPosition = new Vector3((x + playerPositionX) * 4, (y + playerPositionY) * 4, 0);


        if (test)
        {
            Instantiate(tileSet[0], tileGroups[0].transform).transform.localPosition = new Vector3(-1 * renderOffset, 0 * renderOffset, 0);
            test = false;
        }
        
    }
}
