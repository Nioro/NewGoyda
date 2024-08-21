using TMPro;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private GameObject Amount;
    [SerializeField] private TextMeshProUGUI AmountItemsText;
    Inventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>(true).GetComponent<Inventory>();
    }
    public void Inisialisation(CraftSO itemCraft)
    {
        Transform panelTransform = gameObject.transform;
        foreach (Transform child in panelTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in itemCraft.craftResources) {
          
            GameObject TextAmount = Instantiate(Amount);
            TextAmount.transform.SetParent(gameObject.transform);
            TextAmount.GetComponent<TextMeshProUGUI>().text = item.craftObjectAmount.ToString();

            GameObject TextItemType = Instantiate(Amount);
            TextItemType.transform.SetParent(gameObject.transform);
            TextItemType.GetComponent<TextMeshProUGUI>().text = item.craftObject.title;
        }
    }
    public void Craft(CraftSO itemCraft)
    {
        if (inventory.inventoryItems.Count < 9 || inventory.inventoryItems.ContainsKey(itemCraft.finalCraft))
        {
            int count = 0;
        foreach(var CraftItem in itemCraft.craftResources)
        {
                if(inventory.inventoryItems.ContainsKey(CraftItem.craftObject))
                {
                    int outer;
                    inventory.inventoryItems.TryGetValue(CraftItem.craftObject, out outer);
                    if(outer >= CraftItem.craftObjectAmount)
                    {
                        count++;
                    }
                }
        }
        if(count == itemCraft.craftResources.Count)
        {
                foreach (var CraftItem in itemCraft.craftResources)
                {
                    inventory.CraftDelete(CraftItem.craftObject, CraftItem.craftObjectAmount);
                }
                inventory.AddItem(itemCraft.finalCraft);
        }
        else
        {
                return;
        }
        }  
    }
}
