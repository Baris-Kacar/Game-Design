using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
   
    public GameObject gameOverUI;
    public static GameManagerScript instance;
    public TMP_Text coinText;
    public int currentCoins = 0;

    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "SCORE: " + currentCoins.ToString();
        // Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void IncreaseCoins(int v){
        currentCoins += v;
        coinText.text = "SCORE: " + currentCoins.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
           // Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    
   
}
