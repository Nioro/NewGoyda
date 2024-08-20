using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackScreenScript : MonoBehaviour
{   
    public GameObject blackScreen;
    void Start()
    {
        blackScreen = GameObject.FindGameObjectWithTag("screen");
    }
    internal IEnumerator ScreenCoroutine(int duration)
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(duration);
        blackScreen.SetActive(false);
    }
}
