using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class KeybindSettings : MonoBehaviour
{
    public GameObject rebindPanel;
    public TextMeshProUGUI keyText;
    public bool isReadingKeys = false;
    public static KeyCode pressedKey;
    public PlayerKeybindsManager.KEYBINDINGS selectedKey;

    // Store all possible keyboard key presses into static readonly array
    private static readonly KeyCode[] keyCodes = System.Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>()
                                                 .Where(k => ((int)k < (int)KeyCode.Mouse0))
                                                 .ToArray(); 
    void Update()
    {
        if (isReadingKeys)
        {
            if (GetCurrentKeyDown()) {
                ManagePanel(false);
                PlayerKeybindsManager.instance.ChangeBindings(selectedKey, pressedKey);
            }
            
            char a = (char)((int)PlayerKeybindsManager.instance.GetKeyForAction(selectedKey));
            keyText.text = a.ToString();
        }
    }

    public void ManagePanel(bool _flag) {
        rebindPanel.SetActive(_flag);
        isReadingKeys = _flag;
    }

    public void SelectKeyUp() {
        selectedKey = PlayerKeybindsManager.KEYBINDINGS.KB_UP;
    }
    public void SelectKeyDown() {
        selectedKey = PlayerKeybindsManager.KEYBINDINGS.KB_DOWN;
    }
    public void SelectKeyLeft() {
        selectedKey = PlayerKeybindsManager.KEYBINDINGS.KB_LEFT;
    }
    public void SelectKeyRight() {
        selectedKey = PlayerKeybindsManager.KEYBINDINGS.KB_RIGHT;
    }

    // Checks for possible key press from the static readonly array
    private static bool GetCurrentKeyDown()
    {
        if (!Input.anyKey)
        {
            return false;
        }
    
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                pressedKey = keyCodes[i];
                return true;
            }
        }
        return false;
    }
 
}