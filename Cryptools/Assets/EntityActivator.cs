using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActivator : MonoBehaviour
{
    public int distanceFromPlayer = 10;
    public void Start()
    {
        //map.tileGroups[0].ge
        StartCoroutine(CheckActivation());
    }
    int coroutine = 0;
    public IEnumerator CheckActivation()
    {
        //if (map.tileGroups != null)
        //{
        //    if (map.tileGroups.Count > 0)
        //    {
        //        for (int i = 0; i < map.tileGroups.Count; i++)
        //        {
        //            foreach (Transform chunk in map.tileGroups[i].transform)
        //            {
        //                if (Vector3.Distance(map.player.transform.position, chunk.transform.position) > distanceFromPlayer)
        //                    chunk.gameObject.SetActive(false);
        //                else
        //                    chunk.gameObject.SetActive(true);
        //            }
        //        }
        //    }
        //}
        foreach (Transform enemy in this.transform)
        {
            if (Vector3.Distance(this.GetComponent<EnemySpawner>().player.transform.position, enemy.transform.position) > distanceFromPlayer)
                enemy.gameObject.SetActive(false);
            else
                enemy.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(CheckActivation());
        coroutine++;
        //Debug.Log("Coroutine Iteration " + coroutine);
    }
}
