using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingUIManager : MonoBehaviour
{
    public static LevelingUIManager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public GameObject levelingUI;
    public TextMeshProUGUI topUIName;
    public TextMeshProUGUI midUIName;
    public TextMeshProUGUI botUIName;
    public TextMeshProUGUI topUIDescription;
    public TextMeshProUGUI midUIDescription;
    public TextMeshProUGUI botUIDescription;
    public Image topUIImage;
    public Image midUIImage;
    public Image botUIImage;
    public SkillUpgrade[] options;
    public bool isShowingUI;

    // Start is called before the first frame update
    void Start()
    {
        isShowingUI = false;
        options = new SkillUpgrade[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_SHOOT))) {
            PlayerStats.instance.GainExp(1);
        }
        
        if (isShowingUI) {
            Time.timeScale = 0.0f;
            topUIName.text = options[0].sName + " Lv " + options[0].sCurrLevel;
            midUIName.text = options[1].sName + " Lv " + options[1].sCurrLevel;;
            botUIName.text = options[2].sName + " Lv " + options[2].sCurrLevel;;

            topUIDescription.text = options[0].sDescription;
            midUIDescription.text = options[1].sDescription;
            botUIDescription.text = options[2].sDescription;

            topUIImage = options[0].sImage;
            midUIImage = options[1].sImage;
            botUIImage = options[2].sImage;
        }
    }

    public void ShowUI(bool _flag) {
        levelingUI.SetActive(_flag);
        isShowingUI = _flag;

        options = LevelingSystem.instance.GetPossibleUpgrade();

        if (_flag) {
            Time.timeScale = 0.0f;
        } else {
            Time.timeScale = 1.0f;
        }
    }


    public void ActivateTopButton() {
        options[0].sCurrLevel++;
        LevelingSystem.instance.TryUpgrade(options[0]);
        ShowUI(false);
    }
    public void ActivateMidButton() {
        options[1].sCurrLevel++;
        LevelingSystem.instance.TryUpgrade(options[1]);
        ShowUI(false);
    }
    public void ActivateBotButton() {
        options[2].sCurrLevel++;
        LevelingSystem.instance.TryUpgrade(options[2]);
        ShowUI(false);
    }

}
