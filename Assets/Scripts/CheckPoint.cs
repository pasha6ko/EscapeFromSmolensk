using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerHP playerHP;

    private List<Vector3> _points = new List<Vector3>();

    private void Start()
    {
        _points.Add(transform.position);

        playerHP.OnDeath += SetPosition;
    }

    public void SetPosition()
    {
        if (_points.Count == 0) return;

        transform.position = _points[_points.Count - 1];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Checkpoint")) return;

        Vector3 checkpointPosition = collision.gameObject.transform.position;
        checkpointPosition = new Vector3(checkpointPosition.x, transform.position.y, checkpointPosition.z);

        if (IsCheckpointCollected(checkpointPosition)) return;

        _points.Add(checkpointPosition);
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
