using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{

    [SerializeField]
    public RectTransform contentPanel;

    [SerializeField]
    List<UITool> toolList = new List<UITool>();

    [SerializeField]
    public UISelectedTool selectedTool;

    public Sprite sprite;

    public event Action<int> OnNFTRequested;

    public void InitAvailableTools()
    {
        for (int i = 0; i < toolList.Count; i++)
        {
            toolList[i] = Instantiate(toolList[i], Vector3.zero, Quaternion.identity);
            toolList[i].transform.SetParent(contentPanel);
            toolList[i].OnItemClicked += HandleItemSelection;
        }
    }

    private void HandleItemSelection(UITool data)
    {
        int index = toolList.IndexOf(data);
        OnNFTRequested(index);
    }
}
