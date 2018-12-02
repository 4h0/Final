using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHNGGameController : MonoBehaviour
{
    public GameObject keyList;
    public GameObject barrier;
    public Canvas[] gameCanvas;
    public Text[] scoreText;
    public Text[] gameEndText;

    public bool canMove;
    public bool endGame;
    public int key;

    private bool uiTextOn;

    private void Awake()
    {
        endGame = false;
        canMove = false;
        key = keyList.transform.childCount;
    }

    private void Start()
    {
        StartCoroutine(DisableStartScene());
    }

    private void Update()
    {
        if(key <= 0)
        {
            barrier.SetActive(false);
        }

        if(endGame)
        {
            StartCoroutine(GameEnd());
        }
        else
        {
            for(int counter = 0; counter < 2; counter++)
            {
                scoreText[counter].text = "Your Score: " + GameLoader.score;

                scoreText[counter].enabled = true;
                gameEndText[counter].enabled = false;
            }
        }
    }

    IEnumerator DisableStartScene()
    {
        gameCanvas[0].enabled = true;
        gameCanvas[1].enabled = false;

        yield return new WaitForSeconds(3f);

        gameCanvas[0].enabled = false;
        gameCanvas[1].enabled = true;

        canMove = true;
    }

    IEnumerator GameEnd()
    {
        for (int counter = 0; counter < 2; counter++)
        {
            gameEndText[counter].text = "Thank You For Playing";

            scoreText[counter].enabled = false;
            gameEndText[counter].enabled = true;
        }

        Time.timeScale = .1f;

        yield return new WaitForSeconds(.3f);

        GameLoader.gameOn = false;
    }
}
