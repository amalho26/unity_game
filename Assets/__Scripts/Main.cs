using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject goblinEnemy;
    public GameObject goblinEnemy2;
    int selectedCharacter;

 void Start()
{

selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");


if(selectedCharacter == 0){
    Instantiate(goblinEnemy, new Vector3(15,0,20), Quaternion.identity);
    Instantiate(goblinEnemy, new Vector3(10,0,24), Quaternion.identity);
    Instantiate(goblinEnemy, new Vector3(22,0,24), Quaternion.identity);
    Instantiate(goblinEnemy, new Vector3(33,0,21), Quaternion.identity);
    Instantiate(goblinEnemy, new Vector3(20,0,15), Quaternion.identity);
}

if(selectedCharacter == 1){
    Instantiate(goblinEnemy2, new Vector3(15,0,20), Quaternion.identity);
    Instantiate(goblinEnemy2, new Vector3(10,0,24), Quaternion.identity);
    Instantiate(goblinEnemy2, new Vector3(22,0,24), Quaternion.identity);
    Instantiate(goblinEnemy2, new Vector3(33,0,21), Quaternion.identity);
    Instantiate(goblinEnemy2, new Vector3(20,0,15), Quaternion.identity);
}


}

}
