using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items")]
public class Items : ScriptableObject
{
    public Sprite _sprite;
    public string title;
    public string description;
    public float heal;
    public int value;
    public float damage;
    public bool isUsable;
    public bool isHotSlotable;
    public bool isForMining;
    public bool isForCutting;
    public ItemType itemType;
    public enum ItemType
    {
        stick,
        log,
        stone,
        leaf,
        apple,
        axe,
        pickaxe,
        redstone,
        pistol,
        scrap,
    };
    public void UseItem(PlayerStats stats)
    {
        switch (itemType)
        {
            case ItemType.apple:
                if(stats.hp >= 0.8)
                {
                    stats.hp = 1;
                }
                else
                {
                    stats.hp += heal;
                }
                if(stats.hunger <= heal)
                {
                    stats.hunger = 0f;
                }
                else
                {
                    stats.hunger -= heal;
                }
                break;
            case ItemType.pistol:
                GameObject.FindGameObjectWithTag("Canvas").SetActive(false);
                GameObject.FindObjectOfType<CharacterAnimation>().Shooting();
                break;   
        }    
    }
}