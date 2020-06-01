using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour {
    private ControlTower[] controlTowers;
    private int totalHealth;
    
    void Start() {
        controlTowers = GetComponentsInChildren<ControlTower>();
        UpdateBaseHealth();
        _startActiveTowers();
    }

    public void UpdateBaseHealth() {
        totalHealth = 0;

        foreach (var controlTower in controlTowers)
            totalHealth += controlTower.GetCurrentHealth();
        
        if (totalHealth <= 0)
            StartCoroutine(_loadGameOver());
    }

    private void _startActiveTowers() {
        foreach (var controlTower in controlTowers)
            if (controlTower.IsActive) FindObjectOfType<CurrencyController>().AddActiveTower();
    }

    private IEnumerator _loadGameOver() {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<LevelLoader>().LoadGameOver();
    } 
}