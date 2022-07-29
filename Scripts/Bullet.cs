using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _moveDirection;

    private void Start()
    {
        _moveDirection = Vector3.forward;
    }

    private void Update()
    {
        transform.Translate(_moveDirection * Time.deltaTime * _speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Block block))
        {
            block.Break();
            Destroy(gameObject);
        }
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            //game over in next Update
            Destroy(gameObject);
        }


    }
}
