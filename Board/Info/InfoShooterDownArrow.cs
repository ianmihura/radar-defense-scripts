using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoShooterDownArrow : MonoBehaviour {
    private InfoShooter _infoShooter;
    
    private void Start() {
        _infoShooter = FindObjectOfType<InfoShooter>();
    }

    private void OnMouseDown() {
        _infoShooter.UnloadFire();
    }
}