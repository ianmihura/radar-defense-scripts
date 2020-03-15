using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSignController : MonoBehaviour {

    [SerializeField] GameObject ThunderSign;
    [SerializeField] GameObject DustSign;

    private GameObject _currentThunderSign;
    private GameObject _currentDustSign;
    private BoardEvents _boardEvents;

    private int MAX_TIME = 3;
    private int _time = 3;

    private void _resetTime() => _time = MAX_TIME;

    private void Start() {
        _boardEvents = FindObjectOfType<BoardEvents>();
    }

    public void ShowThunderSign() {
        DestroySigns();
        _currentThunderSign = Instantiate(ThunderSign, transform.position, Quaternion.identity);
        _boardEvents.SetThunderStorm();
    }

    public void ShowDustSign() {
        DestroySigns();
        _currentDustSign = Instantiate(DustSign, transform.position, Quaternion.identity);
        _boardEvents.SetDustStorm();
    }

    public void DestroySigns() {
        _resetTime();
        _boardEvents.DestroySigns();

        if (_currentDustSign)
            Destroy(_currentDustSign);
        if (_currentThunderSign)
            Destroy(_currentThunderSign);
    }

    public void PassTurn() {
        if (!_currentDustSign && !_currentThunderSign)
            return;

        _time--;

        if (_time <= 0)
            DestroySigns();
    }
}
