using UnityEngine;
using UnityEngine.UI;

public class RelationshipsController : MonoBehaviour {

    private Slider _moraleSlider;
    private Slider _comraderySlider;
    private Slider _loyaltySlider;

    private int _morale; // index: 4
    private int _comradery; // index: 2
    private int _loyalty; // index: 3

    private string MORALE_INDEX = "4";
    private string COMRADERY_INDEX = "2";
    private string LOYALTY_INDEX = "3";

    private int ADD_VALUE = 5;
    private int RELATIONSHIP_START = 25;

    private int LOW_BOUND = 25;
    private int MEDIUM_BOUND = 50;
    private int HIGH_BOUND = 75;

    private int LOW_ENDSTATE = 0;
    private int HIGH_ENDSTATE = 100;

    public void AddMorale() { _morale += ADD_VALUE; _checkRelationships(_morale); }
    public void AddComradery() { _comradery += ADD_VALUE; _checkRelationships(_comradery); }
    public void AddLoyalty() { _loyalty += ADD_VALUE; _checkRelationships(_loyalty); }

    public void SubstractMorale() { _morale -= ADD_VALUE; _checkRelationships(_morale); }
    public void SubstractComradery() { _comradery -= ADD_VALUE; _checkRelationships(_comradery); }
    public void SubstractLoyalty() { _loyalty -= ADD_VALUE; _checkRelationships(_loyalty); }

    public int GetMorale => _morale;
    public int GetComradery => _comradery;
    public int GetLoyalty => _loyalty;

    public int GetMoraleLevel => _getLevel(_morale);
    public int GetComraderyLevel => _getLevel(_comradery);
    public int GetLoyaltyeLevel => _getLevel(_loyalty);

    // Closest Level => Higherst or Lowest Relationship level (closest to an endstate)
    public string GetClosestLevel() {
        int moralDiff = System.Math.Max(100 - _morale, _morale);
        int comraderyDiff = System.Math.Max(100 - _comradery, _comradery);
        int loyaltyDiff = System.Math.Max(100 - _loyalty, _loyalty);

        if (moralDiff > comraderyDiff && moralDiff > loyaltyDiff) {
            return MORALE_INDEX;
        } else if (comraderyDiff > moralDiff && comraderyDiff > loyaltyDiff) {
            return COMRADERY_INDEX;
        } else {
            return LOYALTY_INDEX;
        }
    }

    /*
    * Low => 1
    * Medium => 2
    * High => 3
    * Error => 0
    */
    private int _getLevel(int relationship) {
        if (relationship < LOW_BOUND) {
            return 1;
        } else if (relationship < MEDIUM_BOUND) {
            return 2;
        } else if (relationship < HIGH_BOUND) {
            return 3;
        } else {
            return 0;
        }
    }

    void Start() {
        _setStartupRelationships();

        _getSliders();
    }

    private void _setStartupRelationships() {
        _morale = _comradery = _loyalty = RELATIONSHIP_START;
    }

    private void _getSliders() {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();

        _moraleSlider = sliders[0];
        _loyaltySlider = sliders[1];
        _comraderySlider = sliders[2];

        _updateSlidersValue();
    }

    private void _updateSlidersValue() {
        _moraleSlider.value = _morale;
        _loyaltySlider.value = _loyalty;
        _comraderySlider.value = _comradery;
    }

    private void _checkRelationships(int relationship) {
        _updateSlidersValue();

        if (relationship >= HIGH_ENDSTATE || relationship <= LOW_ENDSTATE) {
            //TODO shoot endstate by relationship
        }
    }
}
