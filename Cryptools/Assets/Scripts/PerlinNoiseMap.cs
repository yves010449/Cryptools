using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoiseMap : MonoBehaviour
{
    public Dictionary<int, GameObject> tileSet;
    public Dictionary<int, GameObject> tileGroups;
    public List<string> chunks;

    [SerializeField]
    public PlayerMovement player;

    public GameObject[] chunkPrefabs;

    public int renderDistance = 3;
    public int renderOffset = 4;

    public float timeRemaining = 10;
    public float timer = 10;
    public bool timerIsRunning = false;
    Vector3 lastPos;

    //List<List<int>> noiseGrid = new List<List<int>>();
    //List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    List<int> generatedTilesX = new List<int>();
    List<int> generatedTilesY = new List<int>();

    public float magnification = 7f;
    public float xOffset = 0;
    public float yOffset = 0;
    public int randomizer = 0;

    void Start()
    {
        magnification = Random.Range(7f, 20f);

        //renderOffset = renderDistance / 2;
        chunks = new List<string>();
        xOffset = Random.Range(0 - randomizer, randomizer);
        yOffset = Random.Range(0 - randomizer, randomizer);
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
        timerIsRunning = true;

        //Debug.Log(chunks.Count);
        //Debug.Log("counter "+generationCounter);

        StartCoroutine(MapRenderer());
        
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                lastPos = player.transform.position;
                timeRemaining = timer;
            }
        }
        if (player.transform.position == lastPos)
        {
            timerIsRunning = false;
        }
        else
        {
            timerIsRunning = true;
        }
            
    }

    IEnumerator MapRenderer()
    {
        GenerateMap();
        yield return new WaitForSeconds(.5f);
        StartCoroutine(MapRenderer());
        //Debug.Log("counter " + generationCounter);
    }

    void CreateTileset()
    {
        tileSet = new Dictionary<int, GameObject>();
        for (int i = 0; i < chunkPrefabs.Length; i++)
        {
            tileSet.Add(i, chunkPrefabs[i]);
        }
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
        generationCounter++;
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
                //generatedTilesX.Add(x);
                //generatedTilesY.Add(y);
                chunks.Add(string.Format("tile_x{0}_y{1}", x, y));
                CreateTileGroups(tileId, x, y);
            }
        }
        //Debug.Log(chunks.Count);
        //Debug.Log("Children Count "+ (tileGroups[0].transform.childCount + tileGroups[1].transform.childCount));
        //Debug.Log(tileGroups.Count);
    }

    int generationCounter = 0;
    private bool CheckIfGenerated(int x, int y)
    {
        string checker = string.Format("tile_x{0}_y{1}", x, y);
        //if (chunks.ContainsKey(checker))
        //{
        //    Debug.Log(checker + " is Found!");
        //    return true;
        //}
        //Debug.Log(checker + " is not Found!");
        //return false;
        //GameObject chunk = null;
        //string checker = string.Format("tile_x{0}_y{1}", x, y);
        //for (int i = 0; i < tileGroups.Count; i++)
        //{
        //    if (tileGroups[i].GameObject. != null)
        //    {
        //        Debug.Log(checker + " is Found!");
        //        chunk = GameObject.find
        //    }
        //    else
        //    {
        //        Debug.Log(checker + " is not Found!");
        //    }
        //}
        //if (chunk == null)
        //    return false;
        //return true;
        //for (int i = 0; i < generatedTilesX.Count; i++)
        //{
        //    if (generatedTilesX[i] == x && generatedTilesY[i] == y)
        //        return true;
        //}
        //return false;
        return chunks.Contains(checker);
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
        //if (chunks.ContainsKey(tile.name))
        //{
        //    chunks.Add(tile.name, tile);
        //}
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

        /*
        if (test)
        {
            Instantiate(tileSet[0], tileGroups[0].transform).transform.localPosition = new Vector3(-1 * renderOffset, 0 * renderOffset, 0);
            test = false;
        }
        */
        
    }
}
