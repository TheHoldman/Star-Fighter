using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayUI : MonoBehaviour
{
    public Image healthBar1;
    public Image healthBar2;
    public Image healthBar3;
    public Image healthBar4;
    public Image healthBar5;
    public Image healthBar6;
    public Image healthBar7;
    public Image healthBar8;

    public Text scoreText;    

    public Camera mainCamera;

    List<Image> healthBars;

    void Start()
    {        
        Image[] bars = {healthBar1, healthBar2, healthBar3, healthBar4, healthBar5, healthBar6, healthBar7, healthBar8};
        healthBars = new List<Image>(bars);        
    }

    private void Update()
    {
        //Removes and adds health bars
        int i = 1;
        foreach (Image bar in healthBars)        
        {
            if (i> GameManagerScript.lives)
            {
                bar.enabled = false;
            }
            else if (i <= GameManagerScript.lives)
            {
                bar.enabled = true;
            }
            i += 1;
        }

        //displays current score
        scoreText.text = GameManagerScript.score.ToString();
    }    

}
