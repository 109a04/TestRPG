using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 10;//����t��
    public float RotateSpeed = 1f;//��ʳt��(�S��)

    public float Gravity = -9.8f;//���O�w�]
    private Vector3 Velocity = Vector3.zero;//�w�]��0���t�צV�q

    public float JumpHight = 3f;//���D����
    private bool IsGround;//�O�_�b�a��
    public Transform GroundCheck;
    public float CheckRadius = 0.2f;//�O�_�b�a���ˬd�����b�|
    public LayerMask layerMask;//�z��a���h

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
}

    // Update is called once per frame
    void Update()
    {
        PlayerMoveMethod();
    }

    private void PlayerMoveMethod()
    {
        //�O�_�b�a���ˬd
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)//Velocity.y�������t�סA�]�������t�׬��t���󨤦⥿�b���U
        {
            Velocity.y = 0;//����^��a����A���m�����t��
        }

        //���D
        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y = Mathf.Sqrt(JumpHight * -2 * Gravity);//�M�θ��D����
        }

        //w,a,s,d����J
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        //�۾����e�M���k����V
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        //�Q��w,a,s,d����J�M�۾����e�M���k����V�Ӻ�X�����V
        var moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        //���Ⲿ��
        controller.Move(moveDirection * speed * Time.deltaTime);

        //���O�A�������^��a��
        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, Velocity.y, 0f) * Time.deltaTime);


        //mouse vector - player vector ,can get the vector point mouse from player 
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var point = Input.mousePosition - playerScreenPoint;
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;//get the rotate angle

        //�������
        controller.transform.eulerAngles = new Vector3(controller.transform.eulerAngles.x, angle, controller.transform.eulerAngles.z);
    }

}