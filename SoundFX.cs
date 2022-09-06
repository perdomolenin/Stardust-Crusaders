using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public List<AudioClip> soundClips;
    private AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        //Determine which sound FX to play based on the game object's tag
        soundSource = gameObject.GetComponent<AudioSource>();
        if (gameObject.CompareTag("Fuel")) {
            soundSource.clip = soundClips[0];
            soundSource.loop = true;
            soundSource.Play();
        } else if (gameObject.CompareTag("Bullet") || gameObject.CompareTag("GameOver")) {
            soundSource.clip = soundClips[0];
            soundSource.loop = false;
            soundSource.Play();
        }
    }

    //Play Sound FX on trigger
    private void OnTriggerEnter(Collider other) {

        //Determine powerup sound FX
        if (gameObject.CompareTag("Player") && (other.CompareTag("StarPowerUp") || other.CompareTag("ShieldPowerUp"))) {
            if (other.gameObject.CompareTag("StarPowerUp")) soundSource.clip = soundClips[0];
            else soundSource.clip = soundClips[1];

            soundSource.loop = false;
            if (!soundSource.isPlaying) {
                soundSource.Play();
            } else {
                soundSource.Stop();
                soundSource.Play();
            }
        }

        //Determine explosion sound FX
        if (gameObject.CompareTag("Explosion") && other.CompareTag("Enemy")) {
            soundSource.clip = soundClips[0];
            soundSource.loop = false;
            if (!soundSource.isPlaying) {
                soundSource.Play();
            } else {
                soundSource.Stop();
                soundSource.Play();
            }
        }
    }
}
