using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver, win;
    [SerializeField] GameObject winText, loseText, tutorialText;
    [SerializeField] Package package;
    [SerializeField] float money;
    [SerializeField] TextMeshProUGUI durabilityText, moneyText;
    public Image durabilityBar;
    public AudioSource winSound, loseSound;
    bool playedWinSound, playedLoseSound;
    public float tutorialCountdown = 4f;

    public GameObject player;
    public GameObject pauseMenu;
    public bool paused;
    private void Awake()
    {
        money = 0;
    }


    private void Start()
    {
        StartCoroutine(Tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        durabilityText.text = "Velocity: " + player.GetComponent<Rigidbody>().velocity.magnitude.ToString("F1");
        moneyText.text = "$" + money;

        if (win && !gameOver && !paused)
        {
            winText.SetActive(true);
            package.gameObject.SetActive(false);
            if (!playedWinSound)
            {
                winSound.Play();
                playedWinSound = true;
                player.SetActive(false);
            }
        }

        if (paused && !win && !gameOver)
        {
            player.SetActive(false);
        }
        if(!paused && !win && !gameOver)
        {
            player.SetActive(true);
        }

        if (gameOver && !win && !paused)
        {
            loseText.SetActive(true);
            if (!playedLoseSound)
            {
                loseSound.Play();
                playedLoseSound = true;
            }
        }

        if(package.durability <= 0)
        {
            gameOver = true;
            package.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !win && !gameOver)
        {
            pauseMenu.SetActive(true);
            paused = true;
        }

    }

    IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(tutorialCountdown);
        tutorialText.SetActive(false);
        //tutorialOver = true;
    }

    public void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
    public void End()
    {
        Application.Quit();
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseMoney()
    {
        money++;
    }

}
