using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rb2d;
    public bool pause;
    public bool setscore= false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -GameManager.currentSpeed/ Mathf.Sqrt(2));
    }
    public void Update()
    {
        if (GameManager.pause)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        if(gameObject.transform.position.y < -20 && !setscore)
        {
            setscore = true;
            GameManager.gm.ChangeScore(0.5f);   
        }
    }
}
