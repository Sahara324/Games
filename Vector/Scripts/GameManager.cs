using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool pause;
    public static float currentSpeed;
    private static float _score = 0;

    [SerializeField] private Movement _player;
    [SerializeField] private int[] _scoreForNextLevel;
    [SerializeField] private float[] _speedOfLevel;
    [SerializeField] private float startOrRestartSpeed;

    private Spawner _spawner;
    private int _coin;
    private bool _right = false;
    private int _difficultLevel = 0;
    private float _speedUpKoeff;
    private bool _upSpeed;

    public int DifficultLevel => _difficultLevel;
    
    void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
        _upSpeed = true;
        gm = this;
        currentSpeed = startOrRestartSpeed;
        _player.SetSpeed(startOrRestartSpeed);
        _speedUpKoeff = (_speedOfLevel[_difficultLevel] - currentSpeed) / 3;
    }

    void Update()
    {
        if (!pause)
        {
            SpeedUp(ref _upSpeed);
            _player.SetDirection(ref _right);
            
            if(_score == _scoreForNextLevel[_difficultLevel])
            {
                Debug.Log("Next Level");
                _difficultLevel++;
                NextLevel();
            }
        }
        else
        {
            SetPause(true);
        }
    }

    void ResetSpeed()
    {
        currentSpeed = startOrRestartSpeed;
        _player.SetSpeed(startOrRestartSpeed);
        _speedUpKoeff = (_speedOfLevel[_difficultLevel] - currentSpeed) / 3;
        _upSpeed = true;
    }

    public bool SpeedUp( ref bool upspeed)
    {   
        if (upspeed && FindObjectsOfType<Obstacle>().Length == 0) 
        {   
            if (currentSpeed < _speedOfLevel[_difficultLevel])
            {
                Debug.Log(currentSpeed);
                _spawner.SetPause(true);
                currentSpeed += Time.deltaTime * _speedUpKoeff;
                _player.SetSpeed(currentSpeed);
            }
            else
            {
                currentSpeed = _speedOfLevel[_difficultLevel];
                _player.SetSpeed(currentSpeed);
                _spawner.SetPause(false);
                upspeed = !upspeed;
            }
        }
        return upspeed;
    }

    // вынести в класс с UI
    public void ChangeScore(float sc)
    {
        _score += sc;
        FindObjectOfType<TextMeshProUGUI>().text = _score.ToString();
    }

    void SetPause(bool pause)
    {
        GameManager.pause = pause;
        _player.SetParticlePause(pause);
        _spawner.SetPause(pause);
    }

    public void Respawn()
    {
        _player.ResetPosition(new Vector3(0, -20, 0));
        ResetSpeed();
        SetPause(false);
        DestroyAllObstacle();
    }

    public void DestroyAllObstacle()
    {
        var dstr = FindObjectsOfType<Obstacle>();
        foreach (var obst in dstr) Destroy(obst.gameObject);
    }

    void NextLevel()
    {
        _speedUpKoeff = (_speedOfLevel[_difficultLevel]- currentSpeed)/3;
        _spawner.ChoseSuricen(_difficultLevel);
        _spawner.SetPause(true);
        _upSpeed = true;
    }
}
