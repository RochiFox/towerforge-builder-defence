using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float volume = 0.5f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);


        audioSource.volume = volume;
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;

        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;

        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float GetVolume() => volume;
}
