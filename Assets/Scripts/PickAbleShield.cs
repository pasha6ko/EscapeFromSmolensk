using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickAbleShield : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerHP playerHP;
    [Header("Shield Settings")]
    [SerializeField, Range(0,10)] private float timeForShield = 5f;

    private Collider _myCollider;
    private MeshRenderer _myMeshRenderer;

    private void Start()
    {
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;

        _myCollider.enabled = false;
        _myMeshRenderer.enabled = false;

        StartCoroutine(NoDamage(other));
        
    }

    private IEnumerator NoDamage(Collider enemy)
    {
        playerHP.isArmored = true;

        yield return new WaitForSeconds(timeForShield);
        Destroy(gameObject);

        if (!playerHP.isArmored) yield break;
        playerHP.isArmored = false;
    }
}
