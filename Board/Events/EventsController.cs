using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EventsController : MonoBehaviour {
    private EventListController eventListController;
    private EventPopupController eventPopupController;

    // Push events into queue for narrative continuation
    private SingleEvent _pendingEvent;

    private void Start() {
        eventListController = FindObjectOfType<EventListController>();
        eventPopupController = FindObjectOfType<EventPopupController>();
    }

    //TODO execute changeBoardMetrics in EventSidebarController

    public void NextEvent(int currentWave, int currentTurn) {
        SingleEvent sEvent;

        if (currentTurn == 0 && currentWave == 0) {
            sEvent = eventListController.GetStartEvent();
        } else if (currentTurn == 0 && currentWave == 1) {
            sEvent = eventListController.GetCutBRSEvent();

        } else if (Random.Range(0, 5) < 1) {
            // 1 in 5 chances you get a random event
            
            if (_pendingEvent != null) {
                sEvent = _pendingEvent;
            } else {
                sEvent = eventListController.GetRandomEvent();
            }
        } else {
            sEvent = null;
        }

        _triggerEventPopup(sEvent);
    }

    private void _triggerEventPopup(SingleEvent sEvent) {
        if (sEvent != null)
            eventPopupController.Popup(sEvent);
    }
}
