using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    
    [SerializeField]
    private float mouseSensibilityX = 3f;

    [SerializeField]
    private float mouseSensibilityY = 3f;

    [SerializeField]
    private float jetpackForce = 1000f;

    [Header("Joint Options")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 50f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        // calculer la vélocité (vitesse) du mouvement du joueur
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        Vector3 velocity = (moveHorizontal + moveVertical) * speed;

        // Jouer animations jetpack
        animator.SetFloat("ForwardVelocity", zMove);

        motor.Move(velocity);

        // calcule rotation du joueur en Vector3
        float yRota = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRota, 0) * mouseSensibilityX;

        motor.Rotate(rotation);

        // calcule rotation de la caméra en Vector3
        float xRota = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRota * mouseSensibilityY;

        motor.RotateCamera(cameraRotationX);

        // Calcule de la vélocité du jetpack
        Vector3 jetpackVelocity = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            jetpackVelocity = Vector3.up * jetpackForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        // appliquer la vélocité du jetpack
        motor.ApplyJetpack(jetpackVelocity);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
    }

}