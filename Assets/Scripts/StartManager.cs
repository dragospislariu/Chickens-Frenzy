using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartManager : MonoBehaviour
{
    public static StartManager startManager { get; private set; } // ENCAPSULATION
    public int difficultySet;
  
    private void Awake()
    {
        if (startManager == null)
        {
            startManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartNew()
    {
        
        SceneManager.LoadScene(1);
    }
   public  void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
