using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoShooterAuto : MonoBehaviour {
    private bool _autoEnabled = false;
    public bool IsAutoEnabled => _autoEnabled;

    private void Start() {
        var shooter = FindObjectOfType<InfoController>().GetShooterFromInfo();
        if (shooter) _autoEnabled = shooter.IsShootingAutomatic;

        _setColor();
    }

    private void OnMouseDown() {
        _autoEnabled = !_autoEnabled;
        _setColor();

        var shooter = FindObjectOfType<InfoController>().GetShooterFromInfo();
        if (shooter) shooter.SetAutomaticShots();
    }

    private void _setColor() {
        if(_autoEnabled)
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255,255,255, 255);
        else
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255,255,255, 100);
    }
}