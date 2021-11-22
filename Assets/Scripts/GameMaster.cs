using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    public int maxLives = 3;
    public PlayerController paddle;
    public Text livesText;
    private int lives;
    private GameObject[] bricks;
    private AudioController audioController;
    void Start() {
        lives = maxLives;
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
    public void AddLife(int howMany) {
        audioController.LifeUp();
        lives += howMany;
    }

    public void HandleBallDeath() {
        audioController.LifeLost();
        lives--;
        if (lives == 0) {
            SceneManager.LoadScene("GameOver");
            return;
        }
        paddle.Reset();
    }

    public void CheckWon() {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length == 0 && lives > 0) { // probably over-defensive here
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    void Update() {
        string noun = "balls";
        if (lives == 1) {
            noun = "ball";
        }
        livesText.text = string.Format("{0} {1}", lives, noun);
        CheckWon();
    }
}
