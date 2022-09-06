using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public float difficulty;
    private Button button;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(setDifficulty);
    }

    //Sets the game's difficulty (based on enemy spawn rate)
    void setDifficulty() {
        gameManager.startGame(difficulty);
    }
}
