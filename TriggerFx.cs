using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFx : MonoBehaviour
{
    public GameManager gameManager;
    private ParticleSystem particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();
    }

    //Play explosion animation when triggered by an enemy object
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            particleEffect.Play();
        }

    }
}
