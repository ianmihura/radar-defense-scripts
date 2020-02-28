using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPopupController : MonoBehaviour {
    [SerializeField] private EventPopup eventPopup;
    private EventPopup _currentEventPopup;

    private void Start() {
        _destroyEventPopup();
    }

    public void Popup(SingleEvent sEvent) {
        _destroyEventPopup();

        _currentEventPopup = Instantiate(eventPopup, transform.position, Quaternion.identity);
        _currentEventPopup.transform.parent = transform;

        _setDisplayData(sEvent);
    }

    private void _setDisplayData(SingleEvent sEvent) {
        var fields = _currentEventPopup.GetComponents<TextMeshProUGUI>();

        fields[0].text = sEvent.title;
        fields[1].text = sEvent.story;

        //var image = _currentEventPopup.GetComponent<SpriteRenderer>();

        if (sEvent.choices != null) {
            // instantiate different options
        } else {
            // instantiate Continue
        }
    }

    private void _destroyEventPopup() {
        if (_currentEventPopup)
            _currentEventPopup.DestroyMe();
    }

    // TODO recieve parameters sent by EventsController
    // text, options, etc...
    // work only with those, dont call any other external method

}
