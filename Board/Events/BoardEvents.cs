using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEvents : MonoBehaviour {
    
    [SerializeField] private BlueFlash blueFlashPrefab;
    [SerializeField] private AsteroidField asteroidField;

    private BlueFlash _currentBlueFlash;
    private WeatherSignController _weatherSignController;

    private bool _isThunderStorm = false;
    private bool _isDustStorm = false;

    public bool GetThunderStorm => _isThunderStorm;
    public bool GetDustStorm => _isDustStorm;

    public void SetThunderStorm() => _isThunderStorm = true;
    public void SetDustStorm() => _isDustStorm = true;
    public void DestroySigns() => _isDustStorm = _isThunderStorm = false;

    private void Start() {
        _destroyBoardEvents();
        _weatherSignController = FindObjectOfType<WeatherSignController>();
    }

    private void _destroyBoardEvents() {
        _destroyInstantBoardEvents();
    }

    private void _destroyInstantBoardEvents() {
        if (_currentBlueFlash)
            _currentBlueFlash.DestroyMe();
    }
    
    public void TriggerBlueFlash() {
        _destroyInstantBoardEvents();
        
        _currentBlueFlash = Instantiate(blueFlashPrefab, transform.position, Quaternion.identity);
    }

    public void TriggerAsteroidField(Vector3 position) {
        Instantiate(asteroidField, position, Quaternion.identity);
    }

    public void PassTurn() {
        var asteroidField = FindObjectOfType<AsteroidField>();
        if (asteroidField)
            asteroidField.PassTurn();

        _weatherSignController.PassTurn();
    }
}
