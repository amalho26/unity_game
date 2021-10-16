using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
   
    public int keyprogress;
    public Text countText; //public variable UI text for keeping track of score


    public GameObject Chest;
    public GameObject keyText;
    public GameObject Shield;
  

    public int maxHealth = 50;
    public int currentHealth;
    public HealthBarScript healthBar;
    

 

    public Animator animator;
    public int isWalkingHash;
    public int isBackwardHash;
    public int isJumpingHash;
    public int isRunningHash;
    public int isSwingingHash;
    public int isFightingHash;


    public int damage = 3;

    public bool fight = false;
    public bool swing = false;
    public bool protect = false;

    public float speed = 10f;





    //Start is called before the first frame update
    public void Start()
    {
        keyprogress = 0;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isBackwardHash = Animator.StringToHash("isBackward");
        isJumpingHash = Animator.StringToHash("isJumping");
        isRunningHash = Animator.StringToHash("isRunning");
        isSwingingHash = Animator.StringToHash("isSwinging");
        isFightingHash = Animator.StringToHash("isFighting");

    }

    //Update is called once per frame
    public void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey(KeyCode.UpArrow);
        bool isBackward = animator.GetBool("isBackward");
        bool backwardPressed = Input.GetKey(KeyCode.DownArrow);
        bool isJumping = animator.GetBool("isJumping");
        bool jumpingPressed = Input.GetKey(KeyCode.Z);
        bool isRunning = animator.GetBool("isRunning");
        bool runningPressed = Input.GetKey(KeyCode.X);

        healthBar.SetHealth(currentHealth);

        if(keyprogress == 10){

            Instantiate(Chest, new Vector3(25,0,20), Quaternion.identity);
            keyText.SetActive(true);
            keyprogress = 0; 
        }

        if(Input.GetKey(KeyCode.O)){
            if(Input.GetKey(KeyCode.P)){
                if(Input.GetKey(KeyCode.E)){
                    if(Input.GetKey(KeyCode.N)){
                        SceneManager.LoadScene("dungeon");
                    }
                }
            }

        }

        SetCountText();//update score

        Fight();
        Swing();
        
        transform.position += transform.forward * speed * Time.deltaTime;
 
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0.0f, -90.0f, 0.0f);
        }

      
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            transform.Rotate(0.0f, 90.0f, 0.0f);
        }
        
        //if player pressed forward key (or up arrow)
        if(forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if(!forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //if player pressed backward key (or down arrow)
        if(backwardPressed)
        {
            animator.SetBool(isBackwardHash, true);
        }
        if(!backwardPressed)
        {
            animator.SetBool(isBackwardHash, false);
        }

        //if player pressed the Z key 
        if(jumpingPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if(!jumpingPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        //if player pressed the x key 
        if(runningPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if(!runningPressed)
        {
            animator.SetBool(isRunningHash, false);
        }        

    }

    public virtual void Fight()
    {
    }
    public virtual void Swing()
    {
    }
    

    public void OnTriggerEnter(Collider coll)
    {
         if(coll.gameObject.CompareTag("shield"))
        {
            protect = true;
            Destroy(coll.gameObject);
        }
    }

    //Method to update score
    public void SetCountText()
    {
        countText.text = "Points: " + keyprogress.ToString(); //display/update the score 
    }


}
