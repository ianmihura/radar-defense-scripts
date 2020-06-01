using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    
    
    void Start() {
        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        var scoreObject = FindObjectOfType<ScoreController>();
        if (!scoreObject)
            return;

        textChildren[2].text = scoreObject.GetPlayerTurns();
        textChildren[4].text = scoreObject.GetEnemyScore();
        textChildren[6].text = scoreObject.GetKills();
    }
}
