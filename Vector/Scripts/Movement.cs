using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mesh;
    [SerializeField] private Movement _player;
    [SerializeField] private ParticleSystem _particle;

    ParticleSystem m_particleSystem;
    ParticleSystem.ShapeModule _editableShape;
    ParticleSystem.MainModule _editableMain;

    private float _speed;

    void Start()
    {
        m_particleSystem = _particle.GetComponent<ParticleSystem>();
        _editableShape = m_particleSystem.shape;
        _editableMain = m_particleSystem.main;
        _editableMain.simulationSpeed = _speed/ Mathf.Sqrt(2) / 50f;
    }

    public void SetDirection(ref bool right)
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    right = right ? false : true;
                    RotateMesh(right);
                    break;
            }
        }
        Moving(right);
    }

    private void RotateMesh(bool right)
    {
        if (right)
        {
            _mesh.transform.rotation = Quaternion.Euler(0, 0, -45f);
            float i = _particle.transform.position.x;
            _editableShape.position = new Vector3(-1.54f, 0, 0);
        }
        else
        {
            _mesh.transform.rotation = Quaternion.Euler(0, 0, 45f);
            float i = _particle.transform.position.x;
            _editableShape.position = new Vector3(1.54f, 0, 0);
        }
        
    }

    public void Moving(bool right)
    {
        _editableMain.simulationSpeed = _speed / Mathf.Sqrt(2) / 50f;
        if (right)
        {
            _player.transform.position += transform.right * _speed * Time.deltaTime;
        }
        else
        {
            _player.transform.position -= transform.right * _speed * Time.deltaTime;
        }
    }

    public void SetParticlePause(bool pause)
    {
        if (pause)
        {
            _editableMain.simulationSpeed = 0;
        }
        else
        {
            _editableMain.simulationSpeed = _speed / Mathf.Sqrt(2) / 50f;
        }
    }

    public void SetSpeed(float speed)
    {
        if(speed >= 0)
        {
            _speed = speed;
        }
    }

    public void ResetPosition(Vector2 position)
    {
        _player.transform.position = position;
    }
}
