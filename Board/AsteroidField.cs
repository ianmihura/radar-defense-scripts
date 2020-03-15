using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    private int _damage = 2;
    private int _life = 3;

    public int GetDamage => _damage;

    public void PassTurn() {
        _life--;

        if (_life <= 0)
            Destroy(gameObject);
    }
}
