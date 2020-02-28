using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {
    [SerializeField] int powerCost = 1;
    [SerializeField] int moneyCost = 1;
    [SerializeField] public bool isPlane = false;
    [SerializeField] public bool isX71 = false;

    public int GetPowerCost() {
        return powerCost;
    }
    
    public int GetMoneyCost() {
        return moneyCost;
    }

    public void OnMouseDown() {
        FindObjectOfType<InfoController>().SetInfo(gameObject);
        if (isPlane) {
            // TODO: only rows available
        }  
    }
}