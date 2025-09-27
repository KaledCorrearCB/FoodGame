
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementPad : MonoBehaviour
{
    public static PlayerMovementPad instance;
    // Variable para almacenar el Character Controller
    private CharacterController cc;
    // Variable para almacenar el Input System
    private PlayerInput playerInput;

    // Velocidad de movimiento
    public float speed;

    // Velocidad de rotaci�n
    public float rotationSpeed;
    
    // Variables en donde se almacenan los vectores
    private Vector2 inputM;
    private float inputR;
    private Vector3 inputVector;
    private Vector3 movementVector;

    // Variable de gravedad para equilibrar la velocidad en el eje y
    private float characterGravity = -9.82f;

    // Guarda la posici�n de la rotaci�n actual.
    private float currentlookingPos;

    // Almacena el input del jugador usado para la rotaci�n.
    private float rotationInput;

    public Animator animator;

    private bool isWalking;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Se guardan en las variables referencias de los componentes
        playerInput = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        RotatePlayer();
        CamAnim();

        animator.SetBool("isWalking",isWalking);    
    }

    public void GetInput()
    {
        // Se guarda la info del Input System de movimiento
        inputM = playerInput.actions["Move"].ReadValue<Vector2>();

        inputR = playerInput.actions["Look"].ReadValue<float>();

        // Se obtiene un vector 3 con la entrada de movimiento
        inputVector = new Vector3(inputM.x, 0,inputM.y);

         // TransformDirection cambia el movimiento local al global
        inputVector = transform.TransformDirection(inputVector);

        // Se obtiene un vector 3 final con la velocidad y con la adecuaci�n de la gravedad
        movementVector = (inputVector * speed) + (Vector3.up * characterGravity);

        // Se le asigna el vector de rotaci�n y la velocidad de rotaci�n.
        rotationInput = inputR * rotationSpeed * Time.deltaTime;

    }
    // Se efect�a el movimiento del jugador con el m�todo Moce del Character Controller y se multiplica por el tiempo entre frames 
    // para que corra sin importar el frame rate
    public void Movement()
    {
        cc.Move(movementVector * Time.deltaTime);
       
    }

    // Permite que el jugador gire con las teclas.
    void RotatePlayer()
    {
        // Se suma a la variable actual la nueva posici�n
        currentlookingPos += rotationInput;

        // Se realiza la rotaci�n en el eje y con el currentLookingPos como �ngulo
        transform.localRotation = Quaternion.AngleAxis(currentlookingPos, transform.up);
    }

    public void CamAnim()
    {
        if (Mathf.Abs(inputM.x) > 0.1 || Mathf.Abs(inputM.y) > 0.1)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

 }
