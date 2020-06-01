using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RelationshipsController : MonoBehaviour {

    private Slider _moraleSlider;
    private Slider _comraderySlider;
    private Slider _loyaltySlider;

    private readonly string MORALE_INDEX = "4";
    private readonly string COMRADERY_INDEX = "2";
    private readonly string LOYALTY_INDEX = "3";

    private readonly int ADD_VALUE = 5;
    private readonly int RELATIONSHIP_START = 5;

    private readonly int LOW_BOUND = 25;
    private readonly int MEDIUM_BOUND = 50;
    private readonly int HIGH_BOUND = 75;

    private readonly int LOW_ENDSTATE = 0;
    private readonly int HIGH_ENDSTATE = 100;

    public void AddMorale(int amount) { GetMorale += (ADD_VALUE * amount); _checkRelationships(GetMorale, MORALE_INDEX); }
    public void AddComradery(int amount) { GetComradery += (ADD_VALUE * amount); _checkRelationships(GetComradery, COMRADERY_INDEX); }
    public void AddLoyalty(int amount) { GetLoyalty += (ADD_VALUE * amount); _checkRelationships(GetLoyalty, LOYALTY_INDEX); }

    public void AddToAll() {
        AddComradery(-1);
        AddMorale(-1);
        AddLoyalty(-1);
    }

    public int GetMorale { get; private set; }
    public int GetComradery { get; private set; }
    public int GetLoyalty { get; private set; }

    public int GetMoraleLevel => _getLevel(GetMorale);
    public int GetComraderyLevel => _getLevel(GetComradery);
    public int GetLoyaltyeLevel => _getLevel(GetLoyalty);

    // Closest Level => Higherst or Lowest Relationship level (closest to an endstate)
    public string GetClosestLevel() {
        int moralDiff = System.Math.Max(100 - GetMorale, GetMorale);
        int comraderyDiff = System.Math.Max(100 - GetComradery, GetComradery);
        int loyaltyDiff = System.Math.Max(100 - GetLoyalty, GetLoyalty);

        if (moralDiff > comraderyDiff && moralDiff > loyaltyDiff)
            return MORALE_INDEX;
        else if (comraderyDiff > moralDiff && comraderyDiff > loyaltyDiff)
            return COMRADERY_INDEX;
        else
            return LOYALTY_INDEX;
    }

    /*
     * Low => 1
     * Medium => 2
     * High => 3
     * Error => 0
     */
    private int _getLevel(int relationship) {
        if (relationship < LOW_BOUND)
            return 1;
        else if (relationship < MEDIUM_BOUND)
            return 2;
        else if (relationship < HIGH_BOUND)
            return 3;
        else
            return 0;
    }

    void Start() {
        _setStartupRelationships();

        _getSliders();
    }

    private void _setStartupRelationships() {
        GetMorale = GetComradery = GetLoyalty = RELATIONSHIP_START;
    }

    private void _getSliders() {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();

        _moraleSlider = sliders[0];
        _loyaltySlider = sliders[1];
        _comraderySlider = sliders[2];

        _updateSlidersValue();
    }

    private void _updateSlidersValue() {
        _moraleSlider.value = GetMorale;
        _loyaltySlider.value = GetLoyalty;
        _comraderySlider.value = GetComradery;
    }

    private void _checkRelationships(int relationship, string relationshipIndex) {
        _updateSlidersValue();

        if (relationship >= HIGH_ENDSTATE || relationship <= LOW_ENDSTATE)
            FindObjectOfType<TurnController>().LoadRelationshipGameOver(relationshipIndex, relationship <= LOW_ENDSTATE);
    }
}
