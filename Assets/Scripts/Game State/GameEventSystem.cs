using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static GameData;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;

    #region INPUT ACTION
    [SerializeField] private InputActionAsset actions;
    #endregion

    #region PARAMETERS
    [SerializeField] private GameData _gameData;
    private ControlBindings _cRebinds;


    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    [SerializeField] private ChronoEventChannel _cEventChannel;
    #endregion

    private void Awake()
    {
        // Error occurs because of the multiple instances of GameEventSystem, Ignore it. 
        // Need it to launch scene separately, will delete it when building final version.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _cRebinds = new ControlBindings();
    }

    #region playerActions
    public event Action<GameData> OnPlayerTakeDamage;
    public event Action<GameData> OnPlayerRestoreHealth;
    public event Action<bool> OnPlayerInSafeZone;
    public event Action OnPlayerDead;
    #endregion

    #region SaveAndLoad
    public event Action<GameData> OnLoadData;
    public event Action<GameData> OnSaveData;
    #endregion

    public void PlayerTakeDamage(int damage)
    {
        bool playerIsDead = false;
        if (_gameData._canTakeDamage)
        {
            playerIsDead = _gameData.GetDamage(damage);
            if (playerIsDead)
                OnPlayerDead?.Invoke();
            else
                OnPlayerTakeDamage?.Invoke(_gameData);
        }
        Debug.Log($"Current health: {_gameData.PlayerHealth}");
    }

    public void PlayerFallDown()
    {
        OnPlayerDead?.Invoke();
    }
    public void PlayerRestoreHealthCheckpoint(int amount)
    {
        _gameData.GetHealth(amount);
        //Debug.Log($"Current health: {_gameData.PlayerHealth}");
    }

    public void SetPlayerInSafeZone(bool isInSafeZone)
    {
        OnPlayerInSafeZone?.Invoke(isInSafeZone);
    }

    public void NewGame(int difficulty)
    {
        _gameData = new GameData();
        if (difficulty == (int)GameDifficulty.Easy)
        {
            _gameData.SetDifficilty(GameDifficulty.Easy);
            //Debug.Log(_gameData.PlayerHealth);
        }
        if (difficulty == (int)GameDifficulty.Normal)
        {
            _gameData.SetDifficilty(GameDifficulty.Normal);
        }
        SaveData();
    }

    public void LoadScene(int levelIndex)
    {
        _loadingData.sceneToLoad = levelIndex;
        _loadingData.stateToLoad = GameState.Gameplay;
        Instance.SaveData();
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(levelIndex);
    }

    #region SAVING and LOADING GAME DATA METHODS
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
                _loadingData.sceneToLoad = _gameData.SceneToLoad;
                _loadingData.stateToLoad = _gameData.StateToLoad;
            }
            stream.Close();

            OnLoadData?.Invoke(_gameData);
            _cEventChannel.CheckPointRestore(true);
        }
    }
    public void SaveData()
    {
        OnSaveData?.Invoke(_gameData);
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Create);

        _gameData.SceneToLoad = _loadingData.sceneToLoad;
        _gameData.StateToLoad = _loadingData.stateToLoad;

        if (_gameData.GameDifficulty == GameDifficulty.Easy)
        {
            _gameData.PlayerHealth = 2;
        }
        else if (_gameData.GameDifficulty == GameDifficulty.Normal)
        {
            _gameData.PlayerHealth = 1;
        }

        serializer.Serialize(stream, _gameData);
        stream.Close();
    }
    #endregion

    #region SAVING and LOADING OPTION PARAMETERS METHODS
    // TODO add saving and loading for other option parameters
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
    #endregion
}
