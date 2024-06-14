using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // UI �������� ��� �����������
    public Toggle fullscreenToggle;
    public Slider cameraZoomSlider;
    public Slider placeRadiusSlider;
    public Slider destroyRadiusSlider;
    public Toggle disableTextToggle;
    public UnityEngine.UI.Text disableTextElement;

    // ����� ��� ��������� ����������� � PlayerPrefs
    private const string FullscreenPrefsKey = "Fullscreen";
    private const string CameraZoomPrefsKey = "CameraZoom";
    private const string PlaceRadiusPrefsKey = "PlaceRadius";
    private const string DestroyRadiusPrefsKey = "DestroyRadius";
    private const string TextElementEnabledPrefsKey = "TextElementEnabled";

    public Camera mainCamera; // ��������� �� ������� ������

    void Start()
    {
        // ϳ������ �� ��䳿 ���� ������ � ���������� ��������
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        cameraZoomSlider.onValueChanged.AddListener(OnCameraZoomChanged);
        placeRadiusSlider.onValueChanged.AddListener(OnPlaceRadiusChanged);
        destroyRadiusSlider.onValueChanged.AddListener(OnDestroyRadiusChanged);
        disableTextToggle.onValueChanged.AddListener(OnDisableTextToggleChanged);

        // ������������ ����������� ��� �������
        LoadSettings();
    }

    private void LoadSettings()
    {
        // ������������ ����������� � PlayerPrefs
        bool isFullscreen = GetBoolSetting(FullscreenPrefsKey, true);
        float cameraZoom = GetFloatSetting(CameraZoomPrefsKey, 1f);
        float placeRadius = GetFloatSetting(PlaceRadiusPrefsKey, 0f);
        float destroyRadius = GetFloatSetting(DestroyRadiusPrefsKey, 0f);
        bool isTextElementEnabled = GetBoolSetting(TextElementEnabledPrefsKey, true);

        // ������������ ������� � ������� UI ��������
        fullscreenToggle.isOn = isFullscreen;
        cameraZoomSlider.value = cameraZoom;
        placeRadiusSlider.value = placeRadius;
        destroyRadiusSlider.value = destroyRadius;
        disableTextToggle.isOn = isTextElementEnabled;
        disableTextElement.gameObject.SetActive(isTextElementEnabled);

        // ������������ ��������� �� ������� ������
        mainCamera = Camera.main;
    }

    private void OnFullscreenToggleChanged(bool isOn)
    {
        // ���� ����������� �� ��������� ������ �������������� ������
        Screen.fullScreen = isOn;
    }

    private void OnCameraZoomChanged(float value)
    {
        // ����� ���� ����������� ��� ��������� ������
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = value; // ���� ���� ������
        }
    }

    private void OnPlaceRadiusChanged(float value)
    {
        // ����� ���� ����������� ��� ������ ������������ �����
        // ��� ����� �������� �������� 䳿 ��� ���� ������ ������������ �����
    }

    private void OnDestroyRadiusChanged(float value)
    {
        // ����� ���� ����������� ��� ������ ���������� �����
        // ��� ����� �������� �������� 䳿 ��� ���� ������ ���������� �����
    }

    private void OnDisableTextToggleChanged(bool isOn)
    {
        // ���� ����� ���������� �������� �� ��������� ������
        disableTextElement.gameObject.SetActive(!isOn);
    }

    // ������� ������ ��� ���������� �� ������������ �����������

    public static void SetBoolSetting(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBoolSetting(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static void SetFloatSetting(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static float GetFloatSetting(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
}
