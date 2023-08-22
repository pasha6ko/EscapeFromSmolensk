using System.Collections;
using UnityEngine;

public class PickAbleShield : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField, Range(0, 10)] private float timeForShield = 5f;

    private Collider _myCollider;
    private MeshRenderer _myMeshRenderer;
    private PlayerHP _playerHP;

    private void Start()
    {
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;

        _playerHP = other.GetComponent<PlayerHP>();

        _myCollider.enabled = false;
        _myMeshRenderer.enabled = false;

        StartCoroutine(NoDamage(other));

    }

    private IEnumerator NoDamage(Collider enemy)
    {
        _playerHP.isArmored = true;

        yield return new WaitForSeconds(timeForShield);
        Destroy(gameObject);

        if (!_playerHP.isArmored) yield break;
        _playerHP.isArmored = false;
    }
}
