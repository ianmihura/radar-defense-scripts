using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Attacker : MonoBehaviour {

    public float BlockDistance = 1.0f;
    public const float BLOCK_DISTANCE = 1.0f;
    private bool _advance = false;
    private float _startPosition = 0f;

    private WaveController _waveController;

    private void Awake() {
        FindObjectOfType<WaveController>().AttackerSpawned();
        _waveController = FindObjectOfType<WaveController>();
    }

    private void OnDestroy() {
        if (_waveController)
            _waveController.AttackerKilled();
    }

    private void Update() {
        if (_advance) {
            transform.Translate(Vector2.left * Time.deltaTime);
            _advance = (_startPosition - transform.position.x) < BLOCK_DISTANCE;
        }
    }

    public void OnMouseDown() {
        FindObjectOfType<InfoController>().SetInfo(gameObject);
    }

    public void Walk() {
        _startPosition = transform.position.x;

        _advance = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        var defender = otherObject.GetComponent<Defender>();
        var controlTower = otherObject.GetComponent<ControlTower>();
        var health = GetComponent<Health>();
        var damage = GetComponent<Exploder>().GetDamage;

        if (controlTower && controlTower.GetCurrentHealth() <= 0) {
            return;

        } else if (controlTower) {
            controlTower.DealDamage(damage);

            GetComponent<Exploder>().Explode();
            health.Die();

        } else if (defender && !defender.isPlane) {
            health.DealDamage(otherObject.GetComponent<Exploder>().GetDamage);
            var defenderIsDead = otherObject.GetComponent<Health>().DealDamage(damage);
            if (!defenderIsDead) health.Die();

        } else if (otherObject.GetComponent<HZ>()) {
            health.DealDamage(otherObject.GetComponent<HZ>().GetDamage);

        } else if (otherObject.GetComponent<EndOfBoard>()) {
            health.DestroyEscapingAttacker();

        } else if (otherObject.GetComponent<AsteroidField>()) {
            health.DealDamage(otherObject.GetComponent<AsteroidField>().GetDamage);
        }
    }
}