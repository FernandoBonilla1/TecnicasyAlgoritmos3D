using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{

    [SerializeField] private int points = 0;


    private void Start(){
        LoadData();
    }
    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            SaveData();
        }
    }

    
    public void AddPoints(){
        points++;
    }

    public void SaveData(){
        //guardo un int
        PlayerPrefs.SetInt("Puntos", points);
        Debug.Log("Guardando datos");
    }

    public void LoadData(){
        //Cargar datos
        points = PlayerPrefs.GetInt("Puntos");
    }

}
