using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EventsController : MonoBehaviour {
    private EventListController eventListController;
    private EventPopupController eventPopupController;
    private WeatherSignController _weatherSignController;
    private RelationshipsController _relationshipsController;
    private CurrencyController _currencyController;
    private BoardEvents _boardEvents;

    // Push events into queue for narrative continuation
    private SingleEvent _pendingEvent;

    private void Start() {
        eventListController = FindObjectOfType<EventListController>();
        eventPopupController = FindObjectOfType<EventPopupController>();
        _weatherSignController = FindObjectOfType<WeatherSignController>();
        _relationshipsController = FindObjectOfType<RelationshipsController>();
        _currencyController = FindObjectOfType<CurrencyController>();
        _boardEvents = FindObjectOfType<BoardEvents>();
    }

    //TODO execute changeBoardMetrics in EventSidebarController

    public void NextEvent(int currentWave, int currentTurn) {
        SingleEvent sEvent;

        if (currentTurn == 0 && currentWave == 0) {
            sEvent = eventListController.GetStartEvent();

        } else if (currentTurn == 0 && currentWave == 1) {
            sEvent = eventListController.GetCutBRSEvent();
            FindObjectOfType<BRS>().SetNormalBRS();

        } else if (Random.Range(0, 5) > 10) {
        //} else if (Random.Range(0, 5) < 1) {
            // 1 in 5 chances you get a random event
            
            if (_pendingEvent != null) {
                sEvent = _pendingEvent;
            } else {
                sEvent = eventListController.GetRandomEvent();
            }

        } else {
            sEvent = null;
        }

        _shootEvent(sEvent);
    }

    private void _shootEvent(SingleEvent sEvent) {
        if (sEvent == null)
            return;
        
        _manageEventAffects(sEvent);
        _triggerEventPopup(sEvent);
    }

    private void _triggerEventPopup(SingleEvent sEvent) {
       eventPopupController.Popup(sEvent);
    }

    private void _manageEventAffects(SingleEvent sEvent) {

        switch (sEvent.board_event) {
            case "dust":
            _weatherSignController.ShowDustSign();
            break;

            case "thunder":
            _weatherSignController.ShowThunderSign();
            break;

            case "asteroids":
            _boardEvents.TriggerAsteroidField(new Vector3(Random.Range(1, 10), Random.Range(1, 4), 2));
            break;

            case "blue_flash":
            _boardEvents.TriggerBlueFlash();
            break;

            case "defender":
                // TODO
            break;

            default:
            break;
        }
        
        _relationshipsController.AddMorale(sEvent.morale);
        _relationshipsController.AddComradery(sEvent.comradery);
        _relationshipsController.AddLoyalty(sEvent.loyalty);

        _currencyController.SpendCurrency(sEvent.power, sEvent.money);
    }

    public void ManageEventAnswer(string eventId, bool isPending) {
        if (isPending) _pendingEvent = eventListController.GetEventById(eventId);
        else  _shootEvent(eventListController.GetEventById(eventId));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _boardEvents.TriggerAsteroidField(new Vector3(Random.Range(1, 10), Random.Range(1, 4), 2));
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            return;
        else if (Input.GetKeyDown(KeyCode.RightAlt))
            return;
    }
}
