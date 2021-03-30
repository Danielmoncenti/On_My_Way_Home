using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour {
    public enum Sound { PLAYERJUMP, PLAYERDASH, PLAYERRUN, PLAYERDASHRETURN, PLAYERSPIKE, RAT, BAT, DROP, TRAP}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Play(Sound _sound) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(_sound));
    }

    private static AudioClip GetAudioClip(Sound _sound) {
        foreach (GameManagerController.SoundAudioClip soundAudioClip in GameManagerController.Instance.SoundAudioClipArray) {
            if (soundAudioClip.sound == _sound) {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}