using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int playerhealth;
    public bool lifedecreased;
    public static GameObject life1, life2, life3, life4, life5, life6, life7, life8, life9, life10;
    public static GameObject gameoverscreen;
    //SceneManagement sm;

    
    

    // Start is called before the first frame update
    void Start()
    {
        lifedecreased = false;

        /*   life1 = FindObjectOfType<GameObject>();
         life2 = FindObjectOfType<GameObject>();
         life3 = FindObjectOfType<GameObject>();
         life4 = FindObjectOfType<GameObject>();
         life5 = FindObjectOfType<GameObject>();
         */
         life1 = GameObject.Find("life1");
         life2 = GameObject.Find("life2");
         life3 = GameObject.Find("life3");
         life4 = GameObject.Find("life4");
         life5 = GameObject.Find("life5");
        life6 = GameObject.Find("life6");
        life7 = GameObject.Find("life7");
        life8 = GameObject.Find("life8");
        life9 = GameObject.Find("life9");
        life10 = GameObject.Find("life10");
        gameoverscreen = GameObject.Find("GameOverScreen");

        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);
        life4.gameObject.SetActive(true);
        life5.gameObject.SetActive(true);
        life6.gameObject.SetActive(true);
        life7.gameObject.SetActive(true);
        life8.gameObject.SetActive(true);
        life9.gameObject.SetActive(true);
        life10.gameObject.SetActive(true);
        gameoverscreen.gameObject.SetActive(false);

        //sm = GetComponent<SceneManagement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseLife(int damageToGivePlayer)
    {
        LifeCheck();
        lifedecreased = true;
        if (playerhealth <= 0)//GAMEOVER
        {
            
            //CameraShake.shouldShake = false;
            gameoverscreen.SetActive(true);
            FindObjectOfType<SoundManager>().Play("Player hurt");
            //sm.ispaused = true;
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                
                //PauseGame();
                // Debug.Log("Game Over");
            }
        }
        else
        {
            playerhealth-=damageToGivePlayer;
            //FindObjectOfType<SoundManager>().Play("Player hurt");
        }
    }
    
 

    public void Givebullet()
    {
        if (BulletCount.scorevalue < 10)
        {
            //player.bullet = player.bullet + 3;
            BulletCount.scorevalue = BulletCount.scorevalue + 1;
        }
        else if (BulletCount.scorevalue >= 10)
            BulletCount.scorevalue = 10;
    }

    public void GiveStick()
    {
        if (WeaponPickup.pickedweapon)
        {

        }
    }

    public void GiveHealth()
    {
        if (playerhealth <10)
        {
           playerhealth = playerhealth + 1;
           LifeCheckPlus();
        }
        else if (playerhealth >= 10)
            playerhealth = 10;

    }

    public void LifeCheck()
    {
        switch (playerhealth)////UI LIFE SHOW
        {
            case 9:
                life10.gameObject.SetActive(false);
                break;
            case 8:
                life9.gameObject.SetActive(false);
                break;
            case 7:
                life8.gameObject.SetActive(false);
                break;
            case 6:
                life7.gameObject.SetActive(false);
                break;
            case 5:
                life6.gameObject.SetActive(false);
                break;
            case 4:
                life5.gameObject.SetActive(false);
                break;
            case 3:
                life4.gameObject.SetActive(false);
                break;
            case 2:
                life3.gameObject.SetActive(false);
                break;
            case 1:
                life2.gameObject.SetActive(false);
                break;
            case 0:
                life1.gameObject.SetActive(false);
                break;

        }
    }

    public void LifeCheckPlus()
    {
        switch (playerhealth)////UI LIFE SHOW
        {
            case 10:
                life10.gameObject.SetActive(true);
                break;
            case 9:
                life9.gameObject.SetActive(true);
                break;
            case 8:
                life8.gameObject.SetActive(true);
                break;
            case 7:
                life7.gameObject.SetActive(true);
                break;
            case 6:
                life6.gameObject.SetActive(true);
                break;
            case 5:
                life5.gameObject.SetActive(true);
                break;
            case 4:
                life4.gameObject.SetActive(true);
                break;
            case 3:
                life3.gameObject.SetActive(true);
                break;
            case 2:
                life2.gameObject.SetActive(true);
                break;
            case 1:
                life1.gameObject.SetActive(true);
                break;

        }
    }



}
