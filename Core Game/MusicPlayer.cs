using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private AudioSource _audioSource;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefsController.GetMasterVolume;
    }

    public void SetVolume(float volume) {
        _audioSource.volume = volume;
    }
}