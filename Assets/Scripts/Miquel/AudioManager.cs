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

    // Audio Source Prefab
    [SerializeField] private GameObject customAudioSource;

    private void Awake()
    {
        instance = this;

        sounds = new()
        {
            {"CardReaderConfirmation", cardReaderConfirmation},
            {"CardReaderError", cardReaderError},

        };
    }

    public void PlaySoundAt(string name, Vector3 pos)
    {
        GameObject newAudioSource = Instantiate(customAudioSource, pos, Quaternion.identity);
        newAudioSource.GetComponent<CustomAudioSource>().PlayClip(sounds[name]);
    }
}
