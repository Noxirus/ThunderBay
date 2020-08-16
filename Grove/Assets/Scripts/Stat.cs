using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValues;
    private List<int> modifiers;

    public int getValue() {
        int finalValue = baseValues;
        if (modifiers != null)
        {
            modifiers.ForEach(x => finalValue += x);
        }
        return finalValue;
    }

    public void addModifier(int mod) {
        modifiers.Add(mod);
    }

    public void removeModifier(int mod) {
        modifiers.Remove(mod);
    }
}
