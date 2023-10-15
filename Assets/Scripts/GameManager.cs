using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    private AudioSource playerAudio;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject player;
    public ParticleSystem explosionParticle;
    public AudioClip crashSound;

    private int score;
    private int highScore;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        isGameOver = false;
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load the high score from PlayerPrefs
        score = 0;
        UpdateScore(0);
        UpdateHighScore(highScore);
        titleScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            UpdateHighScore(score);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        Vector3 playerPos = player.transform.position;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;
        playerAudio.PlayOneShot(crashSound, 2.0f);

        Destroy(player);
        Instantiate(explosionParticle, playerPos, explosionParticle.transform.rotation);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateHighScore(int newHighScore)
    {
        highScore = newHighScore;
        highScoreText.text = "High Score: " + highScore;
        PlayerPrefs.SetInt("HighScore", highScore); // Save the high score to PlayerPrefs
    }
}
