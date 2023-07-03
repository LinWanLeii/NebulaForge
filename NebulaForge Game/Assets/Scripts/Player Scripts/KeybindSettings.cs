using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindSettings : MonoBehaviour
{
    // Store all possible keyboard key presses into static readonly array
    private static readonly KeyCode[] keyCodes = System.Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>()
                                                 .Where(k => ((int)k < (int)KeyCode.Mouse0))
                                                 .ToArray(); 
    void Update()
    {
        // TODO Add further check and set process here when free
        Debug.Log("KeyCode down: " + GetCurrentKeyDown());
    }

    // Checks for possible key press from the static readonly array
    // Returns null if nothing is pressed
    private static KeyCode? GetCurrentKeyDown()
    {
        if (!Input.anyKey)
        {
            return null;
        }
    
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                return keyCodes[i];
            }
        }
        return null;
    }
 
}