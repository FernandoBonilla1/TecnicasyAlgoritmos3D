using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager:MonoBehaviour
{
    public void GoToScene(string nameScene){
        //Direcionar a la escena con el nombre que le pasemos
        SceneManager.LoadScene(nameScene);
    }

    //Metodo que cerrara la aplicacion
    public void QuitProject(){
        //Cierra la app
        Application.Quit();
    }
}






