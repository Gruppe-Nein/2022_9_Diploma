using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;
    [SerializeField] private GameData _gameData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _gameData = new GameData();
    }

    #region SaveAndLoad
        public event Action<GameData> OnLoadData;
        public event Action<GameData> OnSaveData;
    #endregion

    public void LoadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml,", FileMode.Open);
        GameData tmp = serializer.Deserialize(stream) as GameData;
        if (tmp != null)
        {
            _gameData = tmp;
        }
        stream.Close();

        OnLoadData?.Invoke(_gameData);
    }
    public void SaveData()
    {
        OnSaveData?.Invoke(_gameData);

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml,", FileMode.Create);
        serializer.Serialize(stream, _gameData);
        stream.Close();
    }
}
