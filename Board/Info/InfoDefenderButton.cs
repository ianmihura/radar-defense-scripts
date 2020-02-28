using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDefenderButton : MonoBehaviour {

    private Defender _defender;
    private CurrencyController _currencyController;

    private void Start() {
        _defender = FindObjectOfType<InfoController>().GetDefenderFromButton();
        _currencyController = FindObjectOfType<CurrencyController>();

        UpdateValues();
    }

    public void UpdateValues() {

        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        textChildren[0].text = _defender.GetComponent<Health>().GetHealth.ToString();

        textChildren[4].text = _defender.GetPowerCost().ToString() + "P";
        textChildren[5].text = _defender.GetMoneyCost().ToString() + "M";

        var shooter = _defender.GetComponent<Shooter>();
        var plane = _defender.isPlane;

        if (shooter) {
            textChildren[2].text = shooter.GetProjectileDamage().ToString();

        } else if (plane) {
            textChildren[0].text = "";
            textChildren[1].text = "";
            textChildren[2].text = _defender.GetComponent<Plane>().GetBombDamage.ToString();

            _movePrefab(textChildren[2]);
            _movePrefab(textChildren[3]);

        } else {
            textChildren[2].text = _defender.GetComponent<Exploder>().GetDamage.ToString();
        }
    }

    private void _movePrefab(TextMeshProUGUI text) {
        text.transform.position = new Vector3(
            text.transform.position.x,
            text.transform.position.y + 0.1f,
            text.transform.position.z);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}