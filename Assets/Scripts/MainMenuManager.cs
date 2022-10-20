using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Agregar una libreria que me permita manejar los elementos de las escenas.
using UnityEngine.SceneManagement;
public class MainMenuManager:MonoBehaviour
{
    //Metodo que te direcciona a la escena con el nombre dado por parametro
    void GoToScene(string nameScene){
        //Direcionar a la escena con el nombre que le pasemos
        SceneManager.LoadScene(nameScene);
    }

    //Metodo que cerrara la aplicacion
    void QuitProject(){
        //Cierra la app
        Application.Quit();
    }
}
