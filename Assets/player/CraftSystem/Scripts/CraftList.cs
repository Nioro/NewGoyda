using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class CraftList : MonoBehaviour
{
    [SerializeField] private List<CraftSO> crafts = new List<CraftSO>();
    [SerializeField] private GameObject button;
    private Crafting craft;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image ItemImage;

    [SerializeField] private Button CraftButton;
    public void Start()
    {
        craft = FindObjectOfType<Crafting>(true).GetComponent<Crafting>();
    }
    public void Tools()
    {
        CraftUpdate("Tools");
    }
    public void Common()
    {
        CraftUpdate("Common");
    }
    public void CraftUpdate(string type)
    {
        Transform panelTransform = gameObject.transform;
        foreach (Transform child in panelTransform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (CraftSO i in crafts)
        {
            if (i.craftType.ToString().ToLower() == type.ToLower())
            {
                GameObject btn = Instantiate(button);
                btn.transform.SetParent(gameObject.transform);
                btn.GetComponent<Image>().sprite = i.finalCraft._sprite;
                btn.GetComponent<Button>().onClick.AddListener(() => { Use(i); });
            }  
        }
    }

    public void Use(CraftSO item)
    {
        CraftButton.onClick.AddListener(() => { craft.Craft(item); });
        txt.text = item.finalCraft.title;
        itemDescription.text = item.finalCraft.description;
        ItemImage.sprite = item.finalCraft._sprite;
        craft.Inisialisation(item);
    }
    
}
