using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : AbstractSpawner
{
    //editable inherited attributes
    [SerializeField] private float _spawnRate = 1.5f;
    [SerializeField] private GameObject[] _spawnTypes;

    protected override GameObject[] spawnTypes => _spawnTypes;
    protected override float spawnRate => _spawnRate;


    //tags specify where to spawn and what to monitor after spawn 
    protected override string spawnerTag => "Spawner";
    protected override string spawneeTag => "Enemy";

    private bool spawnedOne;


   






    protected override void Spawn()
    {
        if (spawnTypes.Length == 0) return; // Safety check


        // Pick a random prefab from the list
        GameObject selectedPrefab = spawnTypes[Random.Range(0, spawnTypes.Length)];

        spawnedOne = false;

        while (!spawnedOne)
        {
            //spawn at the area of the spawner 
            Transform spawnPoint = spawnAreas[Random.Range(0, spawnAreas.Length)]; //change this to a list of spawn positions 

            if ((spawnPoint.position - player.transform.position).magnitude <= 20)
            {
                var spawn = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
                spawnedOne = true;
            }
        }

  


    }

    public int getRound()
    {
        return round;
    }



}
