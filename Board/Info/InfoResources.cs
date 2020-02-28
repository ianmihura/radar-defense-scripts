using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoResources : MonoBehaviour {

    private CurrencyController _currencyController;
    private ControlTower _controlTower;
    private Defender _defender;

    private void Start() {
        _defender = FindObjectOfType<InfoController>().GetDefenderFromButton();
        _currencyController = FindObjectOfType<CurrencyController>();
        _controlTower = FindObjectOfType<InfoController>().GetControlTower();

        UpdateValues();
    }

    public void UpdateValues() {
        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();

        if (_controlTower) {
            textChildren[0].text = _controlTower.GetHealth < 1 ? "--" : _controlTower.GetHealth.ToString();

            if (_controlTower.IsFixable) {
                textChildren[2].text = "";
                textChildren[3].text = "";
                textChildren[4].text = "";
                textChildren[5].text = "";

                textChildren[6].text = _controlTower.PowerCost.ToString() + "P";
                textChildren[7].text = _controlTower.MoneyCost.ToString() + "M";

                textChildren[8].text = "FIX";

            } else if (_controlTower.IsActive) {
                textChildren[2].text = "+" + _currencyController.GetControlTowerMoneyAffordance;
                textChildren[3].text = "POWER";
                textChildren[4].text = "+" + _currencyController.GetControlTowerPowerAffordance;
                textChildren[5].text = "MONEY";

                textChildren[6].text = "";
                textChildren[7].text = "";
                textChildren[8].text = "";

            } else { // dead
                textChildren[2].text = "";
                textChildren[3].text = "";
                textChildren[4].text = "";
                textChildren[5].text = "";
                textChildren[6].text = "";
                textChildren[7].text = "";
                textChildren[8].text = "";
            }
        
        } else { // Currency Button
            textChildren[0].text = _defender.GetComponent<Health>().GetHealth.ToString();

            var currencyProducer = FindObjectOfType<InfoController>().GetDefenderFromButton().GetComponent<CurrencyProducer>();
            
            // currency producer does not belong to button -- find boolean IsPower another way

            textChildren[2].text = currencyProducer.IsPower
                ? "+" + _currencyController.GetPowerAffordance
                : "+" + _currencyController.GetMoneyAffordance;
            textChildren[3].text = currencyProducer.IsPower ? "POWER" : "MONEY";

            // Second line of affordances
            textChildren[4].text = textChildren[5].text = "";

            textChildren[6].text = _defender.GetPowerCost() + "P";
            textChildren[7].text = _defender.GetMoneyCost() + "M";

            textChildren[8].text = "";
        }
    }

    private void OnMouseDown() {
        if (_controlTower)
            _controlTower.FixTower();
        UpdateValues();
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
