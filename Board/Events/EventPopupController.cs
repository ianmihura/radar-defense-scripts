using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPopupController : MonoBehaviour {

    public void ChoiceSelected(string choiceSelected) {
        if (choiceSelected == "close")
            return;
        else
            FindObjectOfType<EventsController>().ManageEventAnswer(choiceSelected, choiceSelected[0] == 't');
    }
}
