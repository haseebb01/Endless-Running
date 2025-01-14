using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    private int initAmount = 5;
    private int spawnInterval = 11;
    private int lastSpwanZ = 22;
    private int spawnAmount = 0;

    //public List<GameObject> enemy;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnObstacles();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void SpawnObstacles()
        {
        for (int i = 0; i < spawnAmount; i++) 
        {
            lastSpwanZ += spawnInterval;
            if(Random.Range(0, 4) == 0)
            {
                //enemy
                if (Random.Range(0, 5) == 1)
                {
                    //Instantiate(enemy, new Vector3(space.GetLane(), 0)
                }

            }
            else
            {
                //enemy
                if (Random.Range(0, 5) == 1)
                {

                }
            }
        }

        }
}
