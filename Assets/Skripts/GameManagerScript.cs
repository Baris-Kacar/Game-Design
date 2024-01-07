using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
   
    public GameObject gameOverUI;

    [SerializeField] public CoinText coinText;

    private int coinCount;

    private void OnEnable()
    {
        //coinCount.OnCoinCollected += HandleCoinPickup;
    }

    private void OnDisable()
    {
        //coinCount.CoinCollected -= HandleCoinPickup;
    }

    void HandleCoinPickup()
    {
        coinCount++;
        coinText.IncrementCoinCount(coinCount);
    }

    // Start is called before the first frame update
    void Start()
    {
       // Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
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
