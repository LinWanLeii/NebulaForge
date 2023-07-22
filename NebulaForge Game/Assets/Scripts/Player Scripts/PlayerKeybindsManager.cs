using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeybindsManager : MonoBehaviour
{
    public enum KEYBINDINGS 
    {
        KB_UP = 0,
        KB_DOWN,
        KB_LEFT,
        KB_RIGHT,
        KB_SHOOT,
        KB_PAUSE,
        KB_TOTAL
    }

    public static PlayerKeybindsManager instance;

    private Dictionary<KEYBINDINGS, KeyCode> kbDictionary;

    private void SetDefaultKeyBindings() {
        kbDictionary.Clear();
        kbDictionary.Add(KEYBINDINGS.KB_UP, KeyCode.W);
        kbDictionary.Add(KEYBINDINGS.KB_DOWN, KeyCode.S);
        kbDictionary.Add(KEYBINDINGS.KB_LEFT, KeyCode.A);
        kbDictionary.Add(KEYBINDINGS.KB_RIGHT, KeyCode.D);
        kbDictionary.Add(KEYBINDINGS.KB_SHOOT, KeyCode.Space);
        kbDictionary.Add(KEYBINDINGS.KB_PAUSE, KeyCode.Escape);
    }
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }

    // Given the default keybinding, return the currently registered keybinding
    // If the keycode is not changed by the player, the default keycode is returned.
    public KeyCode GetKeyForAction(KEYBINDINGS _kb) {
        return kbDictionary[_kb];
    }

    // Start is called before the first frame update
    void Start()
    {
        kbDictionary = new Dictionary<KEYBINDINGS, KeyCode>();
        SetDefaultKeyBindings();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
