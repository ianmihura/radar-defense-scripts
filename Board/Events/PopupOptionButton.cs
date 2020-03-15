using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupOptionButton : MonoBehaviour {
    private string _choiceId = "";

    public void SetChoiceId(string id) => _choiceId = id;
    public string GetChoiceId() => _choiceId;

    public void SetText(string text) {
        GetComponent<TextMeshProUGUI>().text = text;
    }

    private void OnMouseDown() {
        gameObject.GetComponentInParent<EventPopupController>().ChoiceSelected(_choiceId);
    }
}
