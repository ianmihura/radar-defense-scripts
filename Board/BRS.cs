using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRS : MonoBehaviour {
    private int _normalPower;
    private int _normalMoney;

    private int _power;
    private int _money;

    public int GetPower => _power;
    public int GetMoney => _money;

    public void SetPower(int power) => _power = power;
    public void SetMoney(int money) => _money = money;

    private void Start() {
        SetPower(PlayerPrefsController.GetBRS * 2);
        SetMoney(PlayerPrefsController.GetBRS * 2);

        _normalPower = PlayerPrefsController.GetBRS;
        _normalMoney = PlayerPrefsController.GetBRS;
    }

    public void SetNormalBRS() {
        SetPower(_normalPower);
        SetMoney(_normalMoney);
    }
}