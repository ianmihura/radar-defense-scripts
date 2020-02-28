using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour {

    private Defender _defender;
    private GameObject _defenderParent;

    private const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start() {
        CreateDefenderParent();
    }

    private void CreateDefenderParent() {
        _defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!_defenderParent)
            _defenderParent = new GameObject(DEFENDER_PARENT_NAME);
    }

    private void OnMouseDown() {
        if (!_defender) return;

        if (AttemptToPlaceDefender()) SpawnDefender(GetSquareClicked());
    }

    private bool AttemptToPlaceDefender() {
        var currencyDisplay = FindObjectOfType<CurrencyController>();
        int powerCost = _defender.GetPowerCost();
        int moneyCost = _defender.GetMoneyCost();

        if (!currencyDisplay.HasEnoughCurrency(powerCost, moneyCost)) return false;
        
        currencyDisplay.SpendCurrency(powerCost, moneyCost);
        return true;
    }

    public bool SetSelectedDefender(Defender currentDefender) {
        if (_defender == currentDefender) {
            _defender = null;
            return false;
        }
        
        _defender = currentDefender;
        return true;
    }

    private Vector3 GetSquareClicked() {
        Vector3 clickPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
        Vector3 gridPosition = SnapToGrid(worldPosition);
        Debug.Log(worldPosition);

        return gridPosition;
    }

    private Vector3 SnapToGrid(Vector3 worldPosition) {
        float xpos = _defender.isPlane ? -5 : Mathf.RoundToInt(worldPosition.x);

        return new Vector3(xpos, Mathf.RoundToInt(worldPosition.y), -3f);
    }

    private void SpawnDefender(Vector3 gridPosition) {
        Defender newDefender = Instantiate(_defender, gridPosition, transform.rotation) as Defender;
        newDefender.transform.parent = _defenderParent.transform;
    }

}
