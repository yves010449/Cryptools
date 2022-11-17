using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public DayNightScript clock;
    public List<GameObject> enemy;

    public float spawnerInterval = 2f;

    //public []int sideSpawnPos = {}

    Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    //private void Update()
    //{
    //    //int randomizer = Random.Range(0, 2);
    //    //Debug.Log(randomizer);
    //    if (clock.activateLights)
    //    {

    //    }
    //}

    private IEnumerator SpawnEnemy()
    {
        spawnPosition = new Vector3();
        int randomizer = Random.Range(0, 2);
        int sideRandomizer = Random.Range(0, 2);
        //Debug.Log(randomizer);

        yield return new WaitForSeconds(spawnerInterval);

        if (clock.activateLights)
        {
            switch (sideRandomizer)
            {
                case 0:
                    switch (randomizer)
                    {
                        case 0:
                            spawnPosition.x = randomizer - .5f;
                            spawnPosition.y = Random.Range(-.5f, 1.5f);
                            break;
                        case 1:
                            spawnPosition.x = randomizer + .5f;
                            spawnPosition.y = Random.Range(-.5f, 1.5f);
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (randomizer)
                    {
                        case 0:
                            spawnPosition.y = randomizer - .5f;
                            spawnPosition.x = Random.Range(-.5f, 1.5f);
                            break;
                        case 1:
                            spawnPosition.y = randomizer + .5f;
                            spawnPosition.x = Random.Range(-.5f, 1.5f);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
            spawnPosition.z = 0f;
            Instantiate(enemy[0], spawnPosition, Quaternion.identity, this.transform);
        }
        if (this.transform.childCount > 0)
        {
            foreach (Transform enemy in transform)
            {
                if (clock.activateLights == false && enemy.gameObject.activeSelf == false)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }

        StartCoroutine(SpawnEnemy());
    }
}
