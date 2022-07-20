using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    
    private Button button;
    public GameObject startButton;
    public GameObject difficultyScreen;
    public int difficulty;
    private StartManager startManager;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        startManager = GameObject.Find("Start Manager").GetComponent<StartManager>();
    }

    
    
    void SetDifficulty()
    {
        startManager.difficultySet=difficulty;
        Debug.Log(gameObject.name + " was cliked");
        startButton.SetActive(true);
        difficultyScreen.SetActive(false);
    }
}
