using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Volume volume;

    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private bool fullScreen;

    private void Start()
    {
        dropDown.onValueChanged.AddListener(delegate { ChangeResolution(dropDown); });
    }
    public void ChangeResolution(TMP_Dropdown dropDown)
    {
        string optionText = dropDown.options[dropDown.value].text;

        if (optionText == "1920x1080") {
            Screen.SetResolution(1920, 1080, fullScreen);
        }

        else if (optionText == "1536x864")
        {
            Screen.SetResolution(1536, 864, fullScreen);
        }
        else if(optionText == "2560x1440")
        {
            Screen.SetResolution(2560, 1440, fullScreen);
        }
        else if (optionText == "800x600")
        {
            Screen.SetResolution(800, 600, fullScreen);
        }
        else if (optionText == "3840x1200")
        {
            Screen.SetResolution(2840, 1200, fullScreen);
        }
    }

    public void SetFullScreen()
    {

        if (fullScreen)
        {

            fullScreen = false;
        }
        else
        {

            fullScreen = true;
        }

        Screen.fullScreen = fullScreen;
    }

    public void SetVSync()
    {
        if (QualitySettings.vSyncCount == 0)
        {

            QualitySettings.vSyncCount = 4;
        }
        else
        {

            QualitySettings.vSyncCount = 0;
        }
    }

    public void SetHighContrast()
    {
        if (volume.enabled)
        {

            volume.enabled = false;
        }
        else
        {

            volume.enabled = true;
        }
    }

    public void ChangeMusic()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
    }
    public void ChangeSFX()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);
    }
}
