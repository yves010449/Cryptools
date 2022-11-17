using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public UITools uiTools;

    [SerializeField]
    public NFTCollectionsSO nftCollection;

    [SerializeField]
    public UISelectedTool selectedTool;

    [SerializeField]
    public GameObject pickaxe;

    [SerializeField]
    public GameObject axe;

    [SerializeField]
    public GameObject hammer;

    public void Start()
    {
        uiTools.InitAvailableTools(nftCollection);
        this.uiTools.OnNFTRequest += HandleDescriptionRequest;
        ResetSelectedTools();
    }

    public void HandleDescriptionRequest(int toolIndex)
    {
        selectedTool.SetSelection(nftCollection.toolList[toolIndex].NFT, nftCollection.toolList[toolIndex].name);
        nftCollection.selectedTools[(int) nftCollection.toolList[toolIndex].toolType] = nftCollection.toolList[toolIndex];
    }

    public void ResetSelectedTools() {
        pickaxe.GetComponent<Image>().sprite = nftCollection.selectedTools[0].NFT;
        axe.GetComponent<Image>().sprite = nftCollection.selectedTools[1].NFT;
        hammer.GetComponent<Image>().sprite = nftCollection.selectedTools[2].NFT;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
