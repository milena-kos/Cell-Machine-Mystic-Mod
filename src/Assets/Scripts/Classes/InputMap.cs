using System;
using System.Collections.Generic;
using UnityEngine;

public class InputMap

{
    public Dictionary<KeyCode, string> KeyCodeMap()
    {
        Dictionary<KeyCode, string> keyNames = new Dictionary<KeyCode, string>();

        foreach (KeyCode k in Enum.GetValues(typeof(KeyCode)))
            keyNames[k] = k.ToString();

        for (int i = 0; i < 10; i++)
        {
            keyNames[(KeyCode)((int)KeyCode.Alpha0 + i)] = i.ToString();
            keyNames[(KeyCode)((int)KeyCode.Keypad0 + i)] = i.ToString();
        }

        keyNames[KeyCode.Comma] = ",";
        keyNames[KeyCode.Escape] = "Esc";
        keyNames[KeyCode.UpArrow] = "Up";
        keyNames[KeyCode.DownArrow] = "Down";
        keyNames[KeyCode.RightArrow] = "Right";
        keyNames[KeyCode.LeftArrow] = "Left";
        keyNames[KeyCode.KeypadPeriod] = ".";
        keyNames[KeyCode.KeypadDivide] = "/";
        keyNames[KeyCode.KeypadMultiply] = "*";
        keyNames[KeyCode.KeypadMinus] = "-";
        keyNames[KeyCode.KeypadPlus] = "+";
        keyNames[KeyCode.DoubleQuote] = "\"";
        keyNames[KeyCode.Quote] = "'";
        keyNames[KeyCode.Comma] = ",";
        keyNames[KeyCode.Minus] = "-";
        keyNames[KeyCode.Period] = ".";
        keyNames[KeyCode.Slash] = "/";
        keyNames[KeyCode.Semicolon] = ";";
        keyNames[KeyCode.Equals] = "=";
        keyNames[KeyCode.LeftBracket] = "[";
        keyNames[KeyCode.Backslash] = "\\";
        keyNames[KeyCode.RightBracket] = "]";
        keyNames[KeyCode.BackQuote] = "`";

        return keyNames;
    }
}
