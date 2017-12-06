using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	public AudioMixer mainMixer;
	public Slider musicVolumeSlider, sfxVolumeSlider;

	void Start()
	{
		float audioOut;
		mainMixer.GetFloat("MusicVolume", out audioOut);
		musicVolumeSlider.value = audioOut;

		mainMixer.GetFloat("SfxVolume", out audioOut);
		sfxVolumeSlider.value = audioOut;
	}

	public void MusicVolumeUpdate(Slider volumeSlider)
	{
		mainMixer.SetFloat("MusicVolume", volumeSlider.value);
	}

	public void SFXVolumeUpdate(Slider volumeSlider)
	{
		mainMixer.SetFloat("SfxVolume", volumeSlider.value);
	}
}
