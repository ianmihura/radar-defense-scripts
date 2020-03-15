using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPopupController : MonoBehaviour {
    [SerializeField] private EventPopup eventPopup;
    [SerializeField] private PopupOptionButton _popupOptionButton;
    private EventPopup _currentEventPopup;
    private Transform _optionsTransform;

    private void Start() {
        _destroyEventPopup();
    }

    public void Popup(SingleEvent sEvent) {
        _destroyEventPopup();

        _currentEventPopup = Instantiate(eventPopup, transform.position, Quaternion.identity);
        _currentEventPopup.transform.parent = transform;

        _setOptionTransform();
        _setDisplayData(sEvent);
    }

    public void ChoiceSelected(string choiceSelected) {
        _destroyEventPopup();

        if (choiceSelected == "close")
            return;
        else
            FindObjectOfType<EventsController>().ManageEventAnswer(choiceSelected, choiceSelected[0] == 't');
    }

    private void _setOptionTransform() {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>()) {
            if (child.name == "Options") {
                _optionsTransform = child;
                break;
            }
        }
    }

    private void _setDisplayData(SingleEvent sEvent) {
        var fields = _currentEventPopup.GetComponentsInChildren<TextMeshProUGUI>();

        fields[0].text = sEvent.title;
        fields[1].text = sEvent.story;

        if (sEvent.choice_id == "" || sEvent.choice_id == null)
            _instantiateChoices(new string[] { "close" }, new string[] { "Continue" }, 0f);
        else
            _instantiateChoices(_parseChoices(sEvent.choice_id), _parseChoices(sEvent.choice_text), 0f);
    }

    private string[] _parseChoices(string choices) {
        return choices.Split(',');
    }
    
    private void _instantiateChoices(string[] choices_id, string[] choices_text, float ypos) {
        for (var i = 0; i < choices_id.Length; i++) {
            var instantiated = Instantiate(
                _popupOptionButton,
                new Vector3(_optionsTransform.position.x, _optionsTransform.position.y + ypos, _optionsTransform.position.z),
                Quaternion.identity);

            instantiated.SetChoiceId(choices_id[i]);
            instantiated.SetText(choices_text[i]);
            instantiated.transform.SetParent(_optionsTransform);

            ypos -= 0.52f;
        }
    }

    private void _destroyEventPopup() {
        if (_currentEventPopup)
            _currentEventPopup.DestroyMe();
    }
}
