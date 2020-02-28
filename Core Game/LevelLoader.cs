using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    // current index 
    // int _currentSceneIndex;
    //
    // public void LoadNextScene() {
    //     SceneManager.LoadScene(_currentSceneIndex + 1);
    //     
    //     _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    // }

    public void LoadMainMenu() {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadOptions() {
        SceneManager.LoadScene("OptionsScene");
    }

    public void LoadPlay() {
        SceneManager.LoadScene("PlayScene");
    }
}
