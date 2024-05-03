using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipBoyAnimFOV : MonoBehaviour
{
    [SerializeField] private Camera canvasCam;
    private Camera mainCam;
    [SerializeField] private float initialFOV;
    [SerializeField] private float focusedFOV;

    private bool isFocused = false;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void FocusOnPipboy()
    {
        isFocused = true;
    }

    private void Update()
    {
        if(isFocused)
        {
            canvasCam.fieldOfView = Mathf.Lerp(canvasCam.fieldOfView, focusedFOV, 3.2f * Time.deltaTime);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, focusedFOV / 2, 2 * Time.deltaTime);
        }
        else
        {
            canvasCam.fieldOfView = Mathf.Lerp(canvasCam.fieldOfView, initialFOV, 3.2f * Time.deltaTime);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, initialFOV, 2 * Time.deltaTime);
        }
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
