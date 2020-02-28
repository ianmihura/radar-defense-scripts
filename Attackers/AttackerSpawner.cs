using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerSpawner : MonoBehaviour {

    bool _spawning = true;
    [SerializeField] Attacker[] attackerArray;
    private WaveController _waveController;
    private int _waveDifficulty = 0;

    private void Start() {
        _waveController = FindObjectOfType<WaveController>();
    }

    public void UpdateWaveDifficulty() {
        _waveDifficulty = _waveController.GetCurrentWave() / _waveController.GetAmountOfWaves();
    }

    private Attacker _getAttackerToSpawn() {
        int attackerIndex = 0;
        float range = Random.Range(0f, 4f);

        // difficulty measured with linear functions, with a range of 1
        if (range > 1f) return null;
        if (range > (_waveDifficulty * 0.5f + 0.5f)) {
            attackerIndex = 0;
        } else if (range > (_waveDifficulty * 0.8f + 0.1f)) {
            attackerIndex = 1;
        } else if (range > (_waveDifficulty * 0.5f - 0.05f)) {
            attackerIndex = 2;
        } else if (range > (_waveDifficulty * 0.5f - 0.2f)) {
            attackerIndex = 3;
        } else {
            attackerIndex = 4;
        }

        return attackerArray[attackerIndex];
    }

    public void SpawnAttackers() {
        _spawn(_getAttackerToSpawn());
    }

    private void _spawn(Attacker attacker) {
        if (!attacker) return;
        
        Attacker newAttacker = Instantiate(
                attacker,
                transform.position,
                Quaternion.identity) 
            as Attacker;
        newAttacker.transform.parent = transform;
    }
}
