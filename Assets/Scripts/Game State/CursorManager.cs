using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [SerializeField] private Texture2D _mainCursorTexture;
    [SerializeField] private Texture2D _crosshairCursorTexture;

    private Vector2 _defaultHotspot = Vector2.zero;
    private Vector2 _crosshairHotspot;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _crosshairHotspot = new Vector2(_crosshairCursorTexture.width / 2, _crosshairCursorTexture.height / 2);
    }

    private void Start()
    {
        
    }

    private void UpdateCursor(GameState currentGameState)
    {
        if (currentGameState == GameState.Gameplay)
        {
            Cursor.SetCursor(_crosshairCursorTexture, _crosshairHotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_mainCursorTexture, _defaultHotspot, CursorMode.Auto);
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += UpdateCursor;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= UpdateCursor;
    }
}
