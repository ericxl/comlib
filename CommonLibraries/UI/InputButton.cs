using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
public class InputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CrossPlatformInputManager.VirtualButton m_JumpVirtualButton;
    [SerializeField]
    private string buttonName = "Jump";

    private void OnEnable()
    {
        CreateVirtualAxes();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_JumpVirtualButton.Pressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_JumpVirtualButton.Released();
    }

    private void CreateVirtualAxes()
    {
        m_JumpVirtualButton = new CrossPlatformInputManager.VirtualButton(buttonName);
        CrossPlatformInputManager.RegisterVirtualButton(m_JumpVirtualButton);

    }

    private void OnDisable()
    {
        m_JumpVirtualButton.Remove();
    }
}


