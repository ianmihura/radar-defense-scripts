using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfBoard : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Attacker>()) {
            FindObjectOfType<RelationshipsController>().AddToAll(-1);
            
            // TODO score
        }
    }
}
