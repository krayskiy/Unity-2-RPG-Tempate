using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private class SaveObject
    {
        public Vector3 Position;
        public int Gold;
        public int Experience;
        public int Difficulty;
        public string Name;
        public List<int> ItemAmounts;
    }

    public PlayerManager pm;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveSystem.Init();
    }

    public void SaveGame()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        SaveObject saveObj = new SaveObject
        {
            Position = pm.transform.position,
            Gold = pm.gold,
            Experience = pm.experience,
            Difficulty = PlayerPrefs.GetInt(PrefNames.difficulty),
            Name = PlayerPrefs.GetString(PrefNames.playerName),
            ItemAmounts = new List<int>(pm.inventory.count)
        };
        foreach (InventorySlotProxy proxy in pm.inventory) {
            saveObj.ItemAmounts.Add(proxy.itemAmount);
        }
        string json = JsonUtility.ToJson(saveObj);
        SaveSystem.Save(json);
    }

    public void LoadGame()
    {

    }

    IEnumerator LoadingValues(SaveObject loadedSave)
    {
        
    }
}
