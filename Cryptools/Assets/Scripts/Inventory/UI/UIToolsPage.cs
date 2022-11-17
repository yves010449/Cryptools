using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolsPage : MonoBehaviour
{
    public NFTCollectionsSO selectedTools;

    public List<GameObject> tools;
    public List<GameplayToolUI> gameplayUItools;

    public int selectedToolIndex = 1;

    private void Awake()
    {
        SetTools();
    }

    public void SetTools()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            tools[i].GetComponent<Image>().sprite = selectedTools.selectedTools[i].NFT;
            gameplayUItools[i].SetTool(selectedTools.selectedTools[i].NFT);
        }
        SelectTool(1);
    }

    public void SelectTool(int index)
    {
        foreach (GameplayToolUI tool in gameplayUItools)
        {
            tool.DeselectTool();
        }

        selectedToolIndex = index;
        gameplayUItools[index].SelectTool();

    }


    public void OpenPage()
    {
        this.gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 2);
    }
}
