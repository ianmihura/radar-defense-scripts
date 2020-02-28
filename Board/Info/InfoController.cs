using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour {

    [SerializeField] private InfoShooter infoShooter;
    [SerializeField] private InfoBox infoBox;
    [SerializeField] private InfoDefenderButton infoDefenderButton; // Only Shooter and Exploder
    [SerializeField] private InfoResources infoResources;
    [SerializeField] private InfoDamageHealthBar infoDamageHealthBar;
    
    private InfoShooter _currentInfoShooter;
    private InfoBox _currentInfoBox;
    private InfoDefenderButton _currentInfoDefenderButton;
    private InfoResources _currentInfoResources;
    private InfoDamageHealthBar _currentInfoDamageHealthBar;

    private ControlTower _controlTower;
    private DefenderButton _defenderButton;
    private GameObject _gameObject; // generic for prefabs that have data dispersed
    
    public void SetInfo(GameObject currentGameObject) {
        _destroyInfoInstances();

        if (!currentGameObject) return;

        _gameObject = currentGameObject;

        var shooter = currentGameObject.GetComponent<Shooter>();
        var defender = currentGameObject.GetComponent<Defender>();
        var attacker = currentGameObject.GetComponent<Attacker>();
        var currencyButton = currentGameObject.GetComponent<CurrencyButton>();
        var _currencyProducer = currentGameObject.GetComponent<CurrencyProducer>();
        _defenderButton = currentGameObject.GetComponent<DefenderButton>();
        _controlTower = currentGameObject.GetComponent<ControlTower>();

        if (shooter) {
            _currentInfoShooter = Instantiate(infoShooter, transform.position, Quaternion.identity);
            _currentInfoShooter.transform.SetParent(transform);
        } else if (_controlTower || currencyButton) {
            _currentInfoResources = Instantiate(infoResources, transform.position, Quaternion.identity);
            _currentInfoResources.transform.SetParent(transform);
        } else if (_defenderButton) {
            _currentInfoDefenderButton = Instantiate(infoDefenderButton, transform.position, Quaternion.identity);
            _currentInfoDefenderButton.transform.SetParent(transform);
        } else if (attacker || _currencyProducer) {
            _currentInfoDamageHealthBar = Instantiate(infoDamageHealthBar, transform.position, Quaternion.identity);
            _currentInfoDamageHealthBar.transform.SetParent(transform);
        } else if (defender) { // Box
            _currentInfoBox = Instantiate(infoBox, transform.position, Quaternion.identity);
            _currentInfoBox.transform.SetParent(transform);
        }
    }

    private void _destroyInfoInstances() {
        if (_currentInfoShooter) _currentInfoShooter.DestroyMe();
        if (_currentInfoBox) _currentInfoBox.DestroyMe();
        if (_currentInfoDefenderButton) _currentInfoDefenderButton.DestroyMe();
        if (_currentInfoResources) _currentInfoResources.DestroyMe();
        if (_currentInfoDamageHealthBar) _currentInfoDamageHealthBar.DestroyMe();
    }

    public ControlTower GetControlTower() {
        return _controlTower;
    }
    
    public Defender GetDefenderFromButton() {
        return _defenderButton ? _defenderButton.GetDefender : null;
    }

    public Shooter GetShooterFromInfo() {
        return _gameObject.GetComponent<Shooter>();
    }

    public GameObject GetGameObject() {
        return _gameObject;
    }

    private void Start() {
        _destroyInfoInstances();
    }
}