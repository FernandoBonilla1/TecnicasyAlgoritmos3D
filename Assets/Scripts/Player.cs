using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class Player : MonoBehaviour
{
    #region CLASS_VARIABLES
    [Header("References")]
    [SerializeField] private Camera _playerCamera;

    [Header("General")]
    [SerializeField] private float _gravityScale = -100f;

    [Header("Movent")]
    [Tooltip("Varaible de velocidad de movimiento")]
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _run = 50f;


    [Header("Rotation")]
    [Tooltip("Sensibilidad del mouse al rotar la camara")]
    [SerializeField] private float _rotationSensibility = 150f;

    [Header("Jump")]
    [Tooltip("Variable para detectar cuando estoy en el suelo")]
    [SerializeField] private bool _isJumping = false;
    [Tooltip("Variable para usar fuerza en el salto")]
    [SerializeField] private float _jumpForce = 50f;

    [Header("Others")]
    [Tooltip("Variable puntaje")]
    [SerializeField] private int points = 0;

    [Header("Audios")]
    [Tooltip("Variable que almacena el sonido de salto")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Weapon")]
    [SerializeField] private SimpleShoot _weapon;

    //Variable que hace referencia al Rigidbody
    private Rigidbody rb;
    //Variable que hace referencia al AudioSource
    private AudioSource audioSource;

    //Variable de referencia al characterController
    private CharacterController _characterController;
    //Variable que almacenará el vector de los inputs
    private Vector3 _moveInput = Vector3.zero;
    private Vector3 _rotationInput = Vector3.zero;

    private float _cameraVerticalAngle = 0f;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Le asigno la referencia a la variable rb
        rb = GetComponent<Rigidbody>();
        //Le asigno la referencia a la variable audiosource
        audioSource = GetComponent<AudioSource>();

        //Le asignamos la referencia a la variable charactercontroller
        _characterController = GetComponent<CharacterController>();

        //Anclamos el cursor en el videojuego
        Cursor.lockState = CursorLockMode.Locked;

        //Busco en la jerarquia algun elmento que tenga el simpleshot
        _weapon = FindObjectOfType<SimpleShoot>();

    }

    // Update is called once per frame
    void Update()
    {

        movement();
        jump();
        Look();
    }

    void movement()
    {
        if (_characterController.isGrounded)
        {
            //Asignar el input del InputManager
            _moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _moveInput = Vector3.ClampMagnitude(_moveInput, 1f);

            //Validamos si el personaje presiona la tecla de correr. Si no camina.
            if (Input.GetButton("Sprint"))
            {
                //Multiplicamos el valor por la variable correr
                _moveInput = transform.TransformDirection(_moveInput) * _run;
            }
            else
            {
                //Multiplicamos el valor por la variable caminar
                _moveInput = transform.TransformDirection(_moveInput) * _speed;
            }
        }

        _moveInput.y = _gravityScale * Time.deltaTime;
        _characterController.Move(_moveInput * Time.deltaTime);
    }

    void jump()
    {

        if (Input.GetButton("Jump"))
        {
            _moveInput.y = Mathf.Sqrt(_jumpForce * -2 * _gravityScale);
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //_isJumping = false;
        }
    }

    public void Look(){
        // Obtenemos las rotaciones segun el input del mouse
        _rotationInput.x = Input.GetAxis("Mouse X") * _rotationSensibility * Time.deltaTime;
        _rotationInput.y = Input.GetAxis("Mouse Y") * _rotationSensibility * Time.deltaTime;
        
        //Agregar la rotacion en el eje y de la camara
        _cameraVerticalAngle = _cameraVerticalAngle + _rotationInput.y;

        //Esta linea define un limite hasta donde se va a inclinar el angulo de la camara en el eje Y
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -70, 70);

        //Hacer la rotacion
        transform.Rotate(Vector3.up * _rotationInput.x);
        _playerCamera.transform.localRotation = Quaternion.Euler(-_cameraVerticalAngle, 0f, 0f);
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("bulletbox")){
            _weapon.SetBullets();
            Destroy(other.gameObject);
        }
    }




}
