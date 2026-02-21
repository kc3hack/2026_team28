using UnityEngine;
using System.Collections.Generic;
public class PieceSpawnData
{
    public int X;
    public int Y;
    public PieceType type;
    public PlayerType player;
}
public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    private List<GameSelectPanel> selectPanelArray = new List<GameSelectPanel>();

    private GamePiece[] boardData = new GamePiece[81]; // 9x9の盤面を想定

    void Start()
    {
        GameSelectPanel[] foundPanels = GetComponentsInChildren<GameSelectPanel>(true);
        selectPanelArray.AddRange(foundPanels);
        Debug.Log($"{selectPanelArray.Count}");

        for(int i = 0; i < selectPanelArray.Count; i++)
        {
            var panel = selectPanelArray[i];
            int x, y;
            GameHelper.CalcPanelPosition(out x, out y, i);

            Vector2 location = GameHelper.CalcPanelLocation(x, y);
            panel.transform.localPosition = location;
            Debug.Log($"{i}: {selectPanelArray[i].name}");
        }
        AllDeactivePanel();
        SetupInitialPieces();
    }

    public void ActivePanel(int num)
    {
        if(num < 0 || num >= selectPanelArray.Count)
        {
            Debug.LogError($"Invalid panel number: {num}");
            return;
        }
        selectPanelArray[num].gameObject.SetActive(true);
    }

    public void AllDeactivePanel()
    {
        foreach(var panel in selectPanelArray)
        {
            panel.gameObject.SetActive(false);
        }
    }
    // 指定座標に駒があるか確認する関数
    public bool HasPieceAt(int x, int y)
    {
        int index = GameHelper.CalcPanelNum(x, y);
        return (index != -1 && boardData[index] != null);
    }

    void SetupInitialPieces()
    {
        List<PieceSpawnData> layout = CreateLayout();
        foreach (PieceSpawnData item in layout)
        {
            int index = GameHelper.CalcPanelNum(item.X, item.Y);
            
            // ここで本物の駒（MonoBehaviour）を生成する
            GameObject obj = Instantiate(piecePrefab, transform);
            GamePiece piece = obj.GetComponent<GamePiece>();
            
            // データをセット
            piece.type = item.type;
            piece.player = item.player;
            piece.X = item.X; // 座標も忘れずにセット
            piece.Y = item.Y;
            
            piece.transform.localPosition = GameHelper.CalcPanelLocation(item.X, item.Y);
            
            if (piece.player == PlayerType.Player2)
            {
                piece.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }

            boardData[index] = piece;
        }
    }
    
    public GamePiece GetPieceAt(int x, int y)
    {
        int index = GameHelper.CalcPanelNum(x, y);
        if (index == -1) return null;
        return boardData[index];
    }

    public void UpdateBoardData(int fromX, int fromY, int toX, int toY)
    {
        int fromIndex = GameHelper.CalcPanelNum(fromX, fromY);
        int toIndex = GameHelper.CalcPanelNum(toX, toY);

        boardData[toIndex] = boardData[fromIndex];
        boardData[fromIndex] = null;
    }

    public GamePiece[] GetBoardData()
    {
        return boardData;
    }

    public List<PieceSpawnData> CreateLayout()
    {
        List<PieceSpawnData> layout = new List<PieceSpawnData>();
        // 例：歩 (Fu) を並べる
        for (int i = 0; i < 9; i++)
        {
            layout.Add(new PieceSpawnData { X = 2, Y = i, type = PieceType.Fu, player = PlayerType.Player1 });
            layout.Add(new PieceSpawnData { X = 6, Y = i, type = PieceType.Fu, player = PlayerType.Player2 });
        }

        // 玉 (Gyoku)
        layout.Add(new PieceSpawnData { X = 0, Y = 4, type = PieceType.Gyoku, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 8, Y = 4, type = PieceType.Gyoku, player = PlayerType.Player2 });

        // 他の駒（飛車、角、金、銀...）も同様に Add していきます
        layout.Add(new PieceSpawnData { X = 1, Y = 7, type = PieceType.Hisha, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 7, Y = 1, type = PieceType.Hisha, player = PlayerType.Player2 });
        layout.Add(new PieceSpawnData { X = 1, Y = 1, type = PieceType.Kaku, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 7, Y = 7, type = PieceType.Kaku, player = PlayerType.Player2 });

        layout.Add(new PieceSpawnData { X = 0, Y = 3, type = PieceType.Kin, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 0, Y = 5, type = PieceType.Kin, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 8, Y = 3, type = PieceType.Kin, player = PlayerType.Player2 });
        layout.Add(new PieceSpawnData { X = 8, Y = 5, type = PieceType.Kin, player = PlayerType.Player2 });

        layout.Add(new PieceSpawnData { X = 0, Y = 2, type = PieceType.Gin, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 0, Y = 6, type = PieceType.Gin, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 8, Y = 2, type = PieceType.Gin, player = PlayerType.Player2 });
        layout.Add(new PieceSpawnData { X = 8, Y = 6, type = PieceType.Gin, player = PlayerType.Player2 });

        layout.Add(new PieceSpawnData { X = 0, Y = 1, type = PieceType.Keima, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 0, Y = 7, type = PieceType.Keima, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 8, Y = 1, type = PieceType.Keima, player = PlayerType.Player2 });
        layout.Add(new PieceSpawnData { X = 8, Y = 7, type = PieceType.Keima, player = PlayerType.Player2 });

        layout.Add(new PieceSpawnData { X = 0, Y = 0, type = PieceType.Kyosha, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 0, Y = 8, type = PieceType.Kyosha, player = PlayerType.Player1 });
        layout.Add(new PieceSpawnData { X = 8, Y = 8, type = PieceType.Kyosha, player = PlayerType.Player2 });
        layout.Add(new PieceSpawnData { X = 8, Y = 0, type = PieceType.Kyosha, player = PlayerType.Player2 });
        return layout;
    }
}
