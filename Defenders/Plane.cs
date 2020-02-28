using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    
    [SerializeField] float currentSpeed = 10f;
    [SerializeField] Bomb bomb;
    private int _dropLoop = 0;
    private int _bombsDroped = 0;

    public int GetBombDamage => bomb.GetDamage;
    
    void Update() {
        transform.Translate(currentSpeed * Vector2.right * Time.deltaTime);

        if (_dropLoop > 5){ 
            DropBomb();
            _bombsDroped++;
            _dropLoop = 0;
        } else {
            _dropLoop++;
        }

        if (_bombsDroped > 20) {
            Destroy(gameObject);
        }
    }

    private void DropBomb() {
        var oneBomb = Instantiate(
            bomb,
            //transform.position,
            new Vector2(transform.position.x - 5f, transform.position.y),
            Quaternion.identity);
    }
}