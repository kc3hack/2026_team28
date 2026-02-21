using UnityEngine;
using System.Collections.Generic;

// ターン管理やゲームの準備を行う
public class GameSystem : MonoBehaviour
{
    private GameState currentState;
    private GameBoard gameBoard;
    [SerializeField] private GameController gameController;
    int x = 0;
    int y = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TODO: ゲームの準備
        currentState = GameState.Preparation;

        gameBoard = Object.FindObjectsByType<GameBoard>(FindObjectsSortMode.None)[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: ターン管理
        switch(currentState)
        {
            case GameState.Preparation:
                DecideTurn();
                break;
            case GameState.Player1Turn:
                
                // プレイヤー1のターンの処理
                // ターン終了条件を満たしたらプレイヤー2のターンに移行
                // カーソルを移動させて駒を選択
                // 駒を移動完了フラグが立ったらターン終了
                if(gameController.IsOkTrigger())
                {
                    int num = GameHelper.CalcPanelNum(x, y);
                    gameBoard.ActivePanel(num); 
                    x++;
                    NextState();
                }
                break;
            case GameState.Player2Turn:
                // プレイヤー2のターンの処理
                // ターン終了条件を満たしたらプレイヤー1のターンに移行
                // カーソルを移動させて駒を選択
                // 駒を移動完了フラグが立ったらターン終了
                if(gameController.IsOkTrigger())
                {
                    int num = GameHelper.CalcPanelNum(x, y);
                    gameBoard.ActivePanel(num); 
                    y++;
                    NextState();
                }
                break;
            case GameState.GameOver:
                // ゲームオーバーの処理
                NextState();
                break;
        }
    }

    void NextState()
    {
        switch(currentState)
        {
            case GameState.Preparation:
                
                break;
            case GameState.Player1Turn:
                currentState = GameState.Player2Turn;
                Debug.Log("プレイヤー2にターンが移りました！");
                break;
            case GameState.Player2Turn:
                currentState = GameState.Player1Turn;
                Debug.Log("プレイヤー1にターンが移りました！");
                break;
            case GameState.GameOver:
                // ゲームオーバーの処理
                break;
        }
    }

    void DecideTurn()
    {
        // ランダムにプレイヤー1かプレイヤー2のどちらが先攻かを決定する処理
        if (Random.value < 0.5f)        {
            currentState = GameState.Player1Turn;
            Debug.Log("プレイヤー1のターンです！");
        } else {
            currentState = GameState.Player2Turn;
            Debug.Log("プレイヤー2のターンです！");
        }
    }
    
}

    

enum GameState
{
    Preparation, // ゲームの準備中
    Player1Turn, // プレイヤー1のターン
    Player2Turn,  // プレイヤー2のターン
    GameOver    // ゲームオーバー
}
