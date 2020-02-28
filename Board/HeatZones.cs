using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZones : MonoBehaviour {
    [SerializeField] HZ HZ;

    private List<Vector3> HZPositions;

    private void Start() {
        HZPositions = new List<Vector3>();
    }

    public void DefenderKilled(Vector3 defenderPosition) {
        _handleAdjacent();
        
        _placeHZ(defenderPosition);
    }

    private void _handleAdjacent() {
        // TODO: handle adjacent
        // prolongue life of adjacent
        // if 2 encompass a 3rd, that 3rd becomes a heatzone
    }

    private void _hasAdjacent() {
        // HZPositions.Contains(defenderPosition);
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