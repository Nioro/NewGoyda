using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAnimation : MonoBehaviour
{
    internal Animator animator;
    private Inventory inventory;
    public Sound shoot;
    private Controller controller;
    public GameObject blackScreen;
    private PlayerStats stats;
    private CharacterController characterController;
    private float horizontal;
    private bool cooldown;
    private float vertical;

    void Start()
    {
        controller = GetComponent<Controller>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        inventory = FindObjectOfType<Inventory>(true).GetComponent<Inventory>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        UpdateAnimation();
        GangnamStyle();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void UpdateAnimation()
    {
        animator.speed = 1;
        if (inventory.holdItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.holdItem.activeSelf && !cooldown && !controller.isFreezed)
            {
                print("baka");
                StartCoroutine(Attacking());
            }
        }
        if (!cooldown)
        {
            if (characterController.isGrounded)
            {     
                animator.SetBool("isJumping", false);   
            }
            else
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isAttacking", false);
            }
            if (characterController.velocity == Vector3.zero || controller.isParalized)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", true);
                animator.SetInteger("intHorizontal", (int)horizontal);
                animator.SetInteger("intVertical", (int)vertical);
                if (Input.GetKey(KeyCode.LeftShift) && stats.endurance > 0.1f)
                {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        animator.speed = Controller.crouchSpeed / Controller.walkSpeed;
                    }
                    else
                    {
                        Controller.currentSpeed = Controller.walkSpeed;
                    }
                }
            }
        }        
    }
    private void GangnamStyle()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (animator.GetBool("isGangnamStyle"))
            {
                animator.SetBool("isGangnamStyle", false);
                return;
            }
            animator.SetBool("isGangnamStyle", true);
        }
    }
    public void Shooting()
    {
        controller.isParalized = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isShooting", true);
        StartCoroutine(GoydaEnding());
    }

    IEnumerator Attacking()
    {
        cooldown = true;
        controller.isParalized = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("isAttacking", false);
        controller.isParalized = false;
        cooldown = false;
    }
    IEnumerator GoydaEnding()
    {
        yield return new WaitForSeconds(1.5f);
        shoot.Shoot();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        File.Delete(Application.dataPath + "/gameSave.json");
        SceneManager.LoadScene("StartScene");
    }
}
