using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
