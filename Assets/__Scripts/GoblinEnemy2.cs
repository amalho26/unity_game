using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoblinEnemy2 : MonoBehaviour
{
    public int enemyHealth2;
    public Transform Player2;

    float moveSpeed2 = 0.5f;
    float range2 = 30f;
    float rotationSpeed2 = 3f;
    float stop2 = 0;

    public float enemyCooldown2 = 1;
    public int damage2 = 1;
    public float protectTime = 10f;
    
    public Transform myTransform2;
    
    //declare private variables
    //for player 2:
    private bool playerInRange2 = false;
    private bool canAttack2 = true;


    void Awake()
    {

        Player2 = GameObject.FindWithTag("Player2").transform;//target player1
        myTransform2 = transform; //cache transform data for easy access
    }

    void Start()
    {
        enemyHealth2 = 1;
    }



    void Update()
    {

        float distance2 = Vector3.Distance(myTransform2.position, Player2.position);

        if(distance2<=range2)
        {
            //look
            myTransform2.rotation = Quaternion.Slerp(myTransform2.rotation, Quaternion.LookRotation(Player2.position-myTransform2.position), rotationSpeed2*Time.deltaTime);

            //move
            if(distance2>stop2)
            {
                myTransform2.position+= myTransform2.forward * moveSpeed2 * Time.deltaTime;
            }
        }


        if(GameObject.Find("Player2").GetComponent<PlayerController2>().protect == true){
            StartCoroutine(MyCoroutine());
        }
  
        else if (playerInRange2 && canAttack2)
        {
            if(GameObject.Find("Player2").GetComponent<PlayerController2>().protect == false){
                GameObject.Find("Player2").GetComponent<PlayerController>().currentHealth -= damage2;
                StartCoroutine(AttackCooldown2());
                if(GameObject.Find("Player2").GetComponent<PlayerController>().currentHealth == 0)
                {
                    Invoke("Restart", 2); //restart the scene when the player's health is zero
                }
            }
        }


    }

    void OnTriggerEnter(Collider coll2)
    {
         if(coll2.gameObject.CompareTag("Player2"))
          {
             playerInRange2 = true;
         
            if(GameObject.Find("Player2").GetComponent<PlayerController2>().fight == true)
            {
            if(enemyHealth2 > 0)
            {
                enemyHealth2 -= damage2;
            }
            if(enemyHealth2 <= 0)
            {
                Destroy(gameObject);
                GameObject.Find("Player2").GetComponent<PlayerController2>().keyprogress += 1 ;
            }
            }
          }
 
        
        if(coll2.gameObject.CompareTag("Sword") && (GameObject.Find("Player2").GetComponent<PlayerController2>().swing == true))
            {
            if(enemyHealth2 > 0)
            {
                enemyHealth2 -= damage2;
            }
            if(enemyHealth2 <= 0)
            {
                Destroy(gameObject);
                GameObject.Find("Player2").GetComponent<PlayerController2>().keyprogress += 1 ;
            }
            }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player2"))
        {
            playerInRange2 = false;
        }
    }

    IEnumerator AttackCooldown2()
     {
        canAttack2 = false;
        yield return new WaitForSeconds(enemyCooldown2);
        canAttack2 = true;
     }

    IEnumerator MyCoroutine()
    {
        GameObject.Find("Player2").GetComponent<PlayerController2>().protect = true; 
        yield return new WaitForSeconds(10f); //wait 10 seconds
        GameObject.Find("Player2").GetComponent<PlayerController2>().protect = false;
    }
   
    public void Restart()
    {
        SceneManager.LoadScene("openingScene");
    }

}

