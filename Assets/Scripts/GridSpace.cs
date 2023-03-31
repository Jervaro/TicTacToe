using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public string text;
    public Image image;

    private GameController gameController;

    public void SetGameControllReference(GameController controller){
        gameController = controller;
    }

    public void SetSpace(){
        text = gameController.GetPlayerSide(); 
        button.interactable = false;

        image.sprite = gameController.GetPlayerSideImage();
        image.color = Color.white;
        gameController.EndTurn();
    }

    public void ResetGridSpace(){
        text = "";
        image.color = Color.clear;
    }
    
}
