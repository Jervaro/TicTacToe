using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] 
public class Player {
    public Image panel;
    public Text text;
    public Button button;
    public Sprite playerImage;
    public UnityEngine.Rendering.Universal.Light2D light2D;

    public Animator animator;

}

[System.Serializable] 
public class PlayerColor {
    public Color panelColor;

}

public class GameController : MonoBehaviour {
    public GridSpace[] gridSpaceList;
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
        for(int i = 0; i < gridSpaceList.Length; i++){
            gridSpaceList[i].SetGameControllReference(this);
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
        if(playerSide == "Capricorn"){
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

    public Sprite GetPlayerSideImage(){
        if (playerSide == "Capricorn") {
            return playerX.playerImage;
        } else {
            return playerO.playerImage;
        }
    }

    public void EndTurn(){
        moveCount++;

        // Checks the win for the first row
        if(gridSpaceList [0].text == playerSide && gridSpaceList [1].text == playerSide && gridSpaceList [2].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the second row
        else if(gridSpaceList [3].text == playerSide && gridSpaceList [4].text == playerSide && gridSpaceList [5].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the third row
        else if(gridSpaceList [6].text == playerSide && gridSpaceList [7].text == playerSide && gridSpaceList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the first column
        else if(gridSpaceList [0].text == playerSide && gridSpaceList [3].text == playerSide && gridSpaceList [6].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the second column
        else if(gridSpaceList [1].text == playerSide && gridSpaceList [4].text == playerSide && gridSpaceList [7].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the third column
        else if(gridSpaceList [2].text == playerSide && gridSpaceList [5].text == playerSide && gridSpaceList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the diagonal, left top to right bottom
        else if(gridSpaceList [0].text == playerSide && gridSpaceList [4].text == playerSide && gridSpaceList [8].text == playerSide){
            GameOver(playerSide);
        }
        // Checks the win for the diagonal, right top to left bottom
        else if(gridSpaceList [2].text == playerSide && gridSpaceList [4].text == playerSide && gridSpaceList [6].text == playerSide){
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
        playerX.animator.enabled = false;
        playerO.animator.enabled = false;
        startInfo.SetActive(false);
    }

    void GameOver(string winningPlayer){
        if(winningPlayer == "draw"){
            SetGameOverText("It´s a Draw!");
            SetPlayerColorsInactive();
        }
        else{
            SetGameOverText(winningPlayer + " Wins!");
            if(winningPlayer == "Capricorn"){
                SetPlayerColors(playerX, playerO);
            }
            else{
                SetPlayerColors(playerO, playerX);
            }
            
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
        SetPlayerColorsActive();
        playerX.animator.enabled = true;
        playerO.animator.enabled = true;
        
        for(int i = 0; i < gridSpaceList.Length; i++){
            gridSpaceList[i].ResetGridSpace();
        }
    }

    void ChangeSides(){
        playerSide = (playerSide == "Capricorn") ? "Libra" : "Capricorn";
        if(playerSide == "Capricorn"){
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
        //newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        //oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerColorsActive(){
        playerX.panel.color = activePlayerColor.panelColor; 

        playerO.panel.color = activePlayerColor.panelColor;
    }

    void SetPlayerColorsInactive(){
        playerX.panel.color = inactivePlayerColor.panelColor; 
        //playerX.text.color = inactivePlayerColor.textColor;

        playerO.panel.color = inactivePlayerColor.panelColor;
        //playerO.text.color = inactivePlayerColor.textColor;
    }

    void SetGameOverText(string value){
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetBoardInteractable(bool toggle){
        for(int i = 0; i < gridSpaceList.Length; i++){
            gridSpaceList[i].button.interactable = toggle;
        }
    }

    
}
