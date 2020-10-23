using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jouer()
    {
        SceneManager.LoadScene("SampleScene"); //permet de charger la scene "SampleScene"
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");    //permet de charger la scene "Credits"
    }
    
    public void Quitter()
    {
        Application.Quit();                    //permet de quitter l'application lorsque l'on appuie sur le bouton
        Debug.Log("Ferme le jeu");
    }
    
    public void Leave()
    {
        SceneManager.LoadScene("Menu");
    }

}
