using System.Collections;
using UnityEngine;

public class TreeLooting : MonoBehaviour
{
    [SerializeField] GameObject treeDrop;
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
        if(Input.GetKeyDown(KeyCode.Mouse0) && stats.canCut && !cooldown && standartRay.hit.collider.gameObject.Equals(gameObject))
        {
            StartCoroutine(Cutting()); 
        }     
    }

    void CutTree()
    {
        Instantiate(treeDrop, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    IEnumerator Cutting()
    {
        cooldown = true;
        yield return new WaitForSeconds(1);
        hitCount++;
        if (hitCount == HP)
        {
            CutTree();
        }
        else
        {
            StartCoroutine(Hint.HintCoroutine($"{hitCount}/{HP}", 1));
            yield return new WaitForSeconds(1);
            cooldown = false;
        }   
    }
}
