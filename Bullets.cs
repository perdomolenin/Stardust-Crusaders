using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public int speed;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        //Determine bullet's starting position based on current player position
        playerPos = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        transform.position = playerPos.position + new Vector3(2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Move object's to the right every frame
        moveRight();
    }

    //Moves objects position to the right and destroys it after reaching a certain position
    private void moveRight() {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
        if (transform.position.x > 11) Destroy(gameObject);
    }

    //Check for collisions and destroy the object it collides with if it's an enemy
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
        }
    }
}
