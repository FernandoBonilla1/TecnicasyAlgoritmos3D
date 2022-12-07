using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;


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
    [SerializeField] private int _points = 0;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private float _lastCall = 1.0f;
    [SerializeField] private int _lastCallValue = 0;

    [Header("Audios")]
    [Tooltip("Variable que almacena el sonido de salto")]
    [SerializeField] private AudioClip _jumpSound;
    [Tooltip("Variable que almacena el sonido de la campana")]
    [SerializeField] private AudioClip _bellSound;
    [SerializeField] private AudioClip _minigameMusic;
    [SerializeField] private AudioSource _bgAudioSource;

    [Header("Weapon")]
    [SerializeField] private SimpleShoot _weapon;

    [Header("Minigame")]
    [SerializeField] private GameObject _feedbackCanvas;
    private bool _canStart = false;

    //Variable que hace referencia al Rigidbody
    private Rigidbody _rb;
    //Variable que hace referencia al AudioSource
    private AudioSource _audioSource;

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
        _rb = GetComponent<Rigidbody>();
        //Le asigno la referencia a la variable audiosource
        _audioSource = GetComponent<AudioSource>();

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

        Movement();
        Look();
        StartMinigame();

        _pointsText.text = "Puntos: " + _points;
    }

    void Movement()
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
            Jump();
        }

        _moveInput.y += _gravityScale * Time.deltaTime;
        _characterController.Move(_moveInput * Time.deltaTime);
    }

    void Jump()
    {

        if (Input.GetButton("Jump"))
        {
            _moveInput.y = Mathf.Sqrt(_jumpForce * -2 * _gravityScale);
            _audioSource.clip = _jumpSound;
            _audioSource.Play();
        }
    }

    public void SetPoints(int value)
    {
        //se calcula el tiempo desde la ultima llamada para evitar llamadas muy rápidas causadas por colisiones multiples de la bala (es decir, colisiones en distintos niveles del target)
        //toma sólo el primer valor con el cual colisionó. Tiempo mínimo entre colisiones es mayor a 500ms
        float callInstant = Time.time;
        if(callInstant - _lastCall > 0.5f)
        {
            _lastCall = callInstant;
            _lastCallValue = value;
            _points = _points + value;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //_isJumping = false;
        }
    }

    public void StartMinigame()
    {
        if (_canStart && Input.GetKeyDown(KeyCode.E))
        {
            _audioSource.clip = _bellSound;
            _audioSource.Play();
            _bgAudioSource.clip = _minigameMusic;
            _bgAudioSource.Play();
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

        if (other.gameObject.CompareTag("bell"))
        {
            _feedbackCanvas.SetActive(true);
            _canStart = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("bell"))
        {
            _feedbackCanvas.SetActive(false);
            _canStart = false;
        }
    }




}
