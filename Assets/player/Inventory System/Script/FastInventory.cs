using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FastInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    public void FastInventoryUpdate(Dictionary<Items, int> inventory)
    {
        int count = inventory.Count;
        for(var i = 0; i < count; i++)
        {
            foreach(var btn in inventory)
            {
                Debug.Log(buttons);
                buttons[i].GetComponent<Image>().sprite = btn.Key._sprite;
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = btn.Value.ToString();
            }
        }
    }
}
