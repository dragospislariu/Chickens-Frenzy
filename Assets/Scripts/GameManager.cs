using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int setDificulty;
    private float spawnRangeX = 13.0f;
    private float spawnRangeZ = 7.0f;
    public GameObject[] powerupPrefab;
    public GameObject[] enemyPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    public TextMeshProUGUI levelText;
    private int level;
    public float time;
    public TextMeshProUGUI TimeText;
    float currCountdownValue;

    
    private void Awake()
    {
        setDificulty = StartManager.startManager.difficultySet;
        Debug.Log("difficulty: " + setDificulty);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(StartCountdown());// ABSTRACTION
        SpawnEnemyWave(waveNumber);// ABSTRACTION
        SpawnPowerup();// ABSTRACTION
    }

    // Update is called once per frame
    void Update()
    {
        
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            time = 30/setDificulty;
            waveNumber++;
            SpawnEnemyWave(waveNumber);// ABSTRACTION
            SpawnPowerup();// ABSTRACTION

        }
        

    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        level++;
        levelText.text = "Level: " + level;
        int enemyIndex = Random.Range(0, enemyPrefab.Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
        if (level == 11 && enemyCount == 0 && time >= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    void SpawnPowerup()
    {
        int powerupIndex = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[powerupIndex], GenerateSpawnPosition(), powerupPrefab[powerupIndex].transform.rotation);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);

        return randomPos;

    }
    public IEnumerator StartCountdown()
    {
        time = 30/setDificulty;
        while (time >= 0)
        {
            
            TimeText.text = "Time:" + time;
            yield return new WaitForSeconds(1.0f);
            time--;
        }
        if (time <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}