using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public GameObject[] chunkObjects;
    public int[] spawnChance;

    public CapsuleCollider2D collider2d;
    Collider2D[] colliders;

    public int minSpawn = 0;
    public int maxSpawn = 5;

    Vector3 pointA;
    Vector3 pointB;

    //private void Start()
    //{

    //}

    void Start()
    {
        int objectCount = Random.Range(minSpawn, maxSpawn);
        int picker;

        for (int i = 0; i < objectCount; i++)
        {
            picker = Random.Range(0, chunkObjects.Length);

            int sum_of_weight = 0;

            for (int j = 0; j < chunkObjects.Length; j++)
            {
                sum_of_weight += spawnChance[j];
            }

            picker = Random.Range(0, sum_of_weight);

            for (int j = 0; j < chunkObjects.Length; j++)
            {
                if (picker < spawnChance[j])
                {
                    //Debug.Log(picker);
                    SpawnObject(chunkObjects[j]);
                    break;
                }
                picker -= spawnChance[j];
            }
        }
        //float radius = 0;
        //float heightRadius = 0;
        //bool collisionCheck = true;
        //Vector3 spawnPoint;
        //GameObject spawned;

        //int rand = Random.Range(1, 3);

        //for (int i = 0; i < rand; i++)
        //{
        //    spawnPoint = GetSpawnPoint();

        //    spawned = Instantiate(chunkObjects[0], spawnPoint, Quaternion.identity, this.transform);

        //    collider2d = spawned.GetComponent<CapsuleCollider2D>();

        //    pointA = spawned.transform.position;
        //    pointB = spawned.transform.position;

        //    int counter = 0;
        //    while (collisionCheck && counter < 50)
        //    {
        //        Debug.Log(collider2d.size.y);
        //        switch (collider2d.direction)
        //        {
        //            case CapsuleDirection2D.Vertical:
        //                radius = collider2d.size.x / 2f;
        //                heightRadius = (collider2d.size.y - collider2d.size.x) / 2f;
        //                pointA.y = pointA.y - heightRadius;
        //                pointB.y = pointA.y + heightRadius;
        //                collisionCheck = Physics.CheckCapsule(pointA, pointB, radius);
        //                break;
        //            case CapsuleDirection2D.Horizontal:
        //                radius = collider2d.size.y / 2;
        //                heightRadius = (collider2d.size.x - collider2d.size.y) / 2f;
        //                pointA.x = spawned.transform.position.x - heightRadius;
        //                pointB.x = spawned.transform.position.x + heightRadius;
        //                pointA.x += collider2d.offset.x;
        //                pointB.x += collider2d.offset.x;
        //                pointA.y += collider2d.offset.y;
        //                pointB.y += collider2d.offset.y;
        //                collisionCheck = Physics2D.OverlapCapsuleAll(spawned.transform.position, collider2d.size, CapsuleDirection2D.Horizontal, 0f);
        //                break;
        //            default:
        //                break;
        //        }

        //        //Debug.Log("R " + radius);
        //        //Debug.Log("HR " + heightRadius);
        //        //Debug.Log(pointA.x + " and " + pointB.x);
        //        Debug.Log(collisionCheck);
        //        Debug.Log(counter);
        //        spawned.transform.position = GetSpawnPoint();
        //        counter++;
        //    }
        //}
    }

    private void SpawnObject(GameObject spawn)
    {
        bool canSpawnHere = false;

        Vector2 spawnPos = Vector2.zero;

        int safety = 0;

        collider2d = spawn.GetComponent<CapsuleCollider2D>();
        
        while (!canSpawnHere)
        {
            spawnPos = GetSpawnPoint();
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
                break;

            safety ++;

            if (safety > 50)
            {
                Debug.Log("Too many attempt");
                break;
            }
        }

        safety = 0;

        Instantiate(spawn, spawnPos, Quaternion.identity, this.transform);
    }

    bool PreventSpawnOverlap(Vector2 spawnPos)
    {
        colliders = Physics2D.OverlapCapsuleAll(spawnPos, collider2d.size, collider2d.direction, 0f);

        //Debug.Log(collider2d.name + colliders.Length);

        if (colliders.Length > 0)
        {
            return false;
        }
        return true;
    }

    private Vector2 GetSpawnPoint()
    {
        return new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(-2f, 2f));
    }

    //int rand = Random.Range(0, chunkObjects.Length);
    //Instantiate(chunkObjects[rand], transform.parent.position, Quaternion.identity, this.transform);
    //}

    //private void Update()
    //{
    //    Debug.DrawLine(pointA, pointB, Color.red);
    //}
}
