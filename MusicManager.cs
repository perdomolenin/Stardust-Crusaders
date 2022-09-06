using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> gameSongs;
    private AudioSource currentSong;

    // Start is called before the first frame update
    void Start()
    {
        //Get the object's audisource component to play music
        currentSong = GetComponent<AudioSource>();
        currentSong.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Play a random song every time the current song ends
        if (!currentSong.isPlaying) {
            currentSong.clip = getRandomSong();
            currentSong.Play();
        }
    }

    //Return a random song
    private AudioClip getRandomSong() {
        return gameSongs[Random.Range(0, gameSongs.Count)];
    }
}
