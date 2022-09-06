using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float speed;
    public int yRange;
    private GameManager gameManager;
    private Rigidbody spawnRb;
    private Vector3 torque;
    private bool moveDown;
    private int zigzagYRange;
    
    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        spawnRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        transform.position = randomSpawnPos();
        moveDown = false;
        zigzagYRange = Random.Range(0, yRange);
        torque = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    // Update is called once per frame
    void Update()
    {
        //Move the object to the left every frame
        moveLeft();

        //Add torque to enemies and make spawn powerups zigzag vertically
        if (gameObject.CompareTag("Enemy")) {
            spawnRb.AddTorque(torque);
        } else {
            zigzagVertically();
        }
    }

    //Moves object to the left by reducing x vector position
    private void moveLeft() {
        transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
        if (transform.position.x < -12) Destroy(gameObject);
    }

    //Makes the object zigzag vertically
    private void zigzagVertically() {
        if (!moveDown) {
            transform.position += new Vector3(0, Time.deltaTime * speed / 2, 0);
            if (transform.position.y >= zigzagYRange) {
                moveDown = true;
                zigzagYRange = Random.Range(0, yRange);
            }
        } else {
            transform.position -= new Vector3(0, Time.deltaTime * speed / 2, 0);
            if (transform.position.y <= -zigzagYRange) {
                moveDown = false;
                zigzagYRange = Random.Range(0, yRange);
            }
        }
    }

    //Spawn the object at random position between -yRange and yRange
    private Vector3 randomSpawnPos() {
        return new Vector3 (11, Random.Range(-yRange, yRange), 0);
    }
}
