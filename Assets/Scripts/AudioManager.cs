using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    [SerializeField] AudioMixer mixer;
    public static AudioManager instance;
    
    public const String MUSIC_KEY = "MusicVolume";
    public const String SFX_KEY = "SFXVolume";
    public const String MASTER_KEY = "MasterVolume";
    public bool isPlayingMenu = false;
    public bool isPlayingTheme = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        LoadVolume();
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }       
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void Start()
    {
        LoadVolume();
        string nombreDeEscena = SceneManager.GetActiveScene().name;
        if (nombreDeEscena.Equals("creditos"))
        {
            Play("FinalTheme");
        }
        else if (nombreDeEscena.Equals("StartMenu"))
        {
            Play("MenuTheme");
        }
        else
        {
            Play("Theme");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Lógica para reproducir sonidos asociados a la carga de escena
        string sceneName = scene.name;
        if (sceneName.Equals("creditos"))
        {
            if (isPlayingTheme)
            {
                Stop("Theme");
                isPlayingTheme = false;
            }
            Play("FinalTheme");
        }
        else if (sceneName.Equals("StartMenu"))
        {
            if (isPlayingTheme)
            {
                Stop("Theme");
                isPlayingTheme = false;
            }
            Play("MenuTheme");
            isPlayingMenu = true;
        }
        else
        {
            if (isPlayingMenu)
            {
                Stop("MenuTheme");
                isPlayingMenu = false;
            }
            Play("Theme");
            isPlayingTheme = true;
            
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }
    public void UnPause(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.UnPause();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        float generalVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        
        mixer.SetFloat(VolumeSettings.MIXER_MASTER_VOLUME, Mathf.Log10(generalVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_Music, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}
