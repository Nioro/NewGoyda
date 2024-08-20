using System.Collections;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [SerializeField] GameObject drop;
    PlayerStats stats;
    StandartRay standartRay;
    int hitCount;
    [SerializeField] int HP = 10;
    bool cooldown = false;
    void Start()
    {   
        stats = FindObjectOfType<PlayerStats>();
        standartRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StandartRay>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && stats.canMine && standartRay.hit.collider.gameObject.Equals(gameObject) && !cooldown)
        {
            StartCoroutine(Mine());  
        }
    }

    void Loot()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    IEnumerator Mine()
    {
        cooldown = true;
        yield return new WaitForSeconds(1);
        hitCount++;
        if (hitCount == HP)
        {
            Loot();
        }
        else
        {
            StartCoroutine(Hint.HintCoroutine($"{hitCount}/{HP}", 1));
            yield return new WaitForSeconds(1);
            cooldown = false;
        }   
    }
}
