using TMPro;
using UnityEngine;

public class SetRealTime : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        m_TextMeshProUGUI.text = System.DateTime.Now.ToString("HH:mm");
    }
}
