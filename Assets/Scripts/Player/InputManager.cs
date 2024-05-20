using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public NewControls inputMaster;

    private void Awake()
    {
        inputMaster = new NewControls();

        inputMaster.PipBoyInput.click.started += _ => PipBoy.instance.Click();
    }

    private void Update()
    {
        if(inputMaster.PipBoyInput.Navigate.triggered)
        {
            PipBoy.instance.ChangeIndex((int)inputMaster.PipBoyInput.SwitchTabs.ReadValue<float>());
        }

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}