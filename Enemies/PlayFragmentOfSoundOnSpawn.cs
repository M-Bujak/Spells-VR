using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFragmentOfSoundOnSpawn : MonoBehaviour
{
    [field: SerializeField]
    private AudioSource Audio { get; set; }
    [field: SerializeField]
    private float PlayTime { get; set; }

    private float StartTime { get; set; }

    private void Awake()
    {
        StartTime = Random.Range(0, Audio.clip.length - PlayTime - 1);
        Audio.time = StartTime;
        Audio.Play();
        StartCoroutine(StopAfterTimePassed());
    }

    IEnumerator StopAfterTimePassed()
    {
        while(Audio.time < StartTime + PlayTime)
        {
            yield return null;
        }

        Audio.Stop();
    }
}
