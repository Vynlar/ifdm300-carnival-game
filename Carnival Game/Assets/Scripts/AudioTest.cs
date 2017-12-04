using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioTest : MonoBehaviour {

    public VideoPlayer player;
    public VideoClip clip;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.SetTargetAudioSource(0, audioSource);
        player.source = VideoSource.VideoClip;
        player.clip = clip;
        player.Play();
    }
}
