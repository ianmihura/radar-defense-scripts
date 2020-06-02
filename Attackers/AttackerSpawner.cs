using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerSpawner : MonoBehaviour {

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
        
        // 1 in 3 chances an enemy gets spawned
        float range = Random.Range(0f, 3f);

        // difficulty measured with linear functions:
        // each attacker has a linear funcion that meassures
        // x = likelihood of spawning that enemy, in the range [0,1], depending on
        // y = _waveDifficulty, has a range of 1, a unique value for each wave
        if (range > 1f) return null;
        if (range > (_waveDifficulty * 0.5f + 0.5f))
            attackerIndex = 0;
        else if (range > (_waveDifficulty - 0.2f))
            attackerIndex = 1;
        else if (range > (_waveDifficulty * 0.8f - 0.2f))
            attackerIndex = 2;
        else if (range > (_waveDifficulty - 0.4f))
            attackerIndex = 3;
        else
            attackerIndex = 4;

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
