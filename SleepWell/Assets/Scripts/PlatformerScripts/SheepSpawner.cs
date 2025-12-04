using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SheepSpawner : MonoBehaviour {

      //Object variables
      public GameObject sheepPrefab;
      public Transform[] spawnPoints;
      private int rangeEnd;
      private Transform spawnPoint;

      //Timing variables
      public float spawnRangeStart = 0.5f;
      public float spawnRangeEnd = 1.2f;
      private float timeToSpawn;
      private float spawnTimer = 0f;

      void Start(){
       //assign the length of the array to the end of the random range
             rangeEnd = spawnPoints.Length - 1 ;
       }

      void FixedUpdate(){
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                  spawnSheep();
                  spawnTimer =0f;
            }
      }

      void spawnSheep(){
            int SPnum = Random.Range(0, rangeEnd);
            spawnPoint = spawnPoints[SPnum];
            Instantiate(sheepPrefab, spawnPoint.position, Quaternion.identity);
      }
}