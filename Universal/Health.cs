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
    
    public bool DealDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            Die();
            return true;
        }

        if (GetComponent<Defender>() && health == 1)
            _warningSign();

        return false;
    }

    private void _warningSign() {
        _currentWarningSign = Instantiate(_warning, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f), Quaternion.identity);
        _currentWarningSign.transform.SetParent(transform);
    }

    public void BoxExec() {
        _heatZones.BoxExec(transform.position);

        Destroy(gameObject, 0.25f);
    }

    public void Die() {
        if (GetComponent<Defender>())
            _heatZones.DefenderKilled(transform.position);

        else if (GetComponent<Attacker>()) {
            FindObjectOfType<ScoreController>().AddKill();
            GetComponent<Exploder>().Explode();
        }

        if (_currentWarningSign)
            Destroy(_currentWarningSign);
        
        Destroy(gameObject, 0.25f);
    }

    public void DestroyEscapingAttacker() {
        FindObjectOfType<ScoreController>().AddEnemyScore();
        Destroy(gameObject);
    }
}
