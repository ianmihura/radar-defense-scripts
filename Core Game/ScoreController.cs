using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    private int _enemyScore = 0;
    private int _playerTurns = 0;
    private int _kills = 0;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void AddEnemyScore() {
        _enemyScore++;
    }

    public string GetEnemyScore() {
        return _enemyScore.ToString();
    }

    public void AddPlayerTurn() {
        _playerTurns++;
    }

    public string GetPlayerTurns() {
        return _playerTurns.ToString();
    }

    public void AddKill() {
        _kills++;
    }

    public string GetKills() {
        return _kills.ToString();
    }
}