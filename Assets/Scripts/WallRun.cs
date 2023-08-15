using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallRun : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovment;
    [SerializeField] private GameObject wayVisualisation;
    [SerializeField] private Transform debugParent;

    [Header("Settings")]
    [SerializeField] private float pointDistance;
    [SerializeField] private int maxPointsCount;

    private List<Vector3> wayPoints;

    private void OnCollisionEnter(Collision collision)
    {
        //Если стена То надо
        BuildWay(collision,0);
    }
    public void ClearWay()
    {
        wayPoints.Clear();
    }
    public void StartWallRun()
    {
        
    }
    void Update()
    {
    }
    public void BuildWay(Collision collision, int iteration)
    {
        if (iteration > maxPointsCount)
        {
            StartWallRun();
            return;
        }
        Vector3 contactPoint = new Vector3(collision.GetContact(0).point.x, transform.position.y,collision.GetContact(0).point.z) ;
        Ray ray = new Ray(transform.position, contactPoint - gameObject.transform.position);
        Debug.DrawRay(gameObject.transform.position, contactPoint - gameObject.transform.position, Color.yellow,10f);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            print("Draw");
            Vector3 directionToRight = hit.transform.right;
            directionToRight = directionToRight.normalized - transform.forward;
            Vector3 directionToLeft = -hit.transform.right;
            directionToLeft = directionToLeft.normalized - transform.forward;
            Debug.DrawRay(transform.position, hit.transform.right * (directionToLeft.magnitude < directionToRight.magnitude ? -1 : 1),Color.red,10f);
        }
    }
}
