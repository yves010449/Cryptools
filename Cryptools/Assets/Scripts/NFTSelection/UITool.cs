using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITool : MonoBehaviour, IPointerClickHandler
{
    public string name;
    public int type;

    public Image selectionBorder;

    public event Action<UITool> OnItemClicked;

    public void Awake()
    {
        Deselect();
    }

    public void Deselect()
    {
        selectionBorder.enabled = false;
    }

    public void Select()
    {
        selectionBorder.enabled = true;
    }

    public void SetData(Sprite nft, string name, int type)
    {
        this.transform.Find("ToolNFT").GetComponent<Image>().sprite = nft;
        this.name = name;
        this.type = type;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked?.Invoke(this);
    }
}
