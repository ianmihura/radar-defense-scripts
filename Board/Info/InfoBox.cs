using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour {

    private GameObject _gameObject;
    private Slider _healthSlider;

    private void Start() {
        _gameObject = FindObjectOfType<InfoController>().GetGameObject();

        _healthSlider = GetComponentInChildren<Slider>();
        _healthSlider.maxValue = _gameObject.GetComponent<Health>().GetMaxHealth;

        UpdateValues();
    }

    public void UpdateValues() {
        int health = _gameObject.GetComponent<Health>().GetHealth;
        _healthSlider.value = health;
    }

    private void OnMouseDown() {
        if (!_gameObject)
            return;
        _gameObject.GetComponent<Health>().BoxExec();
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }

}
