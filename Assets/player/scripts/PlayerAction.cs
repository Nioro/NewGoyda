using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]private GameObject inventory;
    [SerializeField] private GameObject craft; 
    [SerializeField] private GameObject pause;
    //[SerializeField] private GameObject pause;
    Controller controller;
    void Start()
    {
        controller = GetComponent<Controller>();
    }
    void Update()
    {
        CheckInventory();
        CheckCraft();
        CheckPaused();
    }
    void CheckCraft()
    {
        if (!craft.activeSelf && Input.GetKeyDown(KeyCode.C) && !inventory.activeSelf && !pause.activeSelf)
        {
            InInterface();
            craft.SetActive(true);
        }
        else if ((craft.activeSelf && Input.GetKeyDown(KeyCode.C)) || (craft.activeSelf && Input.GetKeyDown(KeyCode.Escape)))
        {
            OutInterface();
            craft.SetActive(false);
        }
    }
    void CheckInventory()
    {
        if (!inventory.activeSelf && Input.GetKeyDown(KeyCode.Tab) && !craft.activeSelf && !pause.activeSelf)
        {
            InInterface();
            inventory.SetActive(true);
        }
        else if ((inventory.activeSelf && Input.GetKeyDown(KeyCode.Tab)) || (inventory.activeSelf && Input.GetKeyDown(KeyCode.Escape)))
        {
            OutInterface();
            inventory.SetActive(false);
            inventory.GetComponent<Inventory>().inventoryPanel.SetActive(false);
        }
    }
    public void CheckPaused()
    {
        if (!pause.activeSelf && Input.GetKeyDown(KeyCode.Escape) && !craft.activeSelf && !inventory.activeSelf)
        {
            InInterface();
            pause.SetActive(true);
        }
        else if (pause.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            OutInterface();
            pause.SetActive(false);
        }
    }
    public void HidePaused()
    {
        OutInterface();
        pause.SetActive(false);
    }
    void InInterface()
    {
        controller.isParalized = true;
        controller.isFreezed = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OutInterface()
    {
        controller.isParalized = false;
        controller.isFreezed = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
