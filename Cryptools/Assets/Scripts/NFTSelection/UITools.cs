using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{
    [SerializeField]
    public UITool nftPrefab;

    [SerializeField]
    public RectTransform contentPanel;

    [SerializeField]
    public List<Sprite> toolBorders; 

    List<UITool> toolList = new List<UITool>();

    public event Action<int> OnNFTRequest;

    public void InitAvailableTools(NFTCollectionsSO nftCollection)
    {
        int toolSelectType = 0;
        for (int i = 0; i < nftCollection.toolList.Count; i++)
        {
            toolList.Add(Instantiate(nftPrefab, Vector3.zero, Quaternion.identity));
            toolList[i].SetData(nftCollection.toolList[i].NFT, nftCollection.toolList[i].name, (int) nftCollection.toolList[i].toolType);
            toolList[i].selectionBorder.sprite = toolBorders[(int) nftCollection.toolList[i].toolType];
            toolList[i].transform.SetParent(contentPanel);
            toolList[i].OnItemClicked += HandleItemSelection;
            if (toolList[i].type == toolSelectType && toolSelectType < 3)
            {
                nftCollection.selectedTools[toolSelectType] = nftCollection.toolList[i];
                toolList[i].Select();
                toolSelectType++;
            }
        }
        toolSelectType = 0;
        /*
        for (int i = 0; i < toolList.Count; i++)
        {
            toolList[i] = Instantiate(nftPrefab, Vector3.zero, Quaternion.identity);
            toolList[i].SetData(nftCollection.toolList[i].NFT, nftCollection.toolList[i].name);
            if (nftCollection.toolList[i].IsSelected)
            {
                toolList[i].Select();
            }
            toolList[i].transform.SetParent(contentPanel);
            toolList[i].OnItemClicked += HandleItemSelection;
        }
        
        for (int i = 0; i < toolList.Count; i++)
        {
            toolList[i] = Instantiate(toolList[i], Vector3.zero, Quaternion.identity);
            toolList[i].transform.SetParent(contentPanel);
            toolList[i].OnItemClicked += HandleItemSelection;
        }
        */
    }

    private void HandleItemSelection(UITool data)
    {
        int index = toolList.IndexOf(data);
        OnNFTRequest(index);
        UpdateSelectedTools(data.type, index);
    }

    public void UpdateSelectedTools(int type, int index)
    {
        for (int i = 0; i < toolList.Count; i++)
        {
            if (toolList[i].type == type)
            {
                if (i == index)
                {
                    toolList[i].Select();
                    continue;
                }
                toolList[i].Deselect();
            }
        }
    }
    /*
    public int GetSelectedTool(int type)
    {
        int toolIndex = 0;
        for (int i = 0; i < toolList.Count; i++)
        {
            if (toolList[i].type == type && toolList[i].selectionBorder.isActiveAndEnabled == true)
            {
                toolIndex = i;
                break;
            }
        }
        return toolIndex;
    }
    */
}
