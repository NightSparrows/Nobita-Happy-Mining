using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SoundEffect", menuName = "Audio/Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    public AudioClip[] clips;
    public Vector2 volumn = new Vector2(0.5f, 0.7f);
    public Vector2 pitch = new Vector2(0.8f, 1f);

    public void Play(AudioSource newSource = null)
    {
        if (clips.Length == 0)
        {
            Debug.LogWarning("SoundEffectSO w/o any audio clip");
            return;
        }

        AudioSource source = newSource;
        if (source == null)
        {
            GameObject obj = new GameObject("Sound", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
        }

        // Setup sound clip
        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(volumn.x, volumn.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();

        Destroy(source.gameObject, source.clip.length / source.pitch);
    }
}
