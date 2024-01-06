using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
   public AudioClip clip;
   public string name;
   [Range(0f,1f)]
   public float volume;
   
   public bool loop;
   public bool playOnAwake;
   public AudioMixerGroup mixerGroup;

   [HideInInspector]
   public AudioSource source;

}
