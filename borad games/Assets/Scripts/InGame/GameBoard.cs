using UnityEngine;
using System.Collections.Generic;

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
        List<GamePiece> layout = CreateLayout();
        foreach (GamePiece item in layout)
        {
            // インデックス計算
            int index = GameHelper.CalcPanelNum(item.X, item.Y);
            
            // 駒を生成
            GameObject obj = Instantiate(piecePrefab, transform); // 自分の子として生成
            GamePiece piece = obj.GetComponent<GamePiece>();
            
            // データをセット
            piece.type = item.type;
            piece.player = item.player;
            
            // 位置を反映
            piece.transform.localPosition = GameHelper.CalcPanelLocation(item.X, item.Y);
            
            // Player2なら180度回転させる
            if (piece.player == PlayerType.Player2)
            {
                piece.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }

            // 配列で管理
            boardData[index] = piece;
        }
    }
    public List<GamePiece> CreateLayout()
    {
        List<GamePiece> layout = new List<GamePiece>();
        // 例：歩 (Fu) を並べる
        for (int i = 0; i < 9; i++)
        {
            layout.Add(new GamePiece { X = 2, Y = i, type = PieceType.Fu, player = PlayerType.Player1 });
            layout.Add(new GamePiece { X = 6, Y = i, type = PieceType.Fu, player = PlayerType.Player2 });
        }

        // 玉 (Gyoku)
        layout.Add(new GamePiece { X = 0, Y = 4, type = PieceType.Gyoku, player = PlayerType.Player1 });
        layout.Add(new GamePiece { X = 8, Y = 4, type = PieceType.Gyoku, player = PlayerType.Player2 });

        // 他の駒（飛車、角、金、銀...）も同様に Add していきます
        return layout;
    }
}
