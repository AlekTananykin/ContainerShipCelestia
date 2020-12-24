using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;

public class Interface : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] GameObject _volSlider;
    [SerializeField] GameObject _input;
    [SerializeField] string _playername;

    [SerializeField] private AudioMixer _mixer;

    const int _beginScene = 1;

    void Start()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_beginScene);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void SetVol(float value)
    {
        AudioListener.volume = value;
    }
    void Update()
    {
        
    }
}
