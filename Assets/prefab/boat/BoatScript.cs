using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public Inventory inventory;
    Controller controller;
    public GameObject smallSpawn;
    public GameObject bigSpawn;
    public GameObject blackScreen;
    public SpawnPoint spawnPoint;
    public enum SpawnPoint
    {
        small,
        big
    };
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
    }
    public Items paddle;
    void OnTriggerEnter(Collider collider)
    {
        if (inventory.inventoryItems.ContainsKey(paddle) && inventory.inventoryItems[paddle] >= 2)
        {
            
            if (spawnPoint == SpawnPoint.small)
            {
                controller.isParalized = true;
                GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = smallSpawn.transform.position;
                StartCoroutine(WaitForUnparalized());         
            }
            if (spawnPoint == SpawnPoint.big)
            {
                controller.isParalized = true;
                GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = bigSpawn.transform.position;
                StartCoroutine(WaitForUnparalized());
            }
        }
        else
        {
            print("sfdsdfsdf");
            StartCoroutine(Hint.HintCoroutine("You don't have paddles", 2));
        }
    }
    IEnumerator WaitForUnparalized()
    {
        StartCoroutine(blackScreen.GetComponent<BlackScreenScript>().ScreenCoroutine(2));
        yield return new WaitForSeconds(2);
        controller.isParalized = false;
    }
}
