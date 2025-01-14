using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemanager : MonoBehaviour
{
    public GameObject[] tileprefabs;
    public float zSpawn = 0;
    public float titleLength = 30;
    public int numberOftiles = 7; // Increased the number of active tiles
    private List<GameObject> activetiles = new List<GameObject>();

    public Transform playerTransform;

    void Start()
    {
        for (int i = 0; i < numberOftiles; i++)
        {
            if (i == 0)
                Spawntile(0); // Spawn the starting tile
            else
                Spawntile(Random.Range(0, tileprefabs.Length)); // Spawn random tiles
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust the spawn condition to keep more tiles visible ahead of the player
        if (playerTransform.position.z - 35 > zSpawn - (numberOftiles * titleLength))
        {
            Spawntile(Random.Range(0, tileprefabs.Length));
            Deletetile();
        }
    }

    public void Spawntile(int titleIndex)
    {
        // Instantiate the tile at the correct position
        GameObject go = Instantiate(tileprefabs[titleIndex], transform.forward * zSpawn, transform.rotation);
        activetiles.Add(go);
        zSpawn += titleLength;
    }

    private void Deletetile()
    {
        // Destroy the oldest tile to manage memory
        Destroy(activetiles[0]);
        activetiles.RemoveAt(0);
    }
}
