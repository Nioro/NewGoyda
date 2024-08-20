using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    string savePath;
    public Inventory inventory;
    public PlayerStats stats;
    public Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
    void Start()
    {
        savePath = Application.dataPath;
        stats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        inventory = FindObjectOfType<Inventory>(true).GetComponent<Inventory>();
        foreach(var i in inventory.gameObjects)
        {
            GameObjects.Add(i.GetComponent<Item_>().item.title, i.gameObject);
        }
    }

    [System.Serializable]

    public struct ItemInfo
    {
        public string title;
        public Vector3 vector;
    }

    [System.Serializable]
    public struct InventoryItem
    {
        public string itemName;
        public int itemCount;
    }

    [System.Serializable]
    internal class GameData
    {
        public Vector3 playerTransformPosition;
        public List<ItemInfo> itemInfos = new List<ItemInfo>();
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public float playerHP;
        public float playerStamina;
        public float playerHunger;
        public float timeOfDay;
    }
    public void Save()
    {
        GameData gameData = new();
        gameData.playerTransformPosition = GameObject.FindWithTag("Player").transform.position;
        
        foreach(var i in inventory.inventoryItems)
        {
            InventoryItem inventoryItems;
            inventoryItems.itemName = i.Key.title;
            inventoryItems.itemCount = i.Value;
            gameData.inventoryItems.Add(inventoryItems);
        }
        foreach (var i in GameObject.FindGameObjectsWithTag("item"))
        {
            ItemInfo itemInfo;
            itemInfo.vector = i.transform.position;
            itemInfo.title = i.GetComponent<Item_>().item.title;
            gameData.itemInfos.Add(itemInfo);
        };
        foreach (var i in gameData.inventoryItems)
        {
            print($"{i.itemName} {i.itemCount}");
        }
        gameData.playerHP = stats.hp;
        gameData.playerStamina = stats.endurance;
        gameData.playerHunger = stats.hunger;
        gameData.timeOfDay = 0;
        File.WriteAllText(
            savePath + "/gameSave.json",
            JsonUtility.ToJson(gameData)
            );
    }
    public void Load()
    {   
        string json = File.ReadAllText(savePath + "/gameSave.json");
        GameData gameData = JsonUtility.FromJson<GameData>(json);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        stats.hp = gameData.playerHP;
        stats.endurance = gameData.playerStamina;
        stats.hunger = gameData.playerHunger;

        player.transform.position = gameData.playerTransformPosition;

        foreach (var i in GameObject.FindGameObjectsWithTag("item"))
        {
            Destroy(i);
        };
        foreach (var i in gameData.itemInfos)
        {
            Instantiate(GameObjects[i.title], i.vector, Quaternion.identity);
        }
        inventory.Clear();
        Dictionary<Items, int> aboba = new Dictionary<Items, int>();
        foreach(var i in gameData.inventoryItems)
        {
            aboba.Add(GameObjects[i.itemName].GetComponent<Item_>().item, i.itemCount);
        }
        inventory.inventoryItems = aboba;
        inventory.UpdateItems();
    }
}
