using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayToolUI : MonoBehaviour
{
    [SerializeField]
    private Image NFT;

    [SerializeField]
    private Image border;

    private bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name != "Axe")
            DeselectTool();
    }

    public void SetTool(Sprite tool)
    {
        NFT.sprite = tool;
    }

    public void SelectTool()
    {
        border.gameObject.SetActive(true);
        isSelected = true;
    }

    public void DeselectTool()
    {
        border.gameObject.SetActive(false);
        isSelected = false;
    }
}
