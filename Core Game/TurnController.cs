﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {
    [SerializeField] private int _currentTurn = 0;
    private bool _isGameOver = false;
    private bool _spawning = true;
    private int _cooldownCounter;
    private void _cooldownCounterReset() => _cooldownCounter = 50;
    private void _cooldownCount() => _cooldownCounter -= 1;
    private bool _isCool => _cooldownCounter <= 0;

    private EventsController _events;
    private WaveController _waveController;
    private CurrencyController _currencyController;
    private AttackerSpawner[] _attackerSpawners;
    private HeatZones _heatZones;
    private InfoController _infoController;
    private BoardEvents _boardEvents;

    private void Start() {
        _events = FindObjectOfType<EventsController>();
        _waveController = FindObjectOfType<WaveController>();
        _currencyController = FindObjectOfType<CurrencyController>();
        _attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        _heatZones = FindObjectOfType<HeatZones>();
        _infoController = FindObjectOfType<InfoController>();
        _boardEvents = FindObjectOfType<BoardEvents>();

        _cooldownCounterReset();

        StartCoroutine(_firstEvent());
    }

    private void Update() {
        if (_isCool && Input.GetKeyDown(KeyCode.Space) && !_isGameOver)
            StartCoroutine(NextTurn());

        _cooldownCount();
    }

    private void OnMouseDown() {
        if (_isCool && !_isGameOver)
            StartCoroutine(NextTurn());
    }

    IEnumerator _firstEvent() {
        yield return new WaitForSeconds(1.5f);
        _events.NextEvent(0, 0);
    }

    IEnumerator NextTurn() {
        _cooldownCounterReset();

        /* ENEMY */
        if (!_boardEvents.GetDustStorm) {
            _attackerSpawn();
            _attackerWalk();
        }

        /* PLAYER */
        if (!_boardEvents.GetThunderStorm) {
            _currencyController.Produce();
            _defenderWalk();
            _defenderFire();
        }

        /* GAME LOGIC & EVENT */
        _heatZones.DissipateHeat();
        _infoController.SetInfo(null);
        _boardEvents.PassTurn();

        yield return new WaitForSeconds(1.5f);
        
        _loadNextTurn();
    }

    private void _loadNextTurn() {
        _currentTurn++;
        FindObjectOfType<ScoreController>().AddPlayerTurn();

        if (_waveController.IsLoadNextWave())
            //TODO wave transition animation
            _nextWave();

        _events.NextEvent(_waveController.GetCurrentWave(), _currentTurn);
    }

    private void _nextWave() {
        foreach (var spawner in _attackerSpawners)
            spawner.UpdateWaveDifficulty();

        _currentTurn = 0;
        _waveController.NextWave();

        SetSpawning(true);
    }

    private void _attackerSpawn() {
        if (_spawning)
            foreach (var spawner in _attackerSpawners)
                spawner.SpawnAttackers();
    }

    private void _attackerWalk() {
        var attackers = FindObjectsOfType<Attacker>();

        foreach (var attacker in attackers)
            attacker.Walk();
    }

    private void _defenderWalk() {
        var defenders = FindObjectsOfType<Defender>();

        foreach (var defender in defenders)
            defender.Walk();
    }

    private void _defenderFire() {
        var shooters = FindObjectsOfType<Shooter>();

        foreach (var shooter in shooters)
            shooter.Fire();
    }

    public void SetSpawning(bool spawning) {
        _spawning = spawning;
    }

    public int GetCurrentTurn() {
        return _currentTurn;
    }

    public void LoadRelationshipGameOver(string relationshipIndex, bool isLow) {
        _isGameOver = true;
        _events.GetGameOverEvent(relationshipIndex, isLow);
    }

    public IEnumerator LoadGameOver() {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    }
}