using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicController : MonoBehaviour
{
    [field: SerializeField]
    private AudioSource MusicSource { get; set; }

    private void Start()
    {
        GameStateManager.Instance.OnPlaying += PlayMusic;
        GameStateManager.Instance.OnGameLost += StopMusic;
    }

    private void PlayMusic()
    {
        MusicSource.Play();
    }

    private void StopMusic()
    {
        MusicSource.Stop();
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnPlaying -= PlayMusic;
        GameStateManager.Instance.OnGameLost -= StopMusic;
    }
}
