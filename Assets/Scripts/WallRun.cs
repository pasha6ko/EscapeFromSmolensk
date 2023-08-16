using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerMovement;

public class WallRun : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovment;
    [SerializeField] private GameObject wayVisualisation;
    [SerializeField] private Transform debugParent;
    [SerializeField] private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float pointDistance;
    [SerializeField] private int maxPointsCount;
    [SerializeField] private float maxRayChekLenght;
    [SerializeField] private float distanceFromWall;
    private float index;

    private List<Vector3> wayPoints = new List<Vector3>();
    public enum WallWayDiraction
    {
        None,
        Right = 1,
        Left = -1,
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Wall")) return;
        ClearWay();
        Vector3 contactPoint = new Vector3(collision.GetContact(0).point.x, transform.position.y, collision.GetContact(0).point.z);
        Vector3 rayDiraction = contactPoint - gameObject.transform.position;
        BuildWay(transform.position,rayDiraction,maxPointsCount);
    }
    public void ClearWay()
    {
        wayPoints.Clear();
    }
    public void StartWallRun()
    {
        print("StartWallRun");
        playerMovment.movementState = PlayerMovement.MovementStates.WallRun;
    }
    
    void Update()
    {
        if (playerMovment.movementState != PlayerMovement.MovementStates.WallRun) return;
        if (wayPoints.Count <= 2)
        {
            playerMovment.movementState = PlayerMovement.MovementStates.InAir;
            ClearWay();
            return;
        }
        index += Time.deltaTime * speed/pointDistance;
        rb.position = Vector3.Lerp(wayPoints[0], wayPoints[1], index);
        if (index < 1) return;     
        index = 0;
        wayPoints.RemoveAt(0);
                
        
        
    }
    
    public void BuildWay(Vector3 rayStart,Vector3 rayDiraction, int iteration, WallWayDiraction wayDirection = WallWayDiraction.None)
    {
        if (iteration <= 0)
        {
            StartWallRun();
            return;
        }
        Ray ray = new Ray(rayStart, rayDiraction);
        Debug.DrawRay(rayStart, rayDiraction, Color.yellow,10f);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayChekLenght))
        {
            GameObject hitPoint = new GameObject();
            hitPoint.transform.position = hit.point;
            hitPoint.transform.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
            if (wayDirection == WallWayDiraction.None)
            {
                Vector3 directionToRight = hitPoint.transform.right;
                directionToRight = directionToRight.normalized - transform.forward;
                Vector3 directionToLeft = -hitPoint.transform.right;
                directionToLeft = directionToLeft.normalized - transform.forward;
                wayDirection = (directionToLeft.magnitude < directionToRight.magnitude ? WallWayDiraction.Left : WallWayDiraction.Right);
            }
            Debug.DrawRay(rayStart, hitPoint.transform.right * (int)wayDirection * pointDistance, Color.red, 10f);
            Vector3 nextRayStart = hit.point + hitPoint.transform.forward * distanceFromWall + hitPoint.transform.right * (int)wayDirection * pointDistance;
            Vector3 nextRayDiraction = -hitPoint.transform.forward;
            wayPoints.Add(hitPoint.transform.position+hitPoint.transform.forward * distanceFromWall);
            BuildWay(nextRayStart, nextRayDiraction, --iteration, wayDirection);
            Destroy(hitPoint);

        }
        else
        {
            StartWallRun();
        }
    }
}
