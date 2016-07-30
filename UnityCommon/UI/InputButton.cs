using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
public class InputButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
	
	CrossPlatformInputManager.VirtualButton m_JumpVirtualButton;
	[SerializeField] string buttonName = "Jump";

	void OnEnable() {
		CreateVirtualAxes();
	}

	public void OnPointerDown (PointerEventData eventData) {
		m_JumpVirtualButton.Pressed();
	}

	public void OnPointerUp (PointerEventData eventData) {
		m_JumpVirtualButton.Released();
	}

	void CreateVirtualAxes() {
		m_JumpVirtualButton = new CrossPlatformInputManager.VirtualButton(buttonName);
		CrossPlatformInputManager.RegisterVirtualButton(m_JumpVirtualButton);
		
	}
	void OnDisable(){
		m_JumpVirtualButton.Remove();
	}
}


