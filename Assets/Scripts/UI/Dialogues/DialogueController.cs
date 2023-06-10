using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actorName;
    [SerializeField] private Image _actorImage;
    [SerializeField] private TextMeshProUGUI _dialogue;

    //[SerializeField] private GameObject _dialoguePanel;
    //private Image _bgImage;

    private int _currentIndex;
    private bool _action;
    private bool _canContinue;
    private bool _instaPrint;

    [SerializeField] private DialogueEventChannelSO _dChannel;

    #region REFERENCES
    private PlayerInput _playerInput;
    #endregion

    private void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Start()
    {
        _action = false;
        _canContinue = false;
        _instaPrint = false;
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        //Time.timeScale = 0;
        Debug.Log("Dialogue event trigger received and its length = " + dialogue.GetLength());
        _playerInput.SwitchCurrentActionMap("UI Controls");

        _currentIndex = 0;
        _actorName.text = string.Empty;
        _dialogue.text = string.Empty;
        _action = false;

        StartCoroutine(DialogueFlow(dialogue));       
    }

    IEnumerator DialogueFlow(DialogueSO dialogue)
    {
        Debug.Log("Dialogue Start");
        while (_currentIndex <= dialogue.GetLength())
        {
            //NextLine(dialogue);
            _actorName.text = dialogue.GetLineByIndex(_currentIndex).getActor().getName();
            _actorImage.sprite = dialogue.GetLineByIndex(_currentIndex).getActor().getImage();
            //_dialogue.text = dialogue.GetLineByIndex(_currentIndex).getLine();

            StartCoroutine(DisplayLine(dialogue.GetLineByIndex(_currentIndex).getLine()));
            
            yield return new WaitUntil(CheckAction);
            _currentIndex++;
            _action = false;
        }
        Debug.Log("Dialogue End");

        _playerInput.SwitchCurrentActionMap("Player Controls");
        //Time.timeScale = 1;
        GameManager.Instance.SetGameState(GameState.Gameplay);
    } 

    /*public void NextLine(DialogueSO dialogue)
    {
        if (_currentIndex > dialogue.GetLength())
        {
            return;
        }
        _actorName.text = dialogue.GetLineByIndex(_currentIndex).getActor().getName();
        _actorImage.sprite = dialogue.GetLineByIndex(_currentIndex).getActor().getImage();

        StartCoroutine(DisplayLine(dialogue.GetLineByIndex(_currentIndex).getLine()));

        //_dialogue.text = dialogue.GetLineByIndex(_currentIndex).getLine();

        _currentIndex++;
    }*/

    private IEnumerator DisplayLine(string line)
    {
        _canContinue = false;

        _dialogue.text = line;
        _dialogue.maxVisibleCharacters = 0;

        foreach (char letter in line.ToCharArray())
        {
            if (_instaPrint)
            {
                _dialogue.maxVisibleCharacters = line.Length;
                break;
            }
            _dialogue.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.1f);
        }

        _canContinue = true;
        _instaPrint = false;
    }

    public void Next(InputAction.CallbackContext context)
    {
        if (context.performed && _canContinue)
        {
            _action = true;
        }
        else if (context.performed && !_instaPrint)
        {
            _instaPrint = true;
        }
    }

    public bool CheckAction()
    {
        if (_action)
        {
            return true;
        }
        else
            return false;
    }

    private void OnEnable()
    {
        _dChannel.OnDialogueEventRaised += StartDialogue;
    }

    private void OnDisable()
    {
        _dChannel.OnDialogueEventRaised -= StartDialogue;
    }
}
