using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    public GameObject splash_panel;
    public GameObject help_panel;
    public GameObject pause_panel;
    public GameObject gameplay_panel;
    public GameObject gameresult_panel;

    public RectTransform[] init_spawn_positions;
    public Sprite[] health_sprits;
    public GameObject item;
    public float minSpawnTime = 0.1f;
    public float maxSpawnTime = 1f;

    private float nextSpawnTime;

    public static bool gamePlay = false;
    public static int score = 0;
    public static int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + GetRandomSpawnTime();
        Debug.Log("health : " + gameplay_panel.transform.GetChild(2).GetComponent<RectTransform>().position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePlay)
        {
            if(health <= 0)
            {
                gamePlay = false;
                gameresult_panel.transform.GetChild(0).GetComponent<TMP_Text>().text = "GAME RESULT\nYOUR SCORE IS " + score;
                gameresult_panel.SetActive(true);
                gameplay_panel.SetActive(false);
                return;
            }

            gameplay_panel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "SCORE : " + score;
            gameplay_panel.transform.GetChild(2).GetComponent<Image>().sprite = health_sprits[health / 30];
            if (Time.time >= nextSpawnTime)
            {
                SpawnObject();
                nextSpawnTime = Time.time + GetRandomSpawnTime();
            }
        }
    }

    private void SpawnObject()
    {
        // Instantiate the prefab at the current position and rotation
        int i = Random.Range(0, init_spawn_positions.Length);
        GameObject var = Instantiate(item, Vector3.zero, transform.rotation);
        var.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().position = init_spawn_positions[i].GetComponent<RectTransform>().position;
    }

    private float GetRandomSpawnTime()
    {
        // Generate a random time between minSpawnTime and maxSpawnTime
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void showSplashPanel()
    {
        splash_panel.SetActive(true);
        help_panel.SetActive(false);
        pause_panel.SetActive(false);
        gameplay_panel.SetActive(false);
        gameresult_panel.SetActive(false);
    }

    public void showHelpPanel()
    {
        splash_panel.SetActive(false);
        help_panel.SetActive(true);
        //pause_panel.SetActive(false);
        //gameplay_panel.SetActive(false);
    }

    public void showPausePanel()
    {
        gamePlay = false;
        pause_panel.transform.GetChild(0).GetComponent<TMP_Text>().text = "THE GAME IS PAUSED!\nYOUR SCORE IS " + score;

        //splash_panel.SetActive(false);
        //help_panel.SetActive(false);
        pause_panel.SetActive(true);
        gameplay_panel.SetActive(false);
    }

    public void showGamePlayPanel()
    {
        gamePlay = true;
        splash_panel.SetActive(false);
        //help_panel.SetActive(false);
        pause_panel.SetActive(false);
        gameplay_panel.SetActive(true);
    }

    public void exitApp()
    {
        Application.Quit();
    }
}
