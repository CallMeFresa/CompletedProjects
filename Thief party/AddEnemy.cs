using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
  
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyKNIGHT");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.GetComponent<EnemyKnightController>().UpdatePlayer();
        }
    }

    
}
