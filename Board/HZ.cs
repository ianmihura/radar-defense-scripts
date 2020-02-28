using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HZ : MonoBehaviour {
    private int _life = 3;
    private int _damage = 1;
    public int GetDamage => _damage;

    public void PassTurn() {
        _life--;
        
        if (_life <= 0)
            Destroy(gameObject);
    }

}