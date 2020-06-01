using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Shooter : MonoBehaviour {

    [SerializeField] GameObject projectile, gun;
    [SerializeField] private int _shotsToFire = 0;
    [SerializeField] private int _automaticShots;
    [SerializeField] private int _shotsCost = 1;
    private CurrencyController _currencyController;
    private GameObject _projectileParent;

    private const string PROJECTILE_PARENT_NAME = "Projectiles";

    private int _shotLoopCounter = 0;
    private bool _allowFire = false;

    public bool IsShootingAutomatic => _automaticShots > 0;
    public int GetShotsToFire => _shotsToFire;
    public int GetShotsCost => _shotsCost;
    public int GetProjectileDamage() {
        return projectile.GetComponent<Projectile>().GetDamage;
    }

    private void Start() {
        _currencyController = FindObjectOfType<CurrencyController>();
        _createProjectileParent();
    }

    private void _createProjectileParent() {
        _projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!_projectileParent)
            _projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
    }

    public void LoadFire() {
        if (!_currencyController.HasEnoughCurrency(_shotsCost, 0))
            return;

        _currencyController.SpendCurrency(_shotsCost, 0);
        _shotsToFire++;
        SetAutomaticShots();
    }

    public void UnloadFire() {
        if (_shotsToFire <= 0)
            return;
        
        _currencyController.UnspendCurrency(_shotsCost, 0);
        _shotsToFire--;
        SetAutomaticShots();
    }

    public void SetAutomaticShots() {
        _automaticShots = FindObjectOfType<InfoShooterAuto>().IsAutoEnabled
            ? _shotsToFire : 0;
    }

    public void Fire() {
        _allowFire = true;
    }

    private void Update() {
        if (!_allowFire) return;

        if (_shotsToFire <= 0) {
            _shotsToFire = 0;
            _allowFire = false;

            _loadAutomaticShots();
        }

        _shootLoop();
    }

    private void _shootLoop() {
        // 15 update loops : time between shots
        if (_shotLoopCounter < 15) {
            _shotLoopCounter++;
            return;
        }
        
        _fire();
        _shotsToFire--;
        _shotLoopCounter = 0;
    }

    private void _fire() {
        GameObject newProjectile = 
            Instantiate(projectile, gun.transform.position, transform.rotation)
            as GameObject;
        newProjectile.transform.parent = _projectileParent.transform;
    }

    private void _loadAutomaticShots() {
        for (int i = 0; i < _automaticShots; i++) {
            if (!_currencyController.HasEnoughCurrency(_shotsCost, 0))
                return;

            _currencyController.SpendCurrency(_shotsCost, 0);
            _shotsToFire++;
        }
    }
}
