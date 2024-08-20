using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float hp;
    private float maxhp = 1;
    public float endurance;
    public float hunger;
    internal bool canMine;
    internal bool canCut;
    [SerializeField] private Slider sliderHp;
    [SerializeField] private Slider sliderEndruace;
    [SerializeField] private Slider sliderHunger;
    void Start()
    {
        hp = maxhp;
        endurance = 1f;
        hunger = 0f;
        sliderHp.value = hp;
        sliderHunger.value = hunger;
        sliderEndruace.value = endurance;
        StartCoroutine(Run(1f));
        StartCoroutine(HpEnum(1f));
        StartCoroutine(HungryHill(2f));
    }

    void Update()
    {
        sliderHp.value = hp;
        sliderHunger.value = hunger;
        sliderEndruace.value = endurance;
        
    }
    public IEnumerator Run(float tick)
    {
        while (true)
        {
            if (endurance >= 0.1 && hunger < 1 && Input.GetKey(KeyCode.LeftShift))
            {
                endurance -= 0.05f;
                hunger += 0.006f;
            }
            else if(endurance < 1 && hunger <= 0.8 && !Input.GetKey(KeyCode.LeftShift))
            {
                endurance += 0.05f;
            }
            yield return new WaitForSeconds(tick);
        }
    }
    public IEnumerator HpEnum(float tick)
    {
        while (true)
        {
            if(hp != 0 && hunger >= 0.9)
            {
                hp -= 0.05f;
            }
            if(hp < 0.01)
            {
                Death();
            }
            yield return new WaitForSeconds(tick);
        }
    }
    public IEnumerator HungryHill(float tick)
    {
        while(true)
        {
            if(hunger < 1)
            {
                hunger += 0.004f;
            }
            yield return new WaitForSeconds(tick);
        }
    }
    void Death()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("StartScene");
    }
}
