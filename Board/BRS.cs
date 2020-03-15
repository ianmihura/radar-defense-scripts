using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BRS : MonoBehaviour {
    private int _power = 1;
    private int _money = 1;

    private int _normalPower = 1;
    private int _normalMoney = 1;

    public int GetPower => _power;
    public int GetMoney => _money;
    
    public void SetPower(int power) => _power = power;
    public void SetMoney(int money) => _money = money;

    public void SetNormalBRS() {
        _power = _normalPower;
        _money = _normalMoney;
    }
}