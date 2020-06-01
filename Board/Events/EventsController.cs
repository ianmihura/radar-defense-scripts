using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EventsController : MonoBehaviour {
    private EventListController eventListController;
    private EventDisplayController eventDisplayController;
    private WeatherSignController _weatherSignController;
    private RelationshipsController _relationshipsController;
    private CurrencyController _currencyController;
    private BoardEvents _boardEvents;

    // Push events into queue for narrative continuation
    private SingleEvent _pendingEvent;
 
    private SingleEvent _gameOverEvent;

    private void Start() {
        eventListController = FindObjectOfType<EventListController>();
        eventDisplayController = FindObjectOfType<EventDisplayController>();
        _weatherSignController = FindObjectOfType<WeatherSignController>();
        _relationshipsController = FindObjectOfType<RelationshipsController>();
        _currencyController = FindObjectOfType<CurrencyController>();
        _boardEvents = FindObjectOfType<BoardEvents>();
    }

    public void NextEvent(int currentWave, int currentTurn) {
        SingleEvent sEvent;

        if (currentTurn == 0) {
            ShootStartEvents(currentWave);
            return;
        
        } else if (Random.Range(0, 5) < 1) {
            // 1 in 5 chances you get a random event (board event included)
            if (_pendingEvent != null)
                sEvent = _pendingEvent;
            else
                sEvent = eventListController.GetRandomEvent();

        } else if (Random.Range(0, 10) < 1)
            // 1 in 10 chances you get a board event
            sEvent = eventListController.GetBoardEvent();

        else
            sEvent = null;

        _shootEvent(sEvent);
    }

    public void ShootStartEvents(int currentWave) {
        SingleEvent sEvent;

        switch (currentWave) {
            case 0:
            sEvent = eventListController.GetStartEvent();
            break;

            case 1:
            sEvent = eventListController.GetCutBRSEvent();
            FindObjectOfType<BRS>().SetNormalBRS();
            break;

            case 2:
            sEvent = eventListController.GetX71Event();
            break;

            case 3:
            sEvent = eventListController.GetP36Event();
            break;

            default:
            sEvent = null;
            break;
        }

        _shootEvent(sEvent);
    }

    private void _shootEvent(SingleEvent sEvent) {
        if (sEvent == null)
            return;
        
        _triggerEventDisplay(sEvent);
        _manageEventAffects(sEvent);
    }

    private void _triggerEventDisplay(SingleEvent sEvent) {
        eventDisplayController.Display(sEvent);
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

    public void GetGameOverEvent(string reationshipIndex, bool isLow) {
        _gameOverEvent = eventListController.GetGameOver(reationshipIndex, isLow);
        _shootEvent(_gameOverEvent);
    }
}
