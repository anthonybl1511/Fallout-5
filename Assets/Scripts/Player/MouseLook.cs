using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    [SerializeField] private float mouseSensitivity = 2;

    private float Xrotation;
    private Volume postProcess;
    private float sensitivity = 1;

    private void Start()
    {
        instance = this;

        postProcess = GetComponent<Volume>();
    }

    public void CameraLook(Vector2 input)
    {
        if (!PipBoy.instance.getActive())
        {
            Xrotation -= input.y * Time.deltaTime * sensitivity;
            Xrotation = Mathf.Clamp(Xrotation, -85f, 85f);
            transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);

            PlayerMovement.instance.transform.Rotate(Vector3.up * input.x * Time.deltaTime * sensitivity);
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
    }
}
