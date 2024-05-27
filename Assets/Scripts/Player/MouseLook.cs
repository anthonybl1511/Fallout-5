using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    private Volume postProcess;
    private float sensitivity = 10;
    private Vector2 mouseDelta;

    private void Start()
    {
        instance = this;

        postProcess = GetComponent<Volume>();
    }

    public void CameraLook(Vector2 input)
    {
        if (!PipBoy.instance.getActive())
        {
            mouseDelta.y = input.y * Time.deltaTime * sensitivity;
            mouseDelta.y = Mathf.Clamp(mouseDelta.y, -85f, 85f);
            mouseDelta.x = input.x;
        }
    }

    public void SetSensitivity(float _sensitivity)
    {
        sensitivity = _sensitivity;
    }

    private void Update()
    {
        if(!PipBoy.instance.getActive())
        {

            postProcess.enabled = false;
        }
        else
        {
            postProcess.enabled = true;
        }

        transform.localRotation *= Quaternion.Euler(-mouseDelta.y, 0, 0);
        PlayerMovement.instance.transform.Rotate(Vector3.up * mouseDelta.x * Time.deltaTime * sensitivity);
    }
}
