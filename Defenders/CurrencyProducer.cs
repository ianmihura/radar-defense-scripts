using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProducer : MonoBehaviour {
    [SerializeField] public bool IsPower = false;

    private void Awake() {
        if (IsPower) {
            FindObjectOfType<CurrencyController>().PowerStationSpawned();
        } else {
            FindObjectOfType<CurrencyController>().MoneyStationSpawned();
        }
    }
    private void OnDestroy() {
        if (IsPower) {
            FindObjectOfType<CurrencyController>().PowerStationDestroyed();
        } else {
            FindObjectOfType<CurrencyController>().MoneyStationDestroyed();
        }
    }
}