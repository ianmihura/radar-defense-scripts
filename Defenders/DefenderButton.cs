using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour {
    DefenderSpawner _defenderSpawner;
    [SerializeField] Defender defenderPrefab;
    private DefenderButton[] _buttons;
    [SerializeField] GameObject defenderButtonHide;

    public Defender GetDefender => defenderPrefab;
    
    private void Start() {
        _defenderSpawner = FindObjectOfType<DefenderSpawner>();
        _buttons = FindObjectsOfType<DefenderButton>();
        _hideSprites();
    }

    private void OnMouseDown() {
        FindObjectOfType<InfoController>().SetInfo(gameObject);
        
        _hideSprites();

        float zpos = defenderPrefab.isPlane ? -5f : -0.4f;
        
        if (FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab))
            GetComponentInChildren<SpriteRenderer>().color = new Color32(255,255,255, 255);

        _moveCoreGameAreaPosition(zpos);
    }

    private void _moveCoreGameAreaPosition(float zpos) {
        Vector3 pos = _defenderSpawner.transform.position;
        _defenderSpawner.transform.position = new Vector3(pos.x, pos.y, zpos);
    }

    private void _hideSprites() {
        foreach (DefenderButton button in _buttons) {
            button.GetComponentInChildren<SpriteRenderer>().color = new Color32(255,255,255, 0);
        }
    }

    public void ShowDefenderButton() {
        if (defenderButtonHide)
            Destroy(defenderButtonHide);
    }
}