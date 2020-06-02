using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoDamageHealthBar : MonoBehaviour {

    private GameObject _gameObject;
    private Attacker _attacker;
    private CurrencyProducer _currencyProducer;
    private Slider _healthSlider;

    private void Start() {
        _gameObject = FindObjectOfType<InfoController>().GetGameObject();
        _currencyProducer = FindObjectOfType<CurrencyProducer>();
        _attacker = _gameObject.GetComponent<Attacker>();

        _healthSlider = GetComponentInChildren<Slider>();
        _healthSlider.maxValue = _gameObject.GetComponent<Health>()
            ? _gameObject.GetComponent<Health>().GetMaxHealth
            : 10;

        UpdateValues();
    }

    public void UpdateValues() {

        int health = _gameObject.GetComponent<Health>() 
            ? _gameObject.GetComponent<Health>().GetHealth
            : 10;

        _healthSlider.value = health;

        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();

        if (_attacker) {
            var damage = _gameObject.GetComponent<Exploder>().GetDamage.ToString();
            textChildren[0].text = damage;

        } else if (_currencyProducer) {
            textChildren[0].text = "";
            textChildren[1].text = "";

        } else { // Plane
            textChildren[0].text = "20";
        }
    }


    public void DestroyMe() {
        Destroy(gameObject);
    }
}
