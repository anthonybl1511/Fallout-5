using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    [SerializeField] private float mouseSensitivity = 2;
    [SerializeField] private InputManager InputManager;

    private float Xrotation;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Vector2 mouseLook;
        mouseLook.y = InputManager.inputMaster.CameraLook.xAxis.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        mouseLook.x = InputManager.inputMaster.CameraLook.yAxis.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        Xrotation -= mouseLook.x;

        transform.localRotation = Quaternion.Euler(Mathf.Clamp(Xrotation, -90f, 90f),0, 0);

        PlayerMovement.instance.transform.Rotate(Vector3.up * mouseLook.y);
    }
}
