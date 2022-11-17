using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapPage : MonoBehaviour
{
    public void OpenPage()
    {
        this.gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 2);
    }
}
