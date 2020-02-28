using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyController : MonoBehaviour {

    private int _powerStations = 0;
    private int _moneyStations = 0;

    private const int ControlTowerPowerAffordance = 1;
    private const int ControlTowerMoneyAffordance = 1;

    private const int PowerAffordance = 1;
    private const int MoneyAffordance = 1;
    
    private int _powerTotalNextTurn = 1;
    private int _moneyTotalNextTurn = 1;
    
    [SerializeField] private int power = 1000;
    [SerializeField] private int money = 1000;
    
    [SerializeField] TextMeshProUGUI _powerText;
    [SerializeField] TextMeshProUGUI _powerTextToAdd;
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _moneyTextToAdd;

    public int GetPowerAffordance => PowerAffordance;
    public int GetMoneyAffordance => MoneyAffordance;
    public int GetControlTowerPowerAffordance => ControlTowerPowerAffordance;
    public int GetControlTowerMoneyAffordance => ControlTowerMoneyAffordance;
    private int _totalActiveTowers = 0;

    public void AddActiveTower() {
        _totalActiveTowers++;
        _updateToAddAmount();
    }

    public void SubtractActiveTower() {
        _totalActiveTowers--;
        _updateToAddAmount();
    } 

    
    private void Start() {
        _updateToAddAmount();
    }
    
    public void Produce() {
        power += _powerTotalNextTurn;
        money += _moneyTotalNextTurn;
        
        _updateDisplay();
    }

    public void PowerStationSpawned() {
        _powerStations++;
        _updateToAddAmount();
    }

    public void PowerStationDestroyed() {
        _powerStations--;
        _updateToAddAmount();
    }

    public void MoneyStationSpawned() {
        _moneyStations++;
        _updateToAddAmount();
    }

    public void MoneyStationDestroyed() {
        _moneyStations--;
        _updateToAddAmount();
    }

    public bool HasEnoughCurrency(int powerAmount, int moneyAmount) {
        return power >= powerAmount && money >= moneyAmount;
    }

    public void SpendCurrency(int powerAmount, int moneyAmount) {
        power -= powerAmount;
        money -= moneyAmount;
        
        _updateDisplay();
    }
    
    public void UnspendCurrency(int powerAmount, int moneyAmount) {
        power += powerAmount;
        money += moneyAmount;
        
        _updateDisplay();
    }

    private void _updateToAddAmount() {
        _powerTotalNextTurn = _moneyTotalNextTurn = 0;
        
        for (var i = 0; i < _powerStations; i++) {
            _powerTotalNextTurn += PowerAffordance;
        }
        for (var i = 0; i < _moneyStations; i++) {
            _moneyTotalNextTurn += MoneyAffordance;
        }
        
        _powerTotalNextTurn += _totalActiveTowers * ControlTowerPowerAffordance;
        _moneyTotalNextTurn += _totalActiveTowers * ControlTowerMoneyAffordance;

        _powerTotalNextTurn += FindObjectOfType<BRS>().GetPower;
        _moneyTotalNextTurn += FindObjectOfType<BRS>().GetMoney;

        _updateDisplay();
    }
    
    private void _updateDisplay() {
        _powerText.text = power.ToString();
        _moneyText.text = money.ToString();

        _powerTextToAdd.text = "+" + _powerTotalNextTurn;
        _moneyTextToAdd.text = "+" + _moneyTotalNextTurn;
    }
}