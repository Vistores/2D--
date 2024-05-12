using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    public Button startButton; // ������ ��� �������� �� �������� �����
    public Button exitButton; // ������ ��� ������ � ���
    public string nextSceneName; // ����� �������� �����

    void Start()
    {
        // ������ ������� �� ���� ������
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame()
    {
        // ������� �� �������� ����� (���� ���� �)
        SceneManager.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
        // �������� ���
        UnityEngine.Application.Quit();
    }
}
