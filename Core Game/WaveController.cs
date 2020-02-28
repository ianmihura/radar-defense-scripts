using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    private int[] TURNS_FOR_WAVE = {10, 12, 15, 20, 20, 25, 26, 27, 28, 29, 30};
    private string[] ENABLE_IN_WAVE = { "", "", "", "X70", "", "P36" };

    private int _currentWave = 0;
    private int _numberOfAttackers = 0;

    private TurnController _turnController;

    private void Start() {
        _turnController = FindObjectOfType<TurnController>();
    }

    public bool IsLoadNextWave() {
        if (TURNS_FOR_WAVE[_currentWave] > _turnController.GetCurrentTurn()) {
            return false;
        }

        _turnController.SetSpawning(false);
        
        return _numberOfAttackers <= 0;
    }

    public void NextWave() {
        _currentWave++;
        if (ENABLE_IN_WAVE.Length <= _currentWave)
            return;

        var defenderButtons = FindObjectsOfType<DefenderButton>();

        switch (ENABLE_IN_WAVE[_currentWave]) {

            case "X70":
            foreach (var defenderButton in defenderButtons) {
                if (defenderButton.GetDefender.isX71) {
                    defenderButton.ShowDefenderButton();
                    return;
                }
            }

            break;

            case "P36":
            foreach (var defenderButton in defenderButtons) {
                if (defenderButton.GetDefender.isPlane) {
                    defenderButton.ShowDefenderButton();
                    return;
                }
            }

            break;

            default:
            break;
        }
    }

    public void AttackerSpawned() {
        _numberOfAttackers++;
    }

    public void AttackerKilled() {
        _numberOfAttackers--;
    }

    public int GetCurrentWave() {
        return _currentWave;
    }

    public int GetAmountOfWaves() {
        return TURNS_FOR_WAVE.Length;
    }

}