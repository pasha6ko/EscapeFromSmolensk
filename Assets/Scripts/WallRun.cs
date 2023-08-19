using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public enum WallWayDiraction
    {
        None,
        Right = 1,
        Left = -1,
    }

    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovment;
    [SerializeField] private Rigidbody rb;

    [Header("WallRun Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float pointDistance;
    [SerializeField] private int maxPointsCount;
    [SerializeField] private float maxRayChekLenght;
    [SerializeField] private float distanceFromWall;

    private float _index;
    private List<Vector3> _wayPoints = new List<Vector3>();

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Wall")) return;
        ClearWay();
        Vector3 contactPoint = new Vector3(collision.GetContact(0).point.x, transform.position.y, collision.GetContact(0).point.z);
        Vector3 rayDiraction = contactPoint - gameObject.transform.position;
        BuildWay(transform.position, rayDiraction, maxPointsCount);
    }

    private void Update()
    {
        if (playerMovment.movementState != PlayerMovement.MovementStates.WallRun) return;
        if (_wayPoints.Count <= 2)
        {
            OnJump();
            return;
        }
        _index += Time.deltaTime * speed / pointDistance;
        rb.position = Vector3.Lerp(_wayPoints[0], _wayPoints[1], _index);
        if (_index < 1) return;
        _index = 0;
        _wayPoints.RemoveAt(0);
    }

    public void ClearWay()
    {
        _wayPoints.Clear();
    }

    public void StartWallRun()
    {
        print("StartWallRun");
        rb.useGravity = false;
        playerMovment.movementState = PlayerMovement.MovementStates.WallRun;
    }

    public void OnJump()
    {
        if (playerMovment == null) return;
        rb.useGravity = true;
        playerMovment.movementState = PlayerMovement.MovementStates.Run;
        playerMovment.OnJump();
        ClearWay();
    }

    public void BuildWay(Vector3 rayStart, Vector3 rayDiraction, int iteration, WallWayDiraction wayDirection = WallWayDiraction.None)
    {
        if (iteration <= 0)
        {
            StartWallRun();
            return;
        }
        Ray ray = new Ray(rayStart, rayDiraction);
        Debug.DrawRay(rayStart, rayDiraction, Color.yellow, 10f);
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
            _wayPoints.Add(hitPoint.transform.position + hitPoint.transform.forward * distanceFromWall);
            BuildWay(nextRayStart, nextRayDiraction, --iteration, wayDirection);
            Destroy(hitPoint);

        }
        else
        {
            StartWallRun();
        }
    }
}
