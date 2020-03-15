using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTower : MonoBehaviour {
    private Base _base;
    CurrencyController _currencyController;
    [SerializeField] private GameObject _warning;
    private GameObject _currentWarningSign;
    [SerializeField] public Sprite BrokenControlTower;

    [SerializeField] private int _health = 4;
    [SerializeField] private bool _isActive = true;
    [SerializeField] private bool _isFixable;

    private int _explosionDamage = 5;
    const int MAX_HEALTH = 4;
    [SerializeField] private int _powerCost = 5;
    [SerializeField] private int _moneyCost = 15;

    public bool IsFixable => _isFixable;
    public bool IsActive => _isActive;

    public int PowerCost => _powerCost;
    public int MoneyCost => _moneyCost;
    public int GetHealth => _health;
    public int GetDamage => _explosionDamage;

    public int GetCurrentHealth() {
        return _health;
    }
    
    public void DealDamage(int damage) {
        _health -= damage;

        _updateTowerHealth();
    }

    public void FixTower() {
        if (!_isFixable || !_currencyController.HasEnoughCurrency(_powerCost, _moneyCost)) {
            return;
        }

        _currencyController.SpendCurrency(_powerCost, _moneyCost);
        _health++;
        _updateTowerHealth();
    }

    private void OnMouseDown() {
        FindObjectOfType<InfoController>().SetInfo(gameObject);
    }

    private void _updateTowerHealth() {
        if (_health >= MAX_HEALTH) {
            _health = MAX_HEALTH;

            _activateTower(true);
            _destroyCurrentWarningSign();

        } else if (_health <= 0) {
            _health = 0;

            _destroyCurrentWarningSign();
            _destroyTower();

        } else {
            _signalWarning();
            
            _activateTower(false);
        }
        
        _base.UpdateBaseHealth();
    }

    private void _destroyCurrentWarningSign() {
        if (_currentWarningSign)
            Destroy(_currentWarningSign);
    }
    
    private void _activateTower(bool activate) {
        _isActive = activate;
        _isFixable = !activate;

        if (activate) _currencyController.AddActiveTower();
        else _currencyController.SubtractActiveTower();
    }

    private void _destroyTower() {
        _isFixable = _isActive = false;

        FindObjectOfType<RelationshipsController>().AddToAll(-1);
        FindObjectOfType<BoardEvents>().TriggerBlueFlash();

        GetComponentInChildren<SpriteRenderer>().sprite = BrokenControlTower;
    }

    private void _signalWarning() {
        if (!_currentWarningSign)
            _currentWarningSign = Instantiate(_warning, transform.position, Quaternion.identity);
    }

    private void Start() {
        _isFixable = _health < MAX_HEALTH;
        _isActive = _health == MAX_HEALTH;
        
        _base = FindObjectOfType<Base>();
        _currencyController = FindObjectOfType<CurrencyController>();
    }
}