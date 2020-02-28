using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlash : MonoBehaviour {
    private void Start() {
        HarmAllObjects();
    }

    public void HarmAllObjects() {
        Health[] healthArray = FindObjectsOfType<Health>();
        foreach (var healthElement in healthArray) {
            healthElement.DealDamage(1);
        }
    }
    
    public void DestroyMe() {
        Destroy(gameObject);
    }
}