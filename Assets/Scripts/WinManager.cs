using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WinManager : MonoBehaviour
{
    private float spawnRangeX = 14.0f;
    private float spawnRangeY = 5.0f;
    public GameObject[] fireworkPrefab;
    private AudioSource winManagerAudio;
    public AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        winManagerAudio = GetComponent<AudioSource>();
        InvokeRepeating("SpawnFirework", 0, 1);
    }

    public void Restart()
    {

        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    void SpawnFirework()
    {
        int fireworkIndex = Random.Range(0, fireworkPrefab.Length);
        Instantiate(fireworkPrefab[fireworkIndex], GenerateSpawnPosition(), fireworkPrefab[fireworkIndex].transform.rotation);
        winManagerAudio.PlayOneShot(explosionSound,1.0f);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY,0);

        return randomPos;

    }
}
