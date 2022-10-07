using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedTool : MonoBehaviour
{
    [SerializeField]
    private Image tool;

    [SerializeField]
    private TMP_Text title;

    public void Start()
    {
        ResetSelection();
    }

    public void ResetSelection()
    {
        this.tool.gameObject.SetActive(false);
        this.title.text = "";
    }

    public void SetSelection(Sprite sprite, string name)
    {
        this.tool.sprite = sprite;
        this.tool.gameObject.SetActive(true);
        this.title.text = name;
    }
}
