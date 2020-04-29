using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
[System.Serializable]
public class AmazaKeyHolder : MonoBehaviour
{
    public AmazaKey Key;
    private Text buttonShowingKeys;
    private Text menuName;

    private void Start()
    {
        menuName = transform.GetChild(0).GetComponent<Text>();
        if (menuName != null)
        {
            menuName.text = Key.Action.ToString();
        }
        else {
            Debug.LogWarning("We need text here");
        }
        buttonShowingKeys = GetComponent<Text>();
        if (Key.KeyCode == KeyCode.None)
        {
            buttonShowingKeys.text = "No keys";
        }
        else if (Key.KeyCode != KeyCode.None)
        {
            buttonShowingKeys.text = Key.KeyCode.ToString();
        }
  
    }

    public void SetKeyStrings() {
        if (Key.KeyCode == KeyCode.None)
        {
            buttonShowingKeys.text = "No keys";
        }
        else if (Key.KeyCode != KeyCode.None)
        {
            buttonShowingKeys.text = Key.KeyCode.ToString();
        }
        if (menuName != null)
        {
            menuName.text = Key.Action.ToString();
        }
    }

    public void SetWaitingString() {
        buttonShowingKeys.text = "Input Now";
    }

}


[System.Serializable]
public enum AmazaKeyActions {Ability1,Ability2,Ability3,Ability4,Interact,Inventory,AutoRun,PvP,Map,Quest}
[System.Serializable]
public struct AmazaKey{
public AmazaKeyActions Action;
public KeyCode KeyCode;
    public void SetKeyCode(KeyCode c) {
        this.KeyCode = c;
    }
}