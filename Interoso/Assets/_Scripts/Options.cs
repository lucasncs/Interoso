using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	public AudioMixer mainMixer;
	public Slider volumeSlider;

	void Start()
	{
		float audioout;
		mainMixer.GetFloat("MainVolume", out audioout);
		volumeSlider.value = audioout;
	}

	public void VolumeUpdate(Slider volumeSlider)
	{
		mainMixer.SetFloat("MainVolume", volumeSlider.value);
	}
}
