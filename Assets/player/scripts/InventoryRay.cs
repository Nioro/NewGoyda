using TMPro;
using UnityEngine;

public class InventoryRay : MonoBehaviour
{
    [SerializeField] Transform pointer;
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI keyHint;
    private Inventory inventory;
    private RaycastHit hit;
    private RaycastHit previousHit;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>(true).GetComponent<Inventory>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(cam.transform.position, cam.transform.forward*25f, Color.red);
        try
        {  
            if (Physics.Raycast(ray, out hit, 3))
            {
                Outline outline = hit.collider.gameObject.GetComponent<Outline>();
                Outline previousOutline = previousHit.collider.gameObject.GetComponent<Outline>();
                if (outline && hit.collider.gameObject.Equals(previousHit.collider.gameObject) && hit.collider.gameObject.tag == "item")
                {
                    outline.OutlineWidth = 2;
                    keyHint.text = "E to pick up";
                    
                }
                if (!hit.collider.gameObject.Equals(previousHit.collider.gameObject) || hit.distance > 2.9f)
                {     
                    previousOutline.OutlineWidth = 0;
                    keyHint.text = "";
                }
                if (hit.collider.gameObject.GetComponent<Item_>() 
                    && Input.GetKeyDown(KeyCode.E) 
                    && (inventory.inventoryItems.Count < 9 
                    || inventory.inventoryItems.ContainsKey(hit.collider.gameObject.GetComponent<Item_>().item)))
                {
                    inventory.AddItem(hit.collider.gameObject.GetComponent<Item_>().item);
                    Destroy(hit.collider.gameObject);
                    keyHint.text = "";
                }
                previousHit = hit;
            }        
        }
        catch 
        {
            if (Physics.Raycast(ray, out previousHit, 25))
            {
                return;
            }
        }  
    }
}
