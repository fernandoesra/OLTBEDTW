using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource Audio;
    public static AudioController Instance;

    void Awake()
    {
        if (AudioController.Instance == null)
        {
            AudioController.Instance = this;
            DontDestroyOnLoad(gameObject);
            Audio = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PauseBackMusic()
    {
        Instance.Audio.Pause();
    }

    public static void ContinueBackMusic()
    {
        Instance.Audio.UnPause();
    }

}
