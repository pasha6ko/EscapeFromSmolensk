using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Death playerDeath;
    
    private GameObject playerPref;
    private Vector3 point;

    private void Start()
    {
        playerDeath.OnDeath += SetPosition;

        playerPref = Instantiate(gameObject);
        playerPref.SetActive(false);
    }

    public void SetPosition()
    {
        if (point == null) return;

        playerPref.SetActive(true);
        playerPref.transform.position = point;

        CheckPoint cloneChekPoint = playerPref.GetComponent<CheckPoint>();

        cloneChekPoint.AddLastCheckPoint(point);
        ScoreCounter.Instance.death = playerPref.GetComponent<Death>();
        ScoreCounter.Instance.playerHP = playerPref.GetComponent<PlayerHP>();
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
    public void AddLastCheckPoint(Vector3 position)
    {
        point = position;
    }
}
