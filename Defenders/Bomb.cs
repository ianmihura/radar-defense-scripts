using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] int damage = 10;

    public int GetDamage => damage;
    
    void OnTriggerEnter2D(Collider2D collision) {
        var attacker = collision.GetComponent<Attacker>();
        var health = collision.GetComponent<Health>();

        if (attacker && health) {
            health.DealDamage(damage);
        }
    }
    
    public void Die() {
        Destroy(gameObject);
    }
}