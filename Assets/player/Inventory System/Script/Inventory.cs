using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Items, int> inventoryItems = new Dictionary<Items, int>();
    [SerializeField] public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> hotSlotObjects = new List<GameObject>();
    public GameObject button;
    public GameObject pistol;
    public GameObject weapons;
    public GameObject hotButton;
    public GameObject hotInventory;
    internal GameObject holdItem;
    bool isHolding = false;
    [SerializeField] TextMeshProUGUI hint;
    private GameObject target;
    private PlayerStats playerStats;
    private Items choosenItem;
    [SerializeField] internal GameObject inventoryPanel;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        target = GameObject.FindGameObjectWithTag("target");
    }
    public void AddItem(Items item)
    {
        if(inventoryItems.Count < 10)
        {
            if (inventoryItems.ContainsKey(item))
            {
                inventoryItems[item] += 1;
            }
            else
            {
                inventoryItems.Add(item, 1);
            }     
        }
        else
        {
            return;
        }
        UpdateItems();
    }
    public void AddHotSlot()
    {
        if (choosenItem.isHotSlotable && !isHolding)
        {
            foreach(var i in hotSlotObjects)
            {
                if(i.GetComponent<Item_>().item.isHotSlotable && i.GetComponent<Item_>().item.title == choosenItem.title)
                {
                    holdItem = i;
                    holdItem.SetActive(true);
                    break;
                }
            }
            GameObject btn = Instantiate(hotButton);
            btn.transform.SetParent(hotInventory.transform);
            btn.GetComponent<Image>().sprite = choosenItem._sprite;
            btn.GetComponent<Button>().onClick.AddListener(() => { DeleteHotSlot(); });
            isHolding = true;
            playerStats.canMine = holdItem.GetComponent<Item_>().item.isForMining;
            playerStats.canCut = holdItem.GetComponent<Item_>().item.isForCutting;
        }
    }
    public void DeleteHotSlot()
    {
        foreach (Transform child in hotInventory.transform)
        {
            Destroy(child.gameObject);
        }
        isHolding = false;
        if (holdItem != null)
        {
            holdItem.SetActive(false);
        }
        playerStats.canMine = false;
        playerStats.canCut = false;
    }
    public void DropItem()
    {
        foreach (GameObject i in gameObjects)
        {
            if (choosenItem.title == i.GetComponent<Item_>().item.title)
            {
                Instantiate(i, target.transform.position, i.transform.rotation);
            }
        }
        print("point");
        DeleteItem();
        UpdateItems();  
    }
    public void DeleteItem()
    {
        int itemCount = inventoryItems[choosenItem];
        if (itemCount == 1)
        {
            if (choosenItem.isHotSlotable)
            {
                DeleteHotSlot();
            }
            inventoryItems.Remove(choosenItem);
            inventoryPanel.SetActive(false);
            choosenItem = null;
        }
        else
        {
            inventoryItems[choosenItem] = itemCount - 1;
        }
        UpdateItems();
    }
    public void Clear()
    {
        inventoryItems.Clear();
        UpdateItems();
    }
    public void CraftDelete(Items item, int amount)
    {
        int itemCount = inventoryItems[item];
        if (itemCount == amount)
        {
            inventoryItems.Remove(item);
        }
        else
        {
            inventoryItems[item] = itemCount - amount;
        }
        UpdateItems();
    }
    public void UpdateItems()
    {
        Transform panelTransform = gameObject.transform;
        foreach (Transform child in panelTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<Items, int> entry in inventoryItems)
        {
            GameObject btn = Instantiate(button);
            btn.transform.SetParent(gameObject.transform);
            btn.GetComponent<Image>().sprite = entry.Key._sprite;
            btn.GetComponentInChildren<TextMeshProUGUI>().text = entry.Value.ToString();
            btn.GetComponent<Button>().onClick.AddListener(() => { ChooseItem(entry.Key); });
        }
    }
    public void UseItem()
    {
        if (choosenItem.isUsable)
        {
            if (choosenItem.title == "pistol")
            {
                weapons.SetActive(false);
                pistol.SetActive(true);
            }
            choosenItem.UseItem(playerStats);
            DeleteItem();
        }
    }
    public void ChooseItem(Items item)
    {
        choosenItem = item;
        inventoryPanel.SetActive(true);
    }
    public IEnumerator ShowHint(string text)
    {
        while (true)
        {
            hint.text = text;
            yield return new WaitForSeconds(5);
        }
    }
}