using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipBoyAnimFOV : MonoBehaviour
{
    [SerializeField] private Camera canvasCam;
    private Camera mainCam;
    [SerializeField] private float initialFOV;
    [SerializeField] private float focusedFOV;
    [SerializeField] private AudioSource IdleSound;

    private bool isFocused = false;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void FocusOnPipboy()
    {
        isFocused = true;
        StartCoroutine(AudioFade(IdleSound, 0.3f, 0.4f));
    }

    public void UnFocusOnPipboy()
    {
        isFocused = false;
        StartCoroutine(AudioFade(IdleSound, 0.3f, 0));
    }

    private void Update()
    {
        if(isFocused)
        {
            canvasCam.fieldOfView = Mathf.Lerp(canvasCam.fieldOfView, focusedFOV, 4f * Time.deltaTime);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, focusedFOV / 2, 3.6f * Time.deltaTime);
        }
        else
        {
            canvasCam.fieldOfView = Mathf.Lerp(canvasCam.fieldOfView, initialFOV, 0.8f * Time.deltaTime);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, initialFOV, 3.6f * Time.deltaTime);
        }
    }

    public static IEnumerator AudioFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
