using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;

    private string playerSide;

    void SetGameControllReferenceOnButtons(){
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllReference(this);
        }
    }

    void Awake() {
        playerSide = "X";
        SetGameControllReferenceOnButtons();
    }

    public string GetPlayerSide(){
        return playerSide;
    }

    public void EndTurn(){
        // Checks the win for the first row
        if(buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide){
            GameOver();
        }
        // Checks the win for the second row
        if(buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide){
            GameOver();
        }
        // Checks the win for the third row
        if(buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide){
            GameOver();
        }
        // Checks the win for the first column
        if(buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide){
            GameOver();
        }
        // Checks the win for the second column
        if(buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide){
            GameOver();
        }
        // Checks the win for the third column
        if(buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide){
            GameOver();
        }
        // Checks the win for the diagonal, left top to right bottom
        if(buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide){
            GameOver();
        }
        // Checks the win for the diagonal, right top to left bottom
        if(buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide){
            GameOver();
        }

        ChangeSides();
    }

    void GameOver(){
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }

    void ChangeSides(){
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
