using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//Nombre: Fernando Nicolas Bonilla Rivera

public class Player : MonoBehaviour
{
    
     #region CLASS_VARIABLES
    //Variable velocidad del jugador
    [SerializeField]
    private float speed = 0.1f;
    
    //Variable que almacena las monedas obtenidas por el jugar en numeros enteros.
    public int coins = 0;

    //Variable nombre del jugador
    [SerializeField]
    private string name = "Player";
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Juego Iniciado, el protagonista está listo para sacarse un 70 en esta prueba");
    }

    // Update is called once per frame
    void Update()
    {
        
        restarMoneda();
        izquierda();

    }

    void restarMoneda(){
        //Restar moneda
        if (Input.GetKeyDown(KeyCode.Space)){
            coins--;
        }
    }
    void izquierda(){
        //Se mueve a la izquierda.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0f, 0f);
        }
    }

    //Detecta cuando se realiza una interseccion
    public void OnTriggerEnter(Collider collider){
        Debug.Log("Se realizo una interseccion");
        //Si colisiono con un gameobject con el tag "Moneda" pasa esto
        if (collider.gameObject.CompareTag("Moneda"))
        {
            coins++;
            Debug.Log("Usted intercepto con una moneda, así que le daré una moneda");
        }
    }





    
}
