using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour {
    private const string MASTER_VOLUME_KEY = "master volume";
    private const string DIFFICULTY_KEY = "difficulty";

    private const float MAX_VOLUME = 1f;
    private const float MIN_VOLUME = 0f;
    
    public static float GetMasterVolume => PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    public static void SetMasterVolume(float volume) {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }
    
    public static float GetDifficulty => PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    public static void SetDifficulty(float difficulty) {
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
    }
}