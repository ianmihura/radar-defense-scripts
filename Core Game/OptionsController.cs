using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    [SerializeField] private Slider volumeSlider;

    private MusicPlayer _musicPlayer;
    private bool volumeChanged = false;

    private int[] relationshipStartDefaults;
    private int[] relationshipAddDefaults;
    private int[] BRSDefaults;
    private string[] defaults = { "EASY", "MEDIUM", "HARD" };
    private int defaultsIndex = 1;

    void Start() {
    relationshipStartDefaults = new int[] { PlayerPrefsController.RELATIONSHIP_START_EASY, PlayerPrefsController.RELATIONSHIP_START_EASY, PlayerPrefsController.RELATIONSHIP_START_HARD };
    relationshipAddDefaults = new int[] { PlayerPrefsController.RELATIONSHIP_ADD_MIN, 5, PlayerPrefsController.RELATIONSHIP_ADD_MAX };
    BRSDefaults = new int[] { PlayerPrefsController.MAX_BRS, 1, PlayerPrefsController.MIN_BRS };


    var textOptions = FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>();
        textOptions[2].text = PlayerPrefsController.GetRelationshipStart.ToString();
        textOptions[4].text = PlayerPrefsController.GetRelationshipAdd.ToString();
        textOptions[6].text = PlayerPrefsController.GetBRS.ToString();

        _musicPlayer = FindObjectOfType<MusicPlayer>();
        volumeSlider.value = PlayerPrefsController.GetMasterVolume;
    }

    private void Update() {
        SetVolumeChange();

        if (volumeChanged) {
            PlayerPrefsController.SetMasterVolume(volumeSlider.value);
            volumeChanged = false;
        }
    }

    public void ToggleRelationshipStart() {
        int relationship = 43 ^ PlayerPrefsController.GetRelationshipStart;
        PlayerPrefsController.SetRelationshipStart(relationship);

        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[2]
            .text = relationship.ToString();
    }

    public void ToggleRelationshipAdd() {
        int relationship = PlayerPrefsController.GetRelationshipAdd + 1;
        if (relationship > PlayerPrefsController.RELATIONSHIP_ADD_MAX)
            relationship = PlayerPrefsController.RELATIONSHIP_ADD_MIN;

        PlayerPrefsController.SetRelationshipAdd(relationship);

        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[4]
            .text = relationship.ToString();
    }
    
    public void ToggleBRS() {
        int brs = PlayerPrefsController.GetBRS + 1;
        if (brs > PlayerPrefsController.MAX_BRS)
            brs = PlayerPrefsController.MIN_BRS;

        PlayerPrefsController.SetBRS(brs);

        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[6]
            .text = brs.ToString();
    }
    
    public void ToggleDefault() {
        defaultsIndex++;
        if (defaultsIndex >= defaults.Length)
            defaultsIndex = 0;

        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[1]
            .text = defaults[defaultsIndex];

        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[2]
            .text = relationshipStartDefaults[defaultsIndex].ToString();
        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[4]
            .text = relationshipAddDefaults[defaultsIndex].ToString();
        FindObjectOfType<OptionsMenu>().GetComponentsInChildren<TextMeshProUGUI>()[6]
            .text = BRSDefaults[defaultsIndex].ToString();

        PlayerPrefsController.SetRelationshipStart(relationshipStartDefaults[defaultsIndex]);
        PlayerPrefsController.SetRelationshipAdd(relationshipAddDefaults[defaultsIndex]);
        PlayerPrefsController.SetBRS(BRSDefaults[defaultsIndex]);
    }

    public void SetVolumeChange() {
        if (_musicPlayer)
            _musicPlayer.SetVolume(volumeSlider.value);

        volumeChanged = true;
    }

    public void Exit() {
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}