using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITool : MonoBehaviour, IPointerClickHandler
{
    public Sprite nft;
    public int type;

    [SerializeField]
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

    public void SetData(Sprite nft)
    {
        this.transform.Find("ToolNFT").GetComponent<Image>().sprite = nft;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked?.Invoke(this);
    }
}
