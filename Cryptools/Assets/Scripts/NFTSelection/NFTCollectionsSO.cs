using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NFTCollectionsSO : ScriptableObject
{
    [SerializeField]
    public List<Tool> toolList;

    public Tool[] selectedTools = new Tool[3];
}

[Serializable]
public struct Tool
{
    public string name;

    public Sprite NFT;

    public enum ToolType { 
        Pickaxe = 0,
        Axe = 1,
        Hammer = 2,
    }

    public ToolType toolType;
}
