using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
   [SerializeField] AudioMixer mixer;
   [SerializeField] Slider masterVolumeSlider;   
   [SerializeField] Slider musicSlider;
   [SerializeField] Slider sfxSlider;

   public const String MIXER_MASTER_VOLUME = "MasterVolume";
   public const String MIXER_SFX = "SFXVolume";
   public const String MIXER_Music = "MusicVolume";
   void Awake()
   {
      musicSlider.onValueChanged.AddListener(SetMusicVolume);
      sfxSlider.onValueChanged.AddListener(SetSFXVolume);
      masterVolumeSlider.onValueChanged.AddListener(SetGeneralVolume);
   }

   void OnEnable()
   {
      musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
      sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
      masterVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
   }

   void SetMusicVolume(float value)
   {
      if (value == 0)
      {
         mixer.SetFloat(MIXER_Music, -80f); // O cualquier otro valor que desees para representar el volumen mínimo
      }
      else
      {
         mixer.SetFloat(MIXER_Music, Mathf.Log10(value) * 20);
      }
      PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, value);
      PlayerPrefs.Save();
   }
   void SetSFXVolume(float value)
   {
      if (value == 0)
      {
         mixer.SetFloat(MIXER_SFX, -80f);
      }
      else
      {
         mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
      }
      PlayerPrefs.SetFloat(AudioManager.SFX_KEY, value);
      PlayerPrefs.Save();
   }
   void SetGeneralVolume(float value)
   {
      if (value == 0)
      {
         mixer.SetFloat(MIXER_MASTER_VOLUME, -80f);
      }
      else
      {
         mixer.SetFloat(MIXER_MASTER_VOLUME, Mathf.Log10(value) * 20);
      }
      PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, value);
      PlayerPrefs.Save();
   }
}
