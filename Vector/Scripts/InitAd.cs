using UnityEngine;
using UnityEngine.SceneManagement;

public class InitAd : MonoBehaviour
{
    //����� �������� ��������� ���� ��������� � ����� ����
    bool x = true;
    private void Start()
    {
        LoadLevel();
    }
    private void LoadLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
