using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public GameObject[] enemies;
    public GameObject[] powerups;
    public Transform[] spawnSpots;
    public Transform[] spawnSpotsPowerup;
    public GameObject spawnEffectEnemy;
    public GameObject spawnEffectPowerup;
    private GameObject spawneffectenemy;
    private GameObject spawneffectpowerup;
   
    public float timeBtwSpawns;
	public float startTimeBtwSpawns;
    public float timeBtwSpawnPowerups;
    public float startTimeBtwSpawnsPowerups;
    public int numEnemies=0;
    public int maxEnemies=10;
    public int numPowerups = 0;
    public int maxPowerups= 10;

    private int CheckRepeatESpot;
    private int CheckRepeatHSpot;

    public List<GameObject> Enemies = new List<GameObject>();
    private void Update()
    {
        if (numEnemies < 0)
        {
            numEnemies = 0;
        }
        if (numEnemies <= maxEnemies)
        {
            if (timeBtwSpawns <= 0)
            {
                StartCoroutine(SpawnEnemy());
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
        if (numPowerups <= maxPowerups)
        {
            if (timeBtwSpawnPowerups <= 0)
            {
                StartCoroutine(SpawnPowerup());
                timeBtwSpawnPowerups = startTimeBtwSpawnsPowerups;
            }
            else
            {
                timeBtwSpawnPowerups -= Time.deltaTime;
            }
        }   

    }
    IEnumerator SpawnEnemy()
    {
        int randPos = Random.Range(0, spawnSpots.Length);
        while(randPos==CheckRepeatESpot) { randPos = Random.Range(0, spawnSpots.Length); };
        CheckRepeatESpot = randPos;
        int randMon = Random.Range(0, enemies.Length);
        spawneffectenemy = Instantiate(spawnEffectEnemy, spawnSpots[randPos].position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(spawneffectenemy, 1);
        GameObject newEnemy=Instantiate(enemies[randMon], spawnSpots[randPos].position, Quaternion.identity);
        numEnemies++;
        Enemies.Add(newEnemy);
    }
    IEnumerator SpawnPowerup()
    {
        int randPos = Random.Range(0, spawnSpotsPowerup.Length);
        while (randPos == CheckRepeatHSpot) { randPos = Random.Range(0, spawnSpots.Length); };
        CheckRepeatHSpot = randPos;
        int randMon = Random.Range(0, powerups.Length);
        spawneffectpowerup = Instantiate(spawnEffectPowerup, spawnSpotsPowerup[randPos].position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(spawneffectpowerup, 0.5f);
        Instantiate(powerups[randMon], spawnSpotsPowerup[randPos].position, Quaternion.identity);
        numPowerups++;
    }
}
