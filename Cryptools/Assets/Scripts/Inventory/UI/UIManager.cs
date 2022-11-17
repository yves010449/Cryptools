using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    List<GameObject> UITabs = new List<GameObject>();
    public float hitTestValue;

    void Awake()
    {
        AddTabs();
    }

    void AddTabs()
    {
        foreach (Transform tab in gameObject.transform)
        {
            if (tab.gameObject.GetComponent<Image>() != null)
            {
                tab.gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = hitTestValue;
            }
        }
    }
}
