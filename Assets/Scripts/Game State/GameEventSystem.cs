using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;
    [SerializeField] private GameData _gameData;

    [SerializeField] private InputActionAsset actions;
    private ControlBindings _cRebinds;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _gameData = new GameData();
        _cRebinds = new ControlBindings();
    }

    #region SaveAndLoadPlayer
        public event Action<GameData> OnLoadData;
        public event Action<GameData> OnSaveData;
    #endregion

    public void LoadData()
    {
        if (File.Exists(Application.dataPath + "/../save.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Open);
            GameData tmp = serializer.Deserialize(stream) as GameData;
            if (tmp != null)
            {
                _gameData = tmp;
                _loadingData.sceneToLoad = _gameData.sceneToLoad;
                _loadingData.stateToLoad = _gameData.stateToLoad;
            }
            stream.Close();

            OnLoadData?.Invoke(_gameData);
        }
    }
    public void SaveData()
    {
        OnSaveData?.Invoke(_gameData);
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Create);

        _gameData.sceneToLoad = _loadingData.sceneToLoad;
        _gameData.stateToLoad = _loadingData.stateToLoad;

        serializer.Serialize(stream, _gameData);
        stream.Close();
    }

    public void NewGame()
    {
        _gameData = new GameData();
        SaveData();
    }

    public void LoadControl()
    {
        if (File.Exists(Application.dataPath + "/../controls.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ControlBindings));
            FileStream stream = new FileStream(Application.dataPath + "/../controls.xml", FileMode.Open);
            ControlBindings tmp = serializer.Deserialize(stream) as ControlBindings;
            if (tmp != null)
            {
                _cRebinds = tmp;
                if (!string.IsNullOrEmpty(_cRebinds.rebinds))
                {
                    actions.LoadBindingOverridesFromJson(_cRebinds.rebinds);
                }
            }
            stream.Close();
        }
    }

    public void SaveControl()
    {
        _cRebinds.rebinds = actions.SaveBindingOverridesAsJson();
        XmlSerializer serializer = new XmlSerializer(typeof(ControlBindings));
        FileStream stream = new FileStream(Application.dataPath + "/../controls.xml", FileMode.Create);
        serializer.Serialize(stream, _cRebinds);
        stream.Close();
    }

    private void OnEnable()
    {
        LoadControl();
    }

    private void OnDisable()
    {
        SaveControl();
    }
}
