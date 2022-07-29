using UnityEngine;

public class ColisionDetection : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        GameManager.pause = true;
    }
}
