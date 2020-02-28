using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] int damage = 1;
    [SerializeField] float projectileSpeed = 0.4f;

    public int GetDamage => damage;
    
    private void Update() {
        transform.Translate(new Vector2(projectileSpeed, 0f));
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<HZ>()) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
        var attacker = collision.GetComponent<Attacker>();
        var health = collision.GetComponent<Health>();
        
        if (attacker && health) {
            health.DealDamage(damage);
            Destroy(gameObject);

        } else if (collision.GetComponent<HZ>()) {
            Destroy(gameObject);

        } else if (collision.GetComponent<EndOfBoard>()) {
            Destroy(gameObject);

        }
    }
}
