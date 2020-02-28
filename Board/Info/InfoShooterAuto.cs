using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoShooterAuto : MonoBehaviour {
    private Shooter _shooter;
    private bool _isAutomatic = false;

    private void Start() {
        _shooter = FindObjectOfType<InfoController>().GetShooterFromInfo();
        if (_shooter)
            _isAutomatic = _shooter.IsShootingAutomatic;

        _setColor();
    }

    private void OnMouseDown() {
        _isAutomatic = !_isAutomatic;
        _setColor();

        if (_shooter)
            _shooter.SetAutomaticShots(_isAutomatic);
    }

    private void _setColor() {
        if(_isAutomatic)
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255,255,255, 255);
        else
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255,255,255, 100);
    }
}