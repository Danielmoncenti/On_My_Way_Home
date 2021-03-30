using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour {
    public enum Sound { PLAYERJUMP, PLAYERDASH, PLAYERRUN, PLAYERDASHRETURN, PLAYERSPIKE, RAT, BAT, DROP, TRAP}

    public static SoundAudioClip[] SoundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }


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
        int size = SoundAudioClipArray.Length;
        for (int i=0; i < size;i++) {
            if (SoundAudioClipArray[i].sound == _sound) {
                return SoundAudioClipArray[i].audioClip;
            }
        }
        return null;
    }
}