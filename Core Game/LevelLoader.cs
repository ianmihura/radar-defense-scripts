using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void LoadMainMenu() {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadOptions() {
        SceneManager.LoadScene("OptionsScene");
    }

    public void LoadCredits() {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadPlay() {
        FindObjectOfType<TransformVerticalMover>().DestroyMe();
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadGameOver() {
        SceneManager.LoadScene("GameOverScene");
    }
}
