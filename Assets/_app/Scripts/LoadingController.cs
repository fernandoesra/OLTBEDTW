using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI LoadingText;
    void Start()
    {
        string LvlToLoad = LoadingScene.nextLvl;
        StartCoroutine(LoadScene(LvlToLoad));
    }

    IEnumerator LoadScene(string nivel)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                LoadingText.color = new Color32(0xE7, 0x9A, 0x54, 255);
                switch (Language())
                {
                    case 0:
                        LoadingText.text = "Tap the screen to continue";
                        break;
                    case 1:
                        LoadingText.text = "Pulsa en la pantalla para continuar";
                        break;
                }
                if (Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    public int Language()
    {
        return LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }

}
