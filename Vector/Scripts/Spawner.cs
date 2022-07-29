using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool _pause;

    [SerializeField] private bool[] _suricenlevel;
    [SerializeField] private float[] _holesize;
    [SerializeField] private Obstacle[] _obstaclesPrefabs;

    private float distance;
    private float lastHole;
    private bool suricenLevel;

    private void Start()
    {
        suricenLevel = _suricenlevel[GameManager.gm.DifficultLevel];
    }

    void Update()
    {
        if (!_pause) 
        {
            distance += GameManager.currentSpeed / Mathf.Sqrt(2) * Time.deltaTime;
            
            if (distance >= 20 && !suricenLevel)
            {
                float nextHole = Random.Range(lastHole - distance * Mathf.Tan(Mathf.PI / 4), lastHole + distance * Mathf.Tan(Mathf.PI / 4));
                
                if (nextHole > 20f) nextHole = 20f;
                if (nextHole < -20f) nextHole = -20f;

                distance = 0;

                if (GameManager.gm.DifficultLevel > 0 && GameManager.gm.DifficultLevel < (_holesize.Length - 1))
                {
                    //float holesize = Random.Range(57.5f, 70f);
                    float holesize = Random.Range(_holesize[GameManager.gm.DifficultLevel - 1], _holesize[GameManager.gm.DifficultLevel + 1]);
                    SpawnTwoWall(nextHole, holesize);
                    Debug.LogWarning(holesize);
                }

                else
                {
                    SpawnTwoWall(nextHole, _holesize[GameManager.gm.DifficultLevel]);
                }
            }
            if (distance >= 15 && suricenLevel)
            {
                distance = 0;
                SpawnSuricen();
                SpawnSuricen();
            }
        }
    }

    void SpawnSuricen()
    {
        Vector3 pos = new Vector3(Random.Range(-20f, 20f), gameObject.transform.position.y, 0);
        Instantiate(_obstaclesPrefabs[1], pos, new Quaternion()).GetComponent<Obstacle>();
    }

    public void SpawnOneWall(bool right)
    {
        Vector3 pos = right ?
            new Vector3(3.5f, gameObject.transform.position.y, 0) :
            new Vector3(-3.5f, gameObject.transform.position.y, 0);

        var odj = right ?
            Instantiate(_obstaclesPrefabs[0], pos, new Quaternion()).GetComponent<Obstacle>():
            Instantiate(_obstaclesPrefabs[0], pos, Quaternion.Euler(0, 0, 180)).GetComponent<Obstacle>();
    }

    public void SpawnTwoWall(float holePos, float distance)
    {
        //сложная сложность дистанции 6
        //оптимальная сложность дистанции 7
        float rightpos = holePos + distance / 2;
        float leftpos = holePos - distance / 2;

        Instantiate(_obstaclesPrefabs[0], new Vector3(rightpos, gameObject.transform.position.y, 0), new Quaternion()).GetComponent<Obstacle>();
        Instantiate(_obstaclesPrefabs[0], new Vector3(leftpos, gameObject.transform.position.y, 0), Quaternion.Euler(0, 0, 180)).GetComponent<Obstacle>();
        lastHole = holePos;
    }

    public void ChoseSuricen(int difficultLevel)
    {
        suricenLevel = _suricenlevel[difficultLevel];
    }

    public void SetPause(bool pause)
    {
        _pause = pause;
    }
}
