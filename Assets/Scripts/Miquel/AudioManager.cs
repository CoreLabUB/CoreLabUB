using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { if (instance == null) { instance = new AudioManager(); } return instance;  } }

    // Sounds are stored here
    Dictionary<string, AudioClip> sounds;

    // Assign Audio Clips
    [SerializeField] private AudioClip cardReaderConfirmation;
    [SerializeField] private AudioClip cardReaderError;

    [SerializeField] private AudioClip stickSwipping;
    [SerializeField] private AudioClip stickSuccess;

    // Audio Source Prefab
    [SerializeField] private GameObject customAudioSource;

    private void Awake()
    {
        instance = this;

        sounds = new()
        {
            {"CardReaderConfirmation", cardReaderConfirmation},
            {"CardReaderError", cardReaderError},

            { "StickSuccess", stickSuccess }
        };
    }

    public void PlaySoundAt(string name, Vector3 pos)
    {
        GameObject newAudioSource = Instantiate(customAudioSource, pos, Quaternion.identity);
        newAudioSource.AddComponent<SFXAudioSource>();
        newAudioSource.GetComponent<SFXAudioSource>().PlayClip(sounds[name]);
    }

    // WIP
    public void PlaySoundAttached(string name, Transform parentObject)
    {
        GameObject newAudioSource = Instantiate(customAudioSource, Vector3.zero, Quaternion.identity, parentObject);
        newAudioSource.GetComponent<CustomAudioSource>().PlayClip(sounds[name]);
    }
}
