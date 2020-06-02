using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZones : MonoBehaviour {
    [SerializeField] HZ HZ;

    public void DefenderKilled(Vector3 defenderPosition) {
        _placeHZ(defenderPosition);
    }

    public void BoxExec(Vector3 boxPosition) {
        _placeHZ(boxPosition);
        _placeHZ(new Vector3(boxPosition.x + 1, boxPosition.y, boxPosition.z + 1));
        _placeHZ(new Vector3(boxPosition.x + 2, boxPosition.y, boxPosition.z + 1));
        _placeHZ(new Vector3(boxPosition.x, boxPosition.y - 1, boxPosition.z + 1));
        _placeHZ(new Vector3(boxPosition.x, boxPosition.y + 1, boxPosition.z + 1));
    }

    private void _placeHZ(Vector3 defenderPosition) {
        HZ newHeatZone = Instantiate(HZ, defenderPosition, Quaternion.identity) as HZ;
        newHeatZone.transform.SetParent(transform);
    }

    public void DissipateHeat() {
        HZ[] HZs = FindObjectsOfType<HZ>();
        
        foreach (var hz in HZs) {
            hz.PassTurn();
        }
    }
}