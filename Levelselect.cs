using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levelselect : MonoBehaviour
{
    public Button level02Button, level03Button;
    int levelPassed;
    public string level1;
    public string level2;

    // Start is called before the first frame update
    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level02Button.interactable = false;
        level03Button.interactable = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);

    }

    public void Level2()
    {
        SceneManager.LoadScene(level2);
    }

}
