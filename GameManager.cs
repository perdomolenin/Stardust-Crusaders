using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> powerUps;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionsText;
    public TextMeshProUGUI endScore;
    public GameObject player;
    public GameObject titleScreen;
    public Button restartButton;
    public float enemySpawnRate;
    public float powerUpSpawnRate;
    public float score;
    public bool isGameActive;
    private Vector3 spawnPos;
    private int yPos;
    
    // Update is called once per frame
    void Update()
    {
        //Show and update the score and instructions text while the game is running
        if (!isGameActive) {
            scoreText.gameObject.SetActive(false);
        } else {
            score += Time.deltaTime;
            scoreText.text = "Score: " + (int)score;
            scoreText.gameObject.SetActive(true);
            instructionsText.gameObject.SetActive(false);
        }
    }

    //Spawns enemies at the spawnRate
    IEnumerator spawnEnemy() {
         while (isGameActive) {
            yield return new WaitForSeconds(enemySpawnRate);
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index]);
        }
    }

    //Spawn random powerups
    IEnumerator spawnPowerUp() {
        while (isGameActive) {
            yield return new WaitForSeconds(Random.Range(powerUpSpawnRate/2, powerUpSpawnRate+2));
            int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index]);
        }
    }

    //Update the score text
    public void UpdateScore() {
        score += ((int)Time.deltaTime);
        scoreText.text = "Score: " + score;
    }

    //Ends the game and shows the game over screen
    public void GameOver() {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        endScore.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        endScore.text = "Score: " + (int)score;
        restartButton.onClick.AddListener(RestartGame);
    }

    //Restarts the game scene
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Sets the difficulty (enemy spawn rate) and starts the game
    public void startGame(float difficulty) {
        titleScreen.SetActive(false);
        isGameActive = true;
        enemySpawnRate /= difficulty;
        Instantiate(player);
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnPowerUp());
    }
}
