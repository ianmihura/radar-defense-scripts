using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {
    [SerializeField] private int damage = 1;
    [SerializeField] public GameObject BoxLaser;

    public int GetDamage => damage;
    
    public void Explode() {
        GetComponent<Animator>().SetTrigger("explode");
    }
}