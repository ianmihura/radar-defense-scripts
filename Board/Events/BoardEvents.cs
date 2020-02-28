using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEvents : MonoBehaviour {
    
    [SerializeField] private BlueFlash blueFlashPrefab;

    private BlueFlash _currentBlueFlash;

    private void Start() {
        _destroyBoardEvents();
    }

    private void _destroyBoardEvents() {
        _destroyInstantBoardEvents();
    }

    private void _destroyInstantBoardEvents() {
        if (_currentBlueFlash)
            _currentBlueFlash.DestroyMe();
    }
    
    public void TriggerBlueFlash() {
        _destroyInstantBoardEvents();
        
        _currentBlueFlash = Instantiate(blueFlashPrefab, transform.position, Quaternion.identity);
    }
}
