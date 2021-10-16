using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GhostController : MonoBehaviour
{

    public int enemyHealth;
    public Transform Player;

    float range = 30f;
    float rotationSpeed = 3f;
    float stop = 0;

    
    private Vector3 smoothVelocity = Vector3.zero;
    float smoothTime = 10.0f;

    public float enemyCooldown = 1;
    public int damage = 1;

    public Transform myTransform;
    
    //declare private variables
    
    private bool playerInRange = false;
    private bool canAttack = true;


    void Awake()
    {

        Player = GameObject.FindWithTag("Player").transform;//target player
        myTransform = transform; //cache transform data for easy access
    }

    void Start()
    {
        enemyHealth = 5;
    }



    void Update()
    {

        float distance = Vector3.Distance(myTransform.position, Player.position);

        if(distance<=range)
        {
            //look
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(Player.position-myTransform.position), rotationSpeed*Time.deltaTime);

            //move
            if(distance>stop)
            {
                myTransform.position = Vector3.SmoothDamp(myTransform.position, new Vector3(Player.position.x, myTransform.position.y, Player.position.z), ref smoothVelocity, smoothTime);

            }
        }


        if (playerInRange && canAttack)
        {
             GameObject.Find("Player").GetComponent<PlayerController1>().currentHealth -= damage;
             StartCoroutine(AttackCooldown());
             if(GameObject.Find("Player").GetComponent<PlayerController1>().currentHealth == 0)
             {
                 Invoke("Restart", 2); //restart the scene
             }
        }

    }

    void OnTriggerEnter(Collider coll)
    {
         if(coll.gameObject.CompareTag("Player"))
          {
             playerInRange = true;
         
            if(GameObject.Find("Player").GetComponent<PlayerController1>().fight == true)
            {
            if(enemyHealth > 0)
            {
                enemyHealth -= damage;
            }
            if(enemyHealth <= 0)
            {
                Destroy(gameObject);
                GameObject.Find("Player").GetComponent<PlayerController1>().keyprogress += 50;
            }
            }
          }

        
        if(coll.gameObject.CompareTag("Sword") && (GameObject.Find("Player").GetComponent<PlayerController1>().swing == true))
            {
            if(enemyHealth > 0)
            {
                enemyHealth -= damage;
            }
            if(enemyHealth <= 0)
            {
                Destroy(gameObject);
                GameObject.Find("Player").GetComponent<PlayerController1>().keyprogress += 50;
            }
            }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator AttackCooldown() //Method to protect the player when the shield is picked up 
     {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
     }

    public void Restart() //Method to reload the scene when the player's health is zero/when invoked
    {
        SceneManager.LoadScene("openingScene");
    }


}
