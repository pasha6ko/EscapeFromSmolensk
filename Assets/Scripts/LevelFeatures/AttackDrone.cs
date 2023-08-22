using System.Collections;
using UnityEngine;

public class AttackDrone : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rig;
    [SerializeField] private GameObject bullet;
    private Transform _player;
    [Header("Settings")]
    [SerializeField] private bool freeRotation;
    [SerializeField] private float timeBetweenFire;

    private void Update()
    {
        if (_player == null) return;
        
            transform.LookAt(_player.position, Vector3.up);
        if (freeRotation) return;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        if (rig == null) return;

    }
    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(FireProcess());
        if (!other.transform.CompareTag("Player")) return;
        _player = other.transform;
        StartCoroutine(FireProcess());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        _player = null;
        StopCoroutine(FireProcess());
    }

    private IEnumerator FireProcess()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(timeBetweenFire);
        }
    }

    public void Fire()
    {
        if(_player == null) return;
        GameObject clone = Instantiate(bullet, firePoint.position, firePoint.rotation);
        clone.transform.LookAt(_player.position);
        Destroy(clone, 10f);
    }
}
