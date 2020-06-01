using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventDisplayController : MonoBehaviour {

    private TextMeshProUGUI[] _textChildren;
    [SerializeField] private OptionButton _optionButton;
    private Transform _optionsTransform;

    void Start() {
        _textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        _optionsTransform = gameObject.transform;
    }

    public void ChoiceSelected(string choiceSelected) {
        if (choiceSelected != "close")
            FindObjectOfType<EventsController>().ManageEventAnswer(choiceSelected, choiceSelected[0] == 't');

        DestroyOptions();
    }

    private string[] _parseChoices(string choices) {
        return choices.Split(',');
    }

    public void Display(SingleEvent sEvent) {
        DestroyOptions();

        _textChildren[0].text = sEvent.title;
        _textChildren[1].text = sEvent.story;

        if (sEvent.choice_id == "" || sEvent.choice_id == null)
            _instantiateChoices(new string[] { "close" }, new string[] { "Continue" }, -2.32f);
        else
            _instantiateChoices(_parseChoices(sEvent.choice_id), _parseChoices(sEvent.choice_text), -2.32f);
    }

    private void _instantiateChoices(string[] choices_id, string[] choices_text, float ypos) {
        for (var i = 0; i < choices_id.Length; i++) {
            var instantiated = Instantiate(
                    _optionButton,
                    new Vector3(_optionsTransform.position.x, _optionsTransform.position.y + ypos, _optionsTransform.position.z),
                    Quaternion.identity);

            string id = choices_id[i].Replace(" ", "");
            string text = choices_text[i];
            text = text[0] == ' ' ? text.Substring(1) : text;

            instantiated.SetChoiceId(id);
            instantiated.SetText(text);
            instantiated.transform.SetParent(_optionsTransform);

            ypos -= 0.3f;
        }
    }

    public void DestroyOptions() {
        var options = FindObjectsOfType<OptionButton>();
        for (int i = 0; i < options.Length; i++) {
            options[i].DestroyMe();
        }
    }

}
