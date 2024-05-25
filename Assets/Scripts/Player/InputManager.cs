using UnityEngine;

public class InputManager : MonoBehaviour
{
    public NewControls inputMaster;

    private void Awake()
    {
        inputMaster = new NewControls();

        inputMaster.PipBoyInput.click.started += _ => PipBoy.instance.Click();
        inputMaster.PipBoyInput.OpenClose.started += _ => PipBoy.instance.OpenClose();
    }

    private void Update()
    {
        if (inputMaster.PipBoyInput.SwitchMenu.triggered)
        {
            PipBoy.instance.ChangeIndex((int)inputMaster.PipBoyInput.SwitchMenu.ReadValue<float>());
        }

        if (inputMaster.PipBoyInput.SwitchSubTabs.triggered)
        {
            PipBoy.instance.ChangeSubTabs((int)inputMaster.PipBoyInput.SwitchSubTabs.ReadValue<float>());
        }

        if (inputMaster.PipBoyInput.Navigate.triggered)
        {
            PipBoy.instance.navigate((int)inputMaster.PipBoyInput.Navigate.ReadValue<float>());
        }

        if (PipBoy.instance.getActive())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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
