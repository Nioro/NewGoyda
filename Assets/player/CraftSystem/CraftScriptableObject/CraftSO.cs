using System.Collections.Generic;
using UnityEngine;

public class CraftSO : ScriptableObject
{
    
    public CraftType craftType;
    public Items finalCraft;
    public int craftAmount;
    public float craftTime;
    public List<CraftResource> craftResources;

    public enum CraftType {
        Common,
        Tools 
    }
}
[System.Serializable]
public class CraftResource
{
    public Items craftObject;
    public int craftObjectAmount;
}
