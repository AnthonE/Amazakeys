using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class MasterInput : MonoBehaviour
{

    //Place Buttons from UI here. 
    public AmazaKeyHolder Ability1;
    public AmazaKeyHolder Ability2;
    public AmazaKeyHolder Ability3;
    public AmazaKeyHolder Ability4;
    public AmazaKeyHolder Interact;
    public AmazaKeyHolder Inventory;
    public AmazaKeyHolder AutoRun;
    public AmazaKeyHolder PvP;
    public AmazaKeyHolder Map;
    public AmazaKeyHolder Quest;
    private static bool ListenForKeys;
    private static AmazaKeyHolder CurrentKey;
    private List<AmazaKey> Keys = new List<AmazaKey>();
    private System.Array EnumVals = new System.Array[1];
    public KeyCode[] UnsafeKeys;
    private static MasterInput instance;

    public static MasterInput Instance
    {
        get
        {
            if (instance == null)
            Debug.LogError("No innstance of MasterInput in the scene");
            return instance;
        }
    }

    private void LoadAmazakeyData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "Amazakey" + ".amz"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "Amazakey" + ".amz", FileMode.Open);
            Keys = (List<AmazaKey>)bf.Deserialize(file);
            file.Close();
            foreach (var k in Keys) {
                switch (k.Action) {
                    case AmazaKeyActions.Ability1: Ability1.Key.KeyCode = k.KeyCode; Ability1.SetKeyStrings(); break;
                    case AmazaKeyActions.Ability2: Ability2.Key.KeyCode = k.KeyCode; Ability2.SetKeyStrings(); break;
                    case AmazaKeyActions.Ability3: Ability3.Key.KeyCode = k.KeyCode; Ability3.SetKeyStrings(); break;
                    case AmazaKeyActions.Ability4: Ability4.Key.KeyCode = k.KeyCode; Ability4.SetKeyStrings(); break;
                    case AmazaKeyActions.Interact: Interact.Key.KeyCode = k.KeyCode; Interact.SetKeyStrings(); break;
                    case AmazaKeyActions.Inventory: Inventory.Key.KeyCode = k.KeyCode; Inventory.SetKeyStrings(); break;
                    case AmazaKeyActions.Map: Map.Key.KeyCode = k.KeyCode; Map.SetKeyStrings(); break;
                    case AmazaKeyActions.PvP: PvP.Key.KeyCode = k.KeyCode; PvP.SetKeyStrings(); break;
                    case AmazaKeyActions.Quest: Quest.Key.KeyCode = k.KeyCode; Quest.SetKeyStrings(); break;
                    case AmazaKeyActions.AutoRun: AutoRun.Key.KeyCode = k.KeyCode; AutoRun.SetKeyStrings(); break;
                }
            }
        }
    }

    private void SaveAmazakeyData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + "Amazakey" + ".amz", FileMode.OpenOrCreate);
        bf.Serialize(file, Keys);
        file.Close();
    }

    private void OnDestroy()
    {
        SaveAmazakeyData();
    }

    public static KeyCode GetKey (AmazaKeyActions action)
    {
        switch(action)
        {
            case AmazaKeyActions.Ability1: return instance.Ability1.Key.KeyCode; 
            case AmazaKeyActions.Ability2: return instance.Ability2.Key.KeyCode; 
            case AmazaKeyActions.Ability3: return instance.Ability3.Key.KeyCode;
            case AmazaKeyActions.Ability4: return instance.Ability4.Key.KeyCode;
            case AmazaKeyActions.Interact: return instance.Interact.Key.KeyCode;
            case AmazaKeyActions.Inventory: return instance.Inventory.Key.KeyCode;
            case AmazaKeyActions.Map: return instance.Map.Key.KeyCode;
            case AmazaKeyActions.PvP: return instance.PvP.Key.KeyCode;
            case AmazaKeyActions.Quest: return instance.Quest.Key.KeyCode;
            case AmazaKeyActions.AutoRun: return instance.AutoRun.Key.KeyCode;
        }
        return KeyCode.None;
    }


    private void Awake()
    {
        if (instance == null) instance = this; else GameObject.Destroy(this);
    }
    private void Start()
    {
          EnumVals = System.Enum.GetValues(typeof(KeyCode));
          Invoke(nameof(LoadAmazakeyData), 1f);
    }

    private void Update()
    {
        if (ListenForKeys)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ListenForKeys = false;
                CurrentKey.SetKeyStrings();
                return;
            }
            DetectPressedKeyOrButton();
        }
    }

    private void DetectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in EnumVals)
        {
            if (Input.GetKeyDown(kcode))
            {
                if (SafeKey(kcode))
                {
                    CurrentKey.Key.KeyCode = kcode;
                    for (int i = 0; i < Keys.Count; i++)
                    {
                        if (Keys[i].Action == CurrentKey.Key.Action)
                        {
                            CurrentKey.Key.KeyCode = kcode;
                            ListenForKeys = false;
                            Keys[i] = new AmazaKey { KeyCode = kcode, Action = Keys[i].Action };
                            CurrentKey.SetKeyStrings();
                        }
                    }
                }
            }
        }
    }

    private bool SafeKey(KeyCode key) {
        for (int i = 0; i < UnsafeKeys.Length; i++)
        {
            if (UnsafeKeys[i] == key)
            {
                return false;
            }
        }
        return true;
    }

    public void ButtonListenForKey(AmazaKeyHolder key)
    {
        CurrentKey = key;
        CurrentKey.SetWaitingString();
        ListenForKeys = true;
    }
   
}
