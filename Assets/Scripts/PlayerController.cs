using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    
    [SerializeField]
    private float mouseSensibilityX = 3f;

    [SerializeField]
    private float mouseSensibilityY = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // calculer la vélocité (vitesse) du mouvement du joueur
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        motor.Move(velocity);

        // calcule rotation du joueur en Vector3
        float yRota = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRota, 0) * mouseSensibilityX;

        motor.Rotate(rotation);

        // calcule rotation de la caméra en Vector3
        float xRota = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRota, 0, 0) * mouseSensibilityY;

        motor.RotateCamera(cameraRotation);
    }

}