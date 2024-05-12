using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public Button closeButton; // ������ ��� �������� ����������
    public Button menuButton; // ������ ��� �������� �� ����
    public GameObject menuCanvas; // ��������� �� Canvas � ����
    public string menuSceneName; // ����� ����� ����

    void Start()
    {
        // ������ ������� �� ���� ������
        closeButton.onClick.AddListener(CloseInterface);
        menuButton.onClick.AddListener(GoToMenu);
    }

    public void CloseInterface()
    {
        // �������� ���������� (������������ ����)
        menuCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        // ������� �� ������� ����� (��������� ����)
        SceneManager.LoadScene(menuSceneName);
    }
}
