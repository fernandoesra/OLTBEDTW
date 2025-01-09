using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private bool ChanginLanguage = false;

    public void NewGame()
    {
        // SceneManager.LoadScene(1);
        LoadingScene.LoadScene("Game");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Mute()
    {
        AudioController.PauseBackMusic();
    }

    public void Unmute()
    {
        AudioController.ContinueBackMusic();
    }

    public void LanguageESP()
    {
        if (ChanginLanguage)
        {
            return;
        }
        StartCoroutine(SetLocale(1));
    }

    public void LanguageENG()
    {
        if (ChanginLanguage)
        {
            return;
        }
        StartCoroutine(SetLocale(0));
    }

    private IEnumerator SetLocale(int localeID)
    {
        ChanginLanguage = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        ChanginLanguage = false;
    }

}
