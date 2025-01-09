using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class IngameInterfaceController : MonoBehaviour
{

    private bool ChanginLanguage = false;

    public void LoadMainMenu()
    {
        LoadingScene.LoadScene("MainMenu");
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
        LogAreaController.Instance.Reset();
    }

    public void LanguageENG()
    {
        if (ChanginLanguage)
        {
            return;
        }
        StartCoroutine(SetLocale(0));
        LogAreaController.Instance.Reset();
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
