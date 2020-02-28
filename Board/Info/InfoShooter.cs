using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoShooter : MonoBehaviour {

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

        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        textChildren[1].text = _gameObject.GetComponent<Shooter>().GetShotsToFire.ToString();
        textChildren[5].text = _gameObject.GetComponent<Shooter>().GetShotsCost.ToString() + "P";
    }

    public void LoadFire() {
        _gameObject.GetComponent<Shooter>().LoadFire();
        UpdateValues();
    }

    public void UnloadFire() {
        _gameObject.GetComponent<Shooter>().UnloadFire();
        UpdateValues();
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}