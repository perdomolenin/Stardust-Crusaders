using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Material[] materials;
    public List<GameObject> bullets;
    public float force;
    public bool shieldsEnabled;
    private Renderer rend;
    private Rigidbody playerRb;
    private GameManager gameManager;
    private bool hasStarPowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables 
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for Space key input to move player
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
        
        //Destroy the player object once the game ends
        if (!gameManager.isGameActive) {
            Destroy(gameObject);
        }
    }

    //Check for powerup triggers and start their respective coroutines
    private void OnTriggerEnter(Collider other) {
        hasStarPowerUp = true;
        if (other.CompareTag("StarPowerUp")) {
            StartCoroutine(starPowerUp());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("BulletPowerUp")) {
            StartCoroutine(bulletPowerUp());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ShieldPowerUp")) {
            StartCoroutine(shieldPowerUp());
            Destroy(other.gameObject);
        }
    }

    //Check for collisions determine whether or not to end the game
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && !shieldsEnabled) {
            StartCoroutine(GameOverCoroutine());
        } else if (other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
        }
    }

    //Waits a few seconds and ends the game
    IEnumerator GameOverCoroutine() {
        yield return new WaitForSeconds(0.05f);
        gameManager.GameOver();
    }

    //Star powerup coroutine (increases score)
    IEnumerator starPowerUp () {
        if (hasStarPowerUp) {
            gameManager.scoreText.color = Color.yellow;
            gameManager.score += 30;
            yield return new WaitForSeconds(3);
            gameManager.scoreText.color = Color.white;
            hasStarPowerUp = false;
        }
    }

    //Bullet powerup coroutine (shoots bullets from player)
    IEnumerator bulletPowerUp () {
        if (hasStarPowerUp) {
            StartCoroutine(shootBullets());
            yield return new WaitForSeconds(5);
            hasStarPowerUp = false;
        }
    }

    //Helper method for the bulletPowerUp coroutine
    IEnumerator shootBullets () {
        while (hasStarPowerUp) {
            yield return new WaitForSeconds(0.2f);
            Instantiate(bullets[0]);
        }
    }

    //Shield powerup coroutine (enables player shields)
    IEnumerator shieldPowerUp () {
        if (hasStarPowerUp) {
            rend.sharedMaterial = materials[1];
            shieldsEnabled = true;
            yield return new WaitForSeconds(8);
            rend.sharedMaterial = materials[0];
            shieldsEnabled = false;
            hasStarPowerUp = false;
        }
    }
}
