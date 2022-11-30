using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public GameObject WinCirle;
    public GameObject player1;
    public GameObject player2;

    //public GameObject player1Ghost;
    //public GameObject player2Ghost;

    public int heightOfBoard = 6;
    public int LenghtOfBoard = 7;

    public GameObject[] spawnLocation;
    bool player1Turn = true;

    int[,] boardState;

    public TextMeshProUGUI player1W;
    public TextMeshProUGUI player2W;
    public TextMeshProUGUI draw;

    void Start()
    {
        Debug.Log("debug message");
        boardState = new int[LenghtOfBoard, heightOfBoard];

        player1W.gameObject.SetActive(false);
        player2W.gameObject.SetActive(false);
        draw.gameObject.SetActive(false);
        //player1Ghost.SetActive(false);
        ///player2Ghost.SetActive(false);
    }
    
    //public void HoverColumn(int column)
    //{
        //if(player1Turn)
        //{
            //player1Ghost.SetActive(true);
            //player1Ghost.transform.position = spawnLocation[column].transform.position;
        //}
        //else
        //{
           // player2Ghost.SetActive(true);
            //player2Ghost.transform.position = spawnLocation[column].transform.position;
        //}
    //}



    public void SelectColumn(int column)
    {
        Debug.Log("GameManager Column" + column);
        TakeTurn(column);
    }

    void TakeTurn(int column)
    {
        if (UpdateBoardState(column))
        {
            // player1Ghost.SetActive(false);
            // player2Ghost.SetActive(false);
            if (player1Turn)
            {
                Instantiate(player1, spawnLocation[column].transform.position, Quaternion.Euler(0, 121, 0));
                player1Turn = false;

                if (DidWin(1))
                {
                    Debug.LogWarning("Player 1 won");
                    player1W.gameObject.SetActive(true);
                }
            }
            else
            {
                Instantiate(player2, spawnLocation[column].transform.position, Quaternion.Euler(0, 122, 0));
                player1Turn = true;
                if (DidWin(2))
                {
                    Debug.LogWarning("Player 2 won");
                    player2W.gameObject.SetActive(true);
                }
            }
            if(DidDraw())
            {
                Debug.LogWarning("Draw");
                draw.gameObject.SetActive(true);
            }
        }
    }
    bool UpdateBoardState(int column)
    {
        for(int row = 0; row < heightOfBoard; row++)
        {
            if (boardState[column,row] == 0)
            {
                if (player1Turn)
                {
                    boardState[column, row] = 1;
                }
                else
                {
                    boardState[column, row] = 2;
                }
                Debug.Log("piece being spawned at" + column + "," + row);
                return true;
            }
        }
        return false;
    }

    bool DidWin(int playerNum)
    {
        //Horizontal
        for (int x = 0; x < LenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard; y++)
            {
                if (boardState[x, y] == playerNum &&
                    boardState[x + 1, y] == playerNum &&
                    boardState[x + 2, y] == playerNum &&
                    boardState[x + 3, y] == playerNum)
                {
                    Instantiate(WinCirle, new Vector3(x, y, 0), Quaternion.identity);
                    Instantiate(WinCirle, new Vector3(x+1, y, 0), Quaternion.identity);
                    Instantiate(WinCirle, new Vector3(x+2, y, 0), Quaternion.identity);
                    Instantiate(WinCirle, new Vector3(x+3, y, 0), Quaternion.identity);
                    return true;
                }
            }
        }
        // vertical 
        for (int x = 0; x < LenghtOfBoard; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y] == playerNum &&
                    boardState[x, y + 1] == playerNum &&
                    boardState[x , y + 2] == playerNum &&
                    boardState[x , y + 3] == playerNum)
                {
                    return true;
                }
            }
        }
        // y = xx
        for (int x = 0; x < LenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y] == playerNum &&
                    boardState[x + 1, y + 1] == playerNum &&
                    boardState[x + 2, y + 2] == playerNum &&
                    boardState[x + 3, y + 3] == playerNum)
                {
                    return true;
                }
            }
        }    


        //y = -x
        for (int x = 0; x < LenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y + 3] == playerNum &&
                    boardState[x + 1, y + 2] == playerNum &&
                    boardState[x + 2, y + 1] == playerNum &&
                    boardState[x + 3, y] == playerNum)
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool DidDraw()
    {
        for (int x = 0; x < LenghtOfBoard; x++)
        {
            if (boardState[x, heightOfBoard - 1] == 0)
            {
                return false;
            }
        }
        return true;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quitting Game!");
    }

    public void replay()
    {
        sceneManager(0);
    }
    public void sceneManager(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
