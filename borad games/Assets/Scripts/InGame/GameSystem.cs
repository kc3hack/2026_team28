using UnityEngine;
using System.Collections.Generic;

// ターン管理やゲームの準備を行う
public class GameSystem : MonoBehaviour
{
    private GameState currentState;
    private GameBoard gameBoard;
    private GameCursor gameCursor;
    private GamePiece selectedPiece; // 現在選択されている駒
    [SerializeField] private GameController gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TODO: ゲームの準備
        currentState = GameState.Preparation;

        gameBoard = Object.FindObjectsByType<GameBoard>(FindObjectsSortMode.None)[0];
        
        gameCursor = Object.FindObjectsByType<GameCursor>(FindObjectsSortMode.None)[0];
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
                    HandlePieceSelection();
                }
                break;
            case GameState.Player2Turn:
                // プレイヤー2のターンの処理
                // ターン終了条件を満たしたらプレイヤー1のターンに移行
                // カーソルを移動させて駒を選択
                // 駒を移動完了フラグが立ったらターン終了
                if(gameController.IsOkTrigger())
                {
                    HandlePieceSelection();
                }
                break;
            case GameState.GameOver:
                // ゲームオーバーの処理
                NextState();
                break;
        }
    }

    void HandlePieceSelection()
    {
        int x = gameCursor.X;
        int y = gameCursor.Y;

        if (selectedPiece == null)
        {
            // --- 駒を選択するフェーズ ---
            GamePiece piece = gameBoard.GetPieceAt(x, y);

            // 自分の駒なら選択
            if (piece != null && IsMyPiece(piece))
            {
                selectedPiece = piece;
                gameBoard.ActivePanel(GameHelper.CalcPanelNum(x, y)); // 選択した足元を光らせる
                Debug.Log($"{piece.type}を選択しました。移動先を選んでください。");
            }
        }
        else
        {
            // --- 駒を移動させるフェーズ ---
            bool canMove = GameHelper.CanMove(
                selectedPiece.type, 
                selectedPiece.player, 
                selectedPiece.X, selectedPiece.Y, 
                x, y
            );
            if (!canMove)
            {
                Debug.Log("そこには動けません！");
                return; // 何もせず入力を待つ（選択は解除しない）
            }
            GamePiece targetPiece = gameBoard.GetPieceAt(x, y);
            if (targetPiece != null && targetPiece.player == selectedPiece.player)
            {
                Debug.Log("自分の駒がある場所には行けません！");
                return;
            }
            if (selectedPiece.type != PieceType.Keima) // 桂馬以外はチェックする
            {
                if (GameHelper.IsPathBlocked(gameBoard.GetBoardData(), selectedPiece.X, selectedPiece.Y, x, y))
                {
                    Debug.Log("途中に駒があるので飛び越えられません！");
                    return;
                }
            }
            gameBoard.UpdateBoardData(selectedPiece.X, selectedPiece.Y, x, y);

            selectedPiece.X = x;
            selectedPiece.Y = y;
            selectedPiece.MoveTo(GameHelper.CalcPanelLocation(x, y));

            selectedPiece = null;
            gameBoard.AllDeactivePanel();
            NextState(); // ターン終了
        }
    }

    bool IsMyPiece(GamePiece piece)
    {
        if (currentState == GameState.Player1Turn && piece.player == PlayerType.Player1) return true;
        if (currentState == GameState.Player2Turn && piece.player == PlayerType.Player2) return true;
        return false;
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
