using UnityEngine.UI;

public interface IActionButtonProvidable
{
    public Button GetButton();

    public void Activate();

    public void Deactivate();
}