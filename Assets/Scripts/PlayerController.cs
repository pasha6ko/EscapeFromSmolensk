
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int hp; //Отделить
    [SerializeField]Transform camera;
    [Range(0.1f, 10f)] public float sensivity;
    [Range(0.1f ,1000f)] public float StandartSpeed,RotationSpeed;
    [Range(0.1f, 2000f)] public float JumpForce;
    [SerializeField] LayerMask layerGround;
    [SerializeField] int MaxJumps;
    public List<Vector3> WRLineData = new List<Vector3>();
    int JumpsCount;
    [Range(0, 200f)] public float WRSpeed;
    float IBetweenPoints;
    Rigidbody rb;
    public MovementStates states;
    public enum GunStates
    {
        Fire,
        Idle,
        Reload,
    }
    GunStates gunState;

    public enum MovementStates
    {
        Stay,
        Walk,
        Run,
        WallRun,
        Fall
    }
    public MovementStates PlayerState;

    void Start()
    {

        hp = 100;

        PlayerState = MovementStates.Walk;
        Cursor.lockState = CursorLockMode.Locked;
        gunState = GunStates.Idle;
        rb = GetComponent<Rigidbody>();
        JumpsCount = MaxJumps;

        
    }

    // Update is called once per frame
    void Update()
    {
        //Отдельный скрипт
        //Новая система ввода
        #region Rotation
        float eulerX = (camera.transform.rotation.eulerAngles.x + -Input.GetAxis("Mouse Y") * sensivity) % 360;
        float eulerY = (transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensivity) % 360;
        camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, camera.transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(0, eulerY, 0);
        #endregion
        //Отдельный скрипт
        
        #region Movement

        float speed = StandartSpeed;
        //переделать

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = StandartSpeed * 1.3f;
        }
        //Новая система ввода
        float HMove = Input.GetAxisRaw("Horizontal");
        float VMove = Input.GetAxisRaw("Vertical");
        //Пердвижение переделать
        if (PlayerState != MovementStates.WallRun)
        {
            float VerticalVelocity = rb.velocity.y;
            rb.velocity = transform.forward * VMove * speed;
            rb.velocity += transform.right * speed * HMove;
            rb.velocity += transform.up * VerticalVelocity;
        }
        //Прыжок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(MovementStates.Fall);
            rb.velocity += transform.up * JumpForce;
            print("Jump");
            ReGenJumps();
        }
        #endregion
        //Бег По стенам
        #region WallWalk

        if (PlayerState != MovementStates.WallRun)
        {
            WRLineData.Clear();
        }
        else
        {
            if (WRLineData.Count <= 2)
            {
                print("Fall");
                ChangeState(MovementStates.Fall);
                rb.AddForce(6 * Camera.main.transform.forward);

            }
            else
            {
                IBetweenPoints += Time.deltaTime * WRSpeed;
                Vector3 WRMoveDiraction = (WRLineData[1] - transform.position).normalized;

                transform.position = Vector3.Lerp(WRLineData[0], WRLineData[1], IBetweenPoints);
                if (IBetweenPoints >= 1)
                {
                    IBetweenPoints = 0;
                    WRLineData.RemoveAt(0);
                }
            }
        }
        #endregion
        
    }

    //Переработать для смены состояниея 
    public void ChangeState(MovementStates state)
    {
        MovementStates LastState = PlayerState;
        PlayerState = state;
        switch (state)
        {
            case MovementStates.WallRun:
                WRLineData.Insert(0,transform.position);
                ReGenJumps();
                break;
            case MovementStates.Fall:                 
                Transform RenderLinesParent = GameObject.Find("WRLines").transform;
                rb.velocity -= new Vector3(0, rb.velocity.y, 0);
                for (int i = 0; i < RenderLinesParent.childCount; i++)
                {
                    Destroy(RenderLinesParent.GetChild(i).gameObject);
                }
                
                WRLineData.Clear();
                break;

            default:
                Debug.LogWarning("This state does not exist");
                break;
        }
    }
    public void ReGenJumps()
    {
        JumpsCount = MaxJumps;
    }
    float lastFireTime = 0;
    [SerializeField] GameObject pref;
}
