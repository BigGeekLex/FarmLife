using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButtonBase : MonoBehaviour, IActionButtonProvidable
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public Button GetButton()
    {
        return _button;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}