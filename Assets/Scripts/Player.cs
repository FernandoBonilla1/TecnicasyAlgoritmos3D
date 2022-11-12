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
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    
        movimiento();

    }

    void movimiento(){

        //Se mueve arriba
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, speed);
        }
        //Se mueve abajo
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -speed);
        }
        //Se mueve a la derecha.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0f, 0f);
        }
        //Se mueve a la izquierda.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0f, 0f);
        }
    }







    
}
