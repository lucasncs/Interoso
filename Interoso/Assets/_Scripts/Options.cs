using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	public AudioMixer mainMixer;
	public Slider musicVolumeSlider;

	void Start()
	{
		float audioOut;
		mainMixer.GetFloat("MusicVolume", out audioOut);
		musicVolumeSlider.value = audioOut;
	}

	public void MusicVolumeUpdate(Slider volumeSlider)
	{
		mainMixer.SetFloat("MusicVolume", volumeSlider.value);
	}
}
