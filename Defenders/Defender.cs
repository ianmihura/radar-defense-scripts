using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {
    [SerializeField] int powerCost = 1;
    [SerializeField] int moneyCost = 1;
    [SerializeField] public bool isPlane = false;
    [SerializeField] public bool isX71 = false;
    private CurrencyController _currencyController;

    [SerializeField] private Arrow _arrow;

    private Arrow _upArrow;
    private Arrow _downArrow;
    private Arrow _rightArrow;
    private Arrow _leftArrow;

    private Vector2 _startPosition;
    private string _direction = "";
    public const float BLOCK_DISTANCE = 1.0f;
    private bool _isWalking;
    private bool _justCreated = false;
    private int _walkPowerCost = 2;
    private int _walkMoneyCost = 0;

    public int GetPowerCost() {
        return powerCost;
    }
    
    public int GetMoneyCost() {
        return moneyCost;
    }

    public void SetDirection(string direction) => _direction = direction;

    public string GetDirection() => _direction;

    private void Start() {
        _currencyController = FindObjectOfType<CurrencyController>();
    }

    public void OnMouseDown() {
        FindObjectOfType<InfoController>().SetInfo(gameObject);

        if (_arrow && _currencyController.HasEnoughCurrency(_walkPowerCost, _walkMoneyCost))
            _showArrows();
    }

    private void _showArrows() {
        DestroyArrows();

        if (transform.position.y != 5) {
            _upArrow = Instantiate(_arrow, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            _upArrow.SetArrow("up", gameObject);
        }
        if (transform.position.y != 0) {
            _downArrow = Instantiate(_arrow, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 180)));
            _downArrow.SetArrow("down", gameObject);
        }
        if (transform.position.x != 11) {
            _rightArrow = Instantiate(_arrow, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
            _rightArrow.SetArrow("right", gameObject);
        }
        if (transform.position.x != 0) {
            _leftArrow = Instantiate(_arrow, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
            _leftArrow.SetArrow("left", gameObject);
        }

        _justCreated = true;
    }

    public void DestroyArrows() {
        if (_upArrow)
            _upArrow.DestroyMe();
        if (_downArrow)
            _downArrow.DestroyMe();
        if (_rightArrow)
            _rightArrow.DestroyMe();
        if (_leftArrow)
            _leftArrow.DestroyMe();

        _justCreated = false;
    }

    public void Walk() {
        if (_direction != "") {
            _currencyController.SpendCurrency(_walkPowerCost, _walkMoneyCost);

            _isWalking = true;
            _startPosition = new Vector2(transform.position.x, transform.position.y);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !_justCreated)
            DestroyArrows();

        _justCreated = false;
        
        if (_isWalking) {
            switch (_direction) {
                case "up":
                transform.Translate(Vector2.up * Time.deltaTime);
                break;

                case "down":
                transform.Translate(Vector2.down * Time.deltaTime);
                break;

                case "right":
                transform.Translate(Vector2.right * Time.deltaTime);
                break;

                case "left":
                transform.Translate(Vector2.left * Time.deltaTime);
                break;

                default:
                break;
            }

            var pos = ((_startPosition.x - transform.position.x) + (_startPosition.y - transform.position.y));
            _isWalking = pos < BLOCK_DISTANCE && pos > (BLOCK_DISTANCE * -1);

            _direction = _isWalking ? _direction : "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;

        if (otherObject.GetComponent<AsteroidField>())
            GetComponent<Health>().DealDamage(otherObject.GetComponent<AsteroidField>().GetDamage);
    }
}