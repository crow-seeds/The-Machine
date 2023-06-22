using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tictactoeNode : normalNode
{
    string[,] board = new string[3,3];
    [SerializeField] List<TextMeshProUGUI> boardObjects = new List<TextMeshProUGUI>();
    [SerializeField] timerNode timerThing;
    bool XorO = false; //false is x, true is o

    // Start is called before the first frame update
    void Start()
    {
        randomBoard();
        displayBoard();
    }

    void randomBoard()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                int k = UnityEngine.Random.Range(0, 3);
                switch (k)
                {
                    case 0:
                        board[i, j] = "-";
                        break;
                    case 1:
                        board[i, j] = "O";
                        break;
                    case 2:
                        board[i, j] = "X";
                        break;
                }
            }
        }
    }

    void displayBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardObjects[i * 3 + j].text = board[i, j];
            }
        }
    }

    bool doesWin(string side)
    {
        bool win = false;
        for(int i = 0; i < 3; i++)
        {
            if(board[0,i] == side && board[1, i] == side && board[2, i] == side)
            {
                win = true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == side && board[i, 1] == side && board[i, 2] == side)
            {
                win = true;
            }
        }

        if(board[0,0] == side && board[1, 1] == side && board[2, 2] == side)
        {
            win = true;
        }

        if (board[2, 0] == side && board[1, 1] == side && board[0, 2] == side)
        {
            win = true;
        }

        return win;
    }





    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            XorO = (int)(timerThing.getTime() / 45) % 2 == 0; //if even amount of 45s, then it is an O, else it is an X
            if(((!XorO && (!doesWin("X") || doesWin("O"))) || (XorO && (!doesWin("O") || doesWin("X"))))) //wrong team wins
            {
                if (!isFaulty)
                {
                    StartCoroutine(system.sendErrorReport("A", IDchar, 0));
                    isFaulty = true;
                }
            }
            else
            {
                if (isFaulty)
                {
                    system.clearErrorReport("A", IDchar);
                    isFaulty = false;
                }
            }
        }
    }

    public void activate(int pos)
    {
        int col = pos % 3;
        int row = pos / 3;

        if (isOn)
        {
            if (board[row, col] == "-")
            {
                board[row, col] = "O";
            }
            else if (board[row, col] == "O")
            {
                board[row, col] = "X";
            }
            else
            {
                board[row, col] = "-";
            }
            displayBoard();
        }
    }

    IEnumerator randomizer()
    {
        yield return new WaitForSeconds(45);
        randomBoard();
        displayBoard();
        StartCoroutine(randomizer());
    }

    public override void turnOn()
    {
        Debug.Log("pee");
        StartCoroutine(randomizer());
        base.turnOn();
    }


}
