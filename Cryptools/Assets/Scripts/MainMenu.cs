using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public UITools uiTools;

    [SerializeField]
    public NFTCollectionsSO nftCollection;

    [SerializeField]
    public UISelectedTool selectedTool;

    public void Start()
    {
        uiTools.InitAvailableTools(nftCollection);
        this.uiTools.OnNFTRequest += HandleDescriptionRequest;
    }

    private void HandleDescriptionRequest(int toolIndex)
    {
        selectedTool.SetSelection(nftCollection.toolList[toolIndex].NFT, nftCollection.toolList[toolIndex].name);
        nftCollection.selectedTools[(int) nftCollection.toolList[toolIndex].toolType] = nftCollection.toolList[toolIndex];
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
