using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] 
public class Player {
    public Image panel;
    public Text text;
    public Button button;

}

[System.Serializable] 
public class PlayerColor {
    public Color panelColor;
    public Color textColor;

}

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public GameObject startInfo;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string playerSide;
    private int moveCount;

    void SetGameControllReferenceOnButtons(){
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllReference(this);
        }
    }

    void Awake() {
        moveCount = 0;

        SetGameControllReferenceOnButtons();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    public void SetStartingSide(string startingSide){
        playerSide = startingSide;
        if(playerSide == "X"){
            SetPlayerColors(playerX, playerO);
        }
        else{
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    public string GetPlayerSide(){
        return playerSide;
    }

    public void EndTurn(){
        moveCount++;

        // Checks the win for the first row
        if(buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the second row
        else if(buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the third row
        else if(buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the first column
        else if(buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the second column
        else if(buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the third column
        else if(buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the diagonal, left top to right bottom
        else if(buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the diagonal, right top to left bottom
        else if(buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide){
            GameOver(playerSide);
        }
        
        else if(moveCount >= 9){
            GameOver("draw");
        }
        else ChangeSides();
    }

    void StartGame(){
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    void GameOver(string winningPlayer){
        if(winningPlayer == "draw"){
            SetGameOverText("It´s a Draw!");
            SetPlayerColorsInactive();
        }
        else{
            SetGameOverText(winningPlayer + " Wins!");
        }
        SetBoardInteractable(false);
        restartButton.SetActive(true);
    }

    public void RestartGame(){
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].text = "";
        }
    }

    void ChangeSides(){
        playerSide = (playerSide == "X") ? "O" : "X";
        if(playerSide == "X"){
            SetPlayerColors(playerX, playerO);
        }
        else{
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerButtons(bool toggle){
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColors (Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerColorsInactive(){
        playerX.panel.color = inactivePlayerColor.panelColor; 
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    void SetGameOverText(string value){
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetBoardInteractable(bool toggle){
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    
}
