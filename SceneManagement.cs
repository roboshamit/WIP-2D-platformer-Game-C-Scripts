using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string mainmenu;
    public string currentscene;
    public string samplescene;
    public string busscene;
    public string schscene;
    public string officescene;
    public string options;
    public string levelselect;
    public GameObject pautext;

    public bool ispaused = false;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Bus")
        {
            FindObjectOfType<SoundManager>().Play("Bus Background");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void PauseGame()
    {
        if (ispaused)
        {
            Time.timeScale = 1;
            ispaused = false;
            pautext.SetActive(false);

        }
        else if(!ispaused)
        {
            Time.timeScale = 0;
            ispaused = true;
            pautext.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneName);
        AudioListener.pause = !AudioListener.pause;
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainmenu);
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void Play()
    {
        SceneManager.LoadScene(samplescene);

    }

    public void BusScene()
    {
        SceneManager.LoadScene(busscene);
    }

    public void SchoolScene()
    {
        SceneManager.LoadScene(schscene);
    }

    public void OfficeScene()
    {
        SceneManager.LoadScene(officescene);
    }

    public void Levelselect()
    {
        SceneManager.LoadScene(levelselect);
    }

    public void FacebookURL()
    {
        Application.OpenURL("https://www.facebook.com");
    }
}
