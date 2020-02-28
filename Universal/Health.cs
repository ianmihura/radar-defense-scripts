using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] int health = 2;
    [SerializeField] int maxHealth = 2;

    [SerializeField] private GameObject _warning;
    private GameObject _currentWarningSign;

    private HeatZones _heatZones;

    private void Start() {
        _heatZones = FindObjectOfType<HeatZones>();
    }

    public int GetHealth => health;
    public int GetMaxHealth => maxHealth;
    
    public void DealDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        } else if (GetComponent<Defender>() && health == 1) {
            _warningSign();
        }
    }

    private void _warningSign() {
        _currentWarningSign = Instantiate(_warning, transform.position, Quaternion.identity);
    }

    public void Die() {
        if (GetComponent<Defender>())
            _heatZones.DefenderKilled(transform.position);

        else if (GetComponent<Attacker>())
            GetComponent<Exploder>().Explode();

        if (_currentWarningSign)
            Destroy(_currentWarningSign);
        
        Destroy(gameObject, 0.25f);
    }

    public void DestroyEscapingAttacker() {
        Destroy(gameObject);
    }
}
