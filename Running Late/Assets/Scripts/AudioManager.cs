using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource gameAudioSource;

    [SerializeField]
    AudioClip carHonk, energyPick, coinPick;


    void Start()
    {
        gameAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    void PlayClip(AudioClip clip)
    {
        gameAudioSource.clip = clip;
        gameAudioSource.Play();
    }

    public void CarHonk()
    {
        PlayClip(carHonk);
    }

    public void Collect(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.energy:
                PlayClip(energyPick);
                break;

            case CollectibleType.cash:
                PlayClip(coinPick);
                break;

            case CollectibleType.scooter:
            default:
                break;
        }
    }
}
