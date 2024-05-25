using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    [SerializeField] private float mouseSensitivity = 2;
    [SerializeField] private InputManager InputManager;

    private float Xrotation;
    private Volume postProcess;

    private void Start()
    {
        instance = this;

        postProcess = GetComponent<Volume>();
    }

    private void Update()
    {

        if (!PipBoy.instance.getActive())
        {
            Vector2 mouseLook;
            mouseLook.y = InputManager.inputMaster.CameraLook.xAxis.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
            mouseLook.x = InputManager.inputMaster.CameraLook.yAxis.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

            Xrotation -= mouseLook.x;

            transform.localRotation = Quaternion.Euler(Mathf.Clamp(Xrotation, -90f, 90f), 0, 0);

            PlayerMovement.instance.transform.Rotate(Vector3.up * mouseLook.y);

            postProcess.enabled = false;
        }
        else
        {
            postProcess.enabled = true;
        }
        
    }
}
