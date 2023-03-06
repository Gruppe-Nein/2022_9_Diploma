using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _qDropdown;
    [SerializeField] private TMP_Dropdown _resDropdown;

    private Resolution[] _resolutions;
    private int _currentResIndex = 0;
    private List<string> _resOptions = new List<string>();

    private void Start()
    {
        _qDropdown.value = QualitySettings.GetQualityLevel();
        _qDropdown.RefreshShownValue();

        _resDropdown.ClearOptions();
        _resolutions = Screen.resolutions;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            _resOptions.Add(_resolutions[i].ToString());
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResIndex = i;
            }
        }        
        _resDropdown.AddOptions(_resOptions);
        _resDropdown.value = _currentResIndex;
        _resDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat(Constants.GENERAL_VOLUME, volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        Debug.Log("CHANGED QUALITY " + QualitySettings.GetQualityLevel());
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, Screen.fullScreen);
    }
}
