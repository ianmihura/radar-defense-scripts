using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListController : MonoBehaviour {

    [SerializeField] public TextAsset jsonEventList;
    private RelationshipsController relationshipsController;

    private EventGroupList _eventGroupList;
    private void Start() {
        relationshipsController = FindObjectOfType<RelationshipsController>();
        _eventGroupList = JsonUtility.FromJson<EventGroupList>(jsonEventList.text);
    }

    public EventGroupList GetEventList() {
        return _eventGroupList;
    }

    public EventGroup GetEventGroup(int eventId) {
        return GetEventList().eventGroupList[eventId];
    }

    public SingleEvent GetStartEvent() {
        return GetEventById("0000");
    }

    public SingleEvent GetCutBRSEvent() {
        return GetEventById("0010");
    }

    public SingleEvent GetEventById(string id) {
        int group = int.Parse(id.Substring(0, 1));
        int evt = int.Parse(id.Substring(1, 2));
        int order = int.Parse(id.Substring(3, 1));

        // evt is the lowest place (in the array) that the Event can be in
        SingleEvent returnEvent = GetEventGroup(group).events[evt];

        // get the real index (may be higher than inicial returnEvent)
        // int returnEventIndex = _getEventIndex(returnEvent, evt, group);

        // (order) amount more over the inicial index
        // returnEvent = GetEventGroup(group).events[returnEventIndex + order];

        if (returnEvent.id != int.Parse(id)) {
            Debug.Log("found wrong event");
            Debug.Log(returnEvent.id);
        }
        Debug.Log(id);

        return returnEvent;
    }

    // TODO: random event
    public SingleEvent GetRandomEvent() {
        return GetEventById(_getNextEventId());
    }

    // 2 in 3 chances you get a Relationship Event
    // 1 in 3 chances you get a General Event
    private string _getNextEventId() {
        string group = Random.Range(0, 3) < 1 ? "1" : _getRelationshipEvent();
        string evt = _getRandomEvtByGroup(int.Parse(group));

        return group + evt + "0";
    }

    // 1 in 4 chances you get Comradery
    // 1 in 4 chances you get Loyalty
    // 1 in 4 chances you get Morale
    // 1 in 4 chances you get own -> highest||lowest
    private string _getRelationshipEvent() {
        switch (Random.Range(0, 4)) {
            case 0: //comradery
            return "2";

            case 1: //loyalty
            return "3";

            case 2: //morale
            return "4";

            case 3: //own
            return relationshipsController.GetClosestLevel();

            default:
            return "1";
        }
    }

    private string _getRandomEvtByGroup(int group) {
        int totalEventsForGroup = 0;

        // Total unique non consecutive events
        for (int i = 0; GetEventGroup(group).events.Length > i; i++)
            if (_getOrder(GetEventGroup(group).events[i]) == "0")
                totalEventsForGroup++;

        var randomEvt = Random.Range(0, totalEventsForGroup).ToString();
        randomEvt = randomEvt.Length == 2 ? randomEvt : "0" + randomEvt;

        return randomEvt;
    }

    private int _getEventIndex(SingleEvent returnEvent, int evt, int group) {
        int returnEventIndex;

        for (returnEventIndex = evt; _getEvt(returnEvent) != evt; returnEventIndex++)
            returnEvent = GetEventGroup(group).events[returnEventIndex];

        return returnEventIndex;
    }

    private int _getEvt(SingleEvent sEvent) {
        return int.Parse(sEvent.id.ToString().Substring(1, 2));
    }

    private string _getOrder(SingleEvent sEvent) {
        return sEvent.id.ToString().Substring(3, 1);
    }
}
