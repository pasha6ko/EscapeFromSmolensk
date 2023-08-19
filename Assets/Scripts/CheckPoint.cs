using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Death playerDeath;

    private GameObject _playerPref;
    private Vector3 _point;

    private void Start()
    {
        playerDeath.OnDeath += SetPosition;

        _playerPref = Instantiate(gameObject);
        _playerPref.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Checkpoint")) return;

        GameObject checkpoint = collision.gameObject;
        checkpoint.transform.tag = "Untagged";

        Vector3 checkpointPosition = collision.gameObject.transform.position;
        checkpointPosition = new Vector3(checkpointPosition.x, transform.position.y, checkpointPosition.z);

        AddLastCheckPoint(checkpointPosition);
    }

    public void SetPosition()
    {
        if (_point == null) return;

        _playerPref.SetActive(true);
        _playerPref.transform.position = _point;

        CheckPoint cloneChekPoint = _playerPref.GetComponent<CheckPoint>();

        cloneChekPoint.AddLastCheckPoint(_point);
        ScoreCounter.Instance.death = _playerPref.GetComponent<Death>();
        ScoreCounter.Instance.playerHP = _playerPref.GetComponent<PlayerHP>();
    }

    public void AddLastCheckPoint(Vector3 position)
    {
        _point = position;
    }
}
