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

    private int _shotLoop = 0;
    private bool _allowFire = false;

    public bool IsShootingAutomatic => _automaticShots > 0;
    public int GetShotsToFire => _shotsToFire;
    public int GetShotsCost => _shotsCost;
    public int GetProjectileDamage() {
        return projectile.GetComponent<Projectile>().GetDamage;
    }

    private void Start() {
        _currencyController = FindObjectOfType<CurrencyController>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent() {
        _projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!_projectileParent)
            _projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
    }

    public void LoadFire() {
        if (_currencyController.HasEnoughCurrency(_shotsCost, 0)) {
            _currencyController.SpendCurrency(_shotsCost, 0);
            _shotsToFire++;
        }
    }

    public void UnloadFire() {
        if (_shotsToFire <= 0)
            return;
        
        _currencyController.UnspendCurrency(_shotsCost, 0);
        _shotsToFire--;
    }

    public void _loadAutomaticShots() {
        for (int i = 0; i < _automaticShots; i++) {
            LoadFire();
        }
    }

    public void SetAutomaticShots(bool isAuto) {
        _automaticShots = isAuto ? _shotsToFire : 0;
    }

    private void Update() {
        if (!_allowFire) return;

        if (_shotsToFire <= 0) {
            _shotsToFire = 0;
            _allowFire = false;

            _loadAutomaticShots();
        }

        if (_shotLoop > 15){
            _fire();
            _shotsToFire--;
            _shotLoop = 0;
        } else {
            _shotLoop++;
        }
    }

    public void Fire() {
        _allowFire = true;
    }

    private void _fire() {
        GameObject newProjectile = 
            Instantiate(projectile, gun.transform.position, transform.rotation)
            as GameObject;
        newProjectile.transform.parent = _projectileParent.transform;
    }
}
