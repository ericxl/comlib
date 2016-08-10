using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using VirtusArts.UI;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPixelScale
{
    [SerializeField] private int MovementRadius = 75;
    private float _movement_radius;
    [SerializeField] private float StickAlpha = 0.5f;
    [SerializeField] private float ElasticSpeed = 180.0f;
    [SerializeField] private AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
    [SerializeField] private string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
    [SerializeField] private string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

    [SerializeField] private Image BaseStick;
    [SerializeField] private Image WheelStick;

    [SerializeField] private float m_fadeTime = 0.3f;
    private float m_fading = 0.0f;
    private bool m_isDragging = false;

    private float _pixel_ratio = 1;

    private Vector3 m_AnchorPos;
    private Vector3 m_CurrentPos;
    private bool m_UseX; // Toggle for using the x axis
    private bool m_UseY; // Toggle for using the Y axis

    private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
    private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

    public float PixelRatio
    {
        get
        {
            return _pixel_ratio;
        }
        set
        {
            _pixel_ratio = value;
            _movement_radius = MovementRadius / value;
        }
    }

    private void Start()
    {
        //_pixel_ratio = 1.0f / (float)scaler.scaleFactor;
        //_movement_radius = MovementRadius / _pixel_ratio;
    }


    private void OnEnable()
    {
        CreateVirtualAxes();
    }

    private void Update()
    {
        if (!m_isDragging && BaseStick.color.a > 0)
        {
            m_fading = Mathf.Clamp(m_fading + Time.unscaledDeltaTime, 0, m_fadeTime);
            BaseStick.color = new Color(1, 1, 1, Mathf.Lerp(StickAlpha, 0, m_fading / m_fadeTime));
            WheelStick.color = new Color(1, 1, 1, Mathf.Lerp(StickAlpha, 0, m_fading / m_fadeTime));
            WheelStick.rectTransform.anchoredPosition = Vector2.MoveTowards(WheelStick.rectTransform.anchoredPosition, BaseStick.rectTransform.anchoredPosition, ElasticSpeed * Time.unscaledDeltaTime);

        }
        else
        {
            m_fading = 0;
        }
    }

    private void CreateVirtualAxes()
    {
        // set axes to use
        m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
        m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

        // create new axes based on axes to use
        if (m_UseX)
        {
            m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
        }
        if (m_UseY)
        {
            m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
        }
    }

    private void UpdateVirtualAxis(Vector3 value)
    {
        var delta = m_AnchorPos - value;

        //float radius = MovementRadius / UIRoot.GetPixelSizeAdjustment(gameObject);
        delta /= -_movement_radius;

        if (m_UseX)
        {
            m_HorizontalVirtualAxis.Update(delta.x);
        }

        if (m_UseY)
        {
            m_VerticalVirtualAxis.Update(delta.y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_AnchorPos = eventData.position;
        m_CurrentPos = m_AnchorPos;

        BaseStick.rectTransform.anchoredPosition = m_AnchorPos * _pixel_ratio;
        WheelStick.rectTransform.anchoredPosition = m_CurrentPos * _pixel_ratio;

        BaseStick.color = new Color(1, 1, 1, StickAlpha);
        WheelStick.color = new Color(1, 1, 1, StickAlpha);
        m_isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = Vector3.zero;
        if (m_UseX)
        {
            delta.x = eventData.position.x - m_AnchorPos.x;
        }
        if (m_UseY)
        {
            delta.y = eventData.position.y - m_AnchorPos.y;
        }
        m_CurrentPos = delta + m_AnchorPos;
        var offset = m_CurrentPos - m_AnchorPos;

        var wheelOffset = offset;
        //float ratio = UIRoot.GetPixelSizeAdjustment(gameObject);
        //float radius = MovementRadius / ratio;
        if (offset.magnitude > _movement_radius)
        {
            wheelOffset = Vector3.ClampMagnitude(offset, _movement_radius);

            var baseOffset = offset - wheelOffset;
            m_AnchorPos += baseOffset;
            //BaseStick.transform.localPosition = m_AnchorPos * _pixel_ratio;
            BaseStick.rectTransform.anchoredPosition = m_AnchorPos * _pixel_ratio;
        }
        var wheelPos = new Vector3(m_AnchorPos.x + wheelOffset.x, m_AnchorPos.y + wheelOffset.y, m_AnchorPos.z + wheelOffset.z);
        WheelStick.rectTransform.anchoredPosition = wheelPos * _pixel_ratio;
        UpdateVirtualAxis(wheelPos);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UpdateVirtualAxis(m_AnchorPos);

        m_isDragging = false;
    }

    private void OnDisable()
    {

        if (m_UseX)
        {
            m_HorizontalVirtualAxis.Remove();
        }
        if (m_UseY)
        {
            m_VerticalVirtualAxis.Remove();
        }


    }

    public enum AxisOption
    {
        // Options for which axes to use
        Both, // Use both
        OnlyHorizontal, // Only horizontal
        OnlyVertical // Only vertical
    }
}
