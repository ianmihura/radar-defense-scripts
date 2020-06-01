using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransformVerticalMover : MonoBehaviour {

    private Transform _background;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _lowerLimit = -6.4f;
    [SerializeField] private float _upperLimit = 8.5f;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (SceneManager.GetActiveScene().name != "CreditsScene" && FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _background = GetComponentInParent<Transform>();
    }

    void Update() {
        _background.Translate(Vector2.up * Time.deltaTime * _speed);
        if (_background.transform.position.y >= _upperLimit || _background.transform.position.y <= _lowerLimit)
            _speed *= -1;
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
