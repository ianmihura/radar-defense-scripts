using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour {
    private const string MASTER_VOLUME_KEY = "master volume";
    private const string RELATIONSHIP_START = "relationship start";
    private const string RELATIONSHIP_ADD = "relationship add";
    private const string BRS = "brs";

    private const float MAX_VOLUME = 1f;
    private const float MIN_VOLUME = 0f;

    public const int RELATIONSHIP_START_EASY = 50;
    public const int RELATIONSHIP_START_HARD = 25;
    public const int RELATIONSHIP_ADD_MAX = 10;
    public const int RELATIONSHIP_ADD_MIN = 1;

    public const int MAX_BRS = 5;
    public const int MIN_BRS = 0;

    public static float GetMasterVolume => PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    public static void SetMasterVolume(float volume) {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static int GetRelationshipStart => PlayerPrefs.GetInt(RELATIONSHIP_START);
    public static void SetRelationshipStart(int relationship) {
        if(relationship == RELATIONSHIP_START_EASY || relationship == RELATIONSHIP_START_HARD)
            PlayerPrefs.SetInt(RELATIONSHIP_START, relationship);
    }

    public static int GetRelationshipAdd => PlayerPrefs.GetInt(RELATIONSHIP_ADD);
    public static void SetRelationshipAdd(int relationship) {
        if(relationship >= RELATIONSHIP_ADD_MIN && relationship <= RELATIONSHIP_ADD_MAX)
            PlayerPrefs.SetInt(RELATIONSHIP_ADD, relationship);
    }

    public static int GetBRS => PlayerPrefs.GetInt(BRS);
    public static void SetBRS(int brs) {
        if(brs >= MIN_BRS && brs <= MAX_BRS)
            PlayerPrefs.SetInt(BRS, brs);
    }
}