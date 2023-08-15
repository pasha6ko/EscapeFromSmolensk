    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CalculateWallWay : MonoBehaviour
{/*
    [SerializeField] GameObject Ball;
    [Range(0.1f, 2f)] public float Width;
    [Range(4, 50)] public int lenght;
    List<List<Vector3>> LinesToDraw = new List<List<Vector3>>();
    PlayerController playerController;
    [SerializeField] LayerMask _ignorMask;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position+ transform.forward);
        foreach (List<Vector3> i in LinesToDraw)
        {
            Debug.DrawLine(i[0], i[1], Color.red);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.gameObject.layer);    
        if (collision.collider.gameObject.layer== 6 && playerController.WRLineData.Count==0 )
        {
            RaycastHit hit;
            print("TryRaycast");
            Ray ray = new Ray(transform.position,collision.GetContact(0).point - gameObject.transform.position);
            Debug.DrawRay(gameObject.transform.position,  collision.GetContact(0).point - gameObject.transform.position, Color.yellow);
            if (Physics.Raycast(ray, out hit, 10f))
            {
                print("Raycast");
                
                playerController.ChangeState(PlayerController.States.WallRun);
                LinesToDraw.Add(new List<Vector3>() { hit.point, hit.point + hit.normal * 1 });
                Instantiate(Ball, hit.point + hit.normal * 1, Quaternion.Euler(0, 0, 0), GameObject.Find("WRLines").transform);
                float Diraction;
                //Time.timeScale = 0f;
                Vector3 LookDifference = Quaternion.LookRotation(transform.position - transform.forward - hit.point).eulerAngles - Quaternion.LookRotation(hit.normal).eulerAngles;
                print(LookDifference.x);
                if (LookDifference.x > 75f && LookDifference.x < -75f)
                {
                    Debug.LogError(LookDifference);
                    playerController.ReGenJumps();
                    return;
                }
                if (LookDifference.y > 0 && LookDifference.y < 180)
                {
                    Diraction = 1;
                }
                else
                {
                    Diraction = -1;
                }
                Vector3 LinePoint = hit.point + hit.normal * 1;
                Vector3 TargerPoint = new Vector3(Width * hit.normal.z * -Diraction, 0, Width * hit.normal.x * Diraction) + LinePoint;
                print("Before");
                playerController.ChangeCameraRot(-20f*Diraction);
                playerController.WRLineData.Add(TargerPoint);
                RaycastLine(TargerPoint, hit, lenght, Diraction);
                
            }
            else {
                
                for (int i = 0; i < collision.contactCount; i++)
                {
                    print("Vector 3 ray point: " + collision.GetContact(i).point);
                    Debug.DrawRay(gameObject.transform.position, collision.GetContact(i).point - gameObject.transform.position, Color.blue);
                }
                Debug.DrawRay(gameObject.transform.position, collision.GetContact(0).point -gameObject.transform.position,Color.blue);
                Debug.LogError("No Raycast to the wall");   
            }
        }
    }


    void RaycastLine(Vector3 LinePosition, RaycastHit lastHit, int repeat, float Diraction)
    {
        if(repeat > 0)
        {
            //print(repeat);
            RaycastHit HitForoward;
            if (Physics.Raycast(LinePosition, (lastHit.point + lastHit.normal) - LinePosition, out HitForoward, Width))
            {
                Debug.LogWarning("No Way Foroword");
                return;

            }
            RaycastHit hit;
            Debug.DrawRay(LinePosition, -lastHit.normal,Color.black);
            if (Physics.Raycast(LinePosition, -lastHit.normal, out hit, 2f))
            {
                LinesToDraw.Add(new List<Vector3>() { hit.point, hit.point + hit.normal * 1 });

                Vector3 LinePoint = hit.point + hit.normal * 1;
                Vector3 TargerPoint = new Vector3(Width * hit.normal.z * -Diraction, 0, Width * hit.normal.x * Diraction) + LinePoint;
                LinesToDraw.Add(new List<Vector3>() { LinePoint, TargerPoint });
                repeat --;
                playerController.WRLineData.Add(TargerPoint);
                RaycastLine(TargerPoint, hit, repeat, Diraction);
            }
            else
            {
            }
        }
        else playerController.ChangeState(PlayerController.States.WallRun);
    }*/
}

