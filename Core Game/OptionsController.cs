using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [SerializeField] private Slider volumeSlider;
    
    private MusicPlayer _musicPlayer;
    private bool changed = false;

    void Start() {
        _musicPlayer = FindObjectOfType<MusicPlayer>();

        volumeSlider.value = PlayerPrefsController.GetMasterVolume;
    }

    private void Update() {
        SetVolumeChange();

        if (changed) {
            PlayerPrefsController.SetMasterVolume(volumeSlider.value);
            changed = false;
        }
    }

    public void SetVolumeChange() {
        if (_musicPlayer)
            _musicPlayer.SetVolume(volumeSlider.value);
        
        changed = true;
    }

    public void Exit() {
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}