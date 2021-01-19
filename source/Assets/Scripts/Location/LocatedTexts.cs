using UnityEngine;
using System.Collections.Generic;

public class LocatedTexts :  Singleton<LocatedTexts>
{

    Dictionary<string, Dictionary<string, string>> keysAndTexts = null;

    private void Start() {
        InitiateKeys();
    }

    private void InitiateKeys()
    {
        keysAndTexts = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> start = new Dictionary<string, string>();
        start.Add("pt-br", "Configurações");
        start.Add("us-us", "Settings");
        keysAndTexts.Add("settings", start);
    }

    public string GetKey(string key)
    {
        if (keysAndTexts == null) {
            InitiateKeys();
        }
        Debug.Log(keysAndTexts);
        return keysAndTexts[key]["pt-br"];
    }
    

}