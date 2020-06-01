using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionButton : MonoBehaviour {
    private string _choiceId = "";

    public void SetChoiceId(string id) => _choiceId = id;
    public string GetChoiceId() => _choiceId;

    public void SetText(string text) {
        GetComponent<TextMeshProUGUI>().text = text;
    }

    private void OnMouseDown() {
        gameObject.GetComponentInParent<EventDisplayController>().ChoiceSelected(_choiceId);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }

    void OnMouseEnter() {
        GetComponent<TextMeshProUGUI>().color = new Color32(100, 100, 100, 255);
    }

    void OnMouseExit() {
        GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }
}
