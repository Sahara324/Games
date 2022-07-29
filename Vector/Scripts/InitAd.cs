using UnityEngine;
using UnityEngine.SceneManagement;

public class InitAd : MonoBehaviour
{
    //после загрузки рекламной сети перевести в режим игры
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
