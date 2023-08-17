using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Death playerDeath;
    private GameObject playerPref;

    private List<Vector3> _points = new List<Vector3>();

    private void Start()
    {
        _points.Add(transform.position);
        playerDeath.OnDeath += SetPosition;
        playerPref = Instantiate(gameObject);
        playerPref.SetActive(false);
    }

    public void SetPosition()
    {
        if (_points.Count == 0) return;

        playerPref.SetActive(true); 
        playerPref.transform.position = _points[_points.Count - 1];
        CheckPoint cloneChekPoint = playerPref.GetComponent<CheckPoint>();
        cloneChekPoint.AddLastCheckPoint(_points[_points.Count - 1]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Checkpoint")) return;

        Vector3 checkpointPosition = collision.gameObject.transform.position;
        checkpointPosition = new Vector3(checkpointPosition.x, transform.position.y, checkpointPosition.z);

        if (IsCheckpointCollected(checkpointPosition)) return;

        AddLastCheckPoint(checkpointPosition);
    }
    public void AddLastCheckPoint(Vector3 position)
    {
        _points.Add(position);
    }

    private bool IsCheckpointCollected(Vector3 checkingCheckpointPosition)
    {
        foreach (Vector3 collectedCheckpoint in _points)
        {
            if (Mathf.Approximately(checkingCheckpointPosition.x, collectedCheckpoint.x)
                && Mathf.Approximately(checkingCheckpointPosition.z, collectedCheckpoint.z))
            {
                return true;
            }
        }
        return false;
    }
}
