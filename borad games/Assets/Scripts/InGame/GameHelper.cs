using UnityEngine;

public static class GameHelper
{
    const int GAME_LOCATION_WIDTH = 9;
    const int GAME_LOCATION_HEIGHT = 9;
    const int BLOCK_SIZE = 1;
    const float OFFSET_SIZE = 0.0f;
    const int GAME_FILED_WIDTH  = GAME_LOCATION_WIDTH * BLOCK_SIZE; // 戦闘フィールドの幅
    const int GAME_FILED_HEIGHT = GAME_LOCATION_HEIGHT * BLOCK_SIZE; // 戦闘フィールドの奥行き

    public static int CalcPanelNum(int x, int y)
    {
        if(x < 0 || x >= GAME_LOCATION_WIDTH || y < 0 || y >= GAME_LOCATION_HEIGHT)
        {
            return -1; // 無効な位置
        }
        int Num = x + (y * GAME_LOCATION_WIDTH);
        if(Num >= GAME_LOCATION_WIDTH * GAME_LOCATION_HEIGHT)
        {
            return -1; // 無効な位置
        }
        return Num;
    }

    public static Vector2 CalcPanelLocation(int x, int y)
    {
        Vector2 Location;
        Location.x  = y * BLOCK_SIZE + OFFSET_SIZE - GAME_FILED_WIDTH * 0.5f + BLOCK_SIZE * 0.5f;
        Location.y  = x * BLOCK_SIZE + OFFSET_SIZE - GAME_FILED_HEIGHT * 0.5f + BLOCK_SIZE * 0.5f;
        return Location;
    }

    // 添字から座標を取得
    public static void CalcPanelPosition(out int X, out int Y, int Num)
    {
        X = Num % GAME_LOCATION_WIDTH;
        Y = Num / GAME_LOCATION_WIDTH;
    }
    
    public static bool CanMove(PieceType type, PlayerType player, int fromX, int fromY, int toX, int toY)
    {
        int dx = toX - fromX; // 縦の移動距離
        int dy = toY - fromY; // 横の移動距離

        // そもそも移動していない、または盤面外（CalcPanelNumで弾くので基本大丈夫）
        if (dx == 0 && dy == 0) return false;

        // プレイヤー1（手前）は -x 方向が「前」
        // プレイヤー2（奥）は +x 方向が「前」
        // 計算しやすいように、Player2の場合は反転させて考えます
        int forwardX = (player == PlayerType.Player1) ? -dx : dx;
        int forwardY = (player == PlayerType.Player1) ? -dy : dy;

        switch (type)
        {
            case PieceType.Fu: // 歩：前1マス
                return (forwardX == -1 && forwardY == 0);

            case PieceType.Kyosha: // 香：前どこまでも（本来は間に駒がないか判定が必要ですが、まずは方向だけ）
                return (forwardX < 0 && forwardY == 0);

            case PieceType.Keima: // 桂：2つ前、左右1つ
                return (forwardX == -2 && Mathf.Abs(forwardY) == 1);

            case PieceType.Gin: // 銀：前3方向 ＋ 斜め後ろ2方向
                return (forwardX == -1 && Mathf.Abs(forwardY) <= 1) || (forwardX == 1 && Mathf.Abs(forwardY) == 1);

            case PieceType.Kin: // 金：前後左右4方向 ＋ 斜め前2方向
                return (Mathf.Abs(forwardX) <= 1 && Mathf.Abs(forwardY) <= 1) && !(forwardX == 1 && Mathf.Abs(forwardY) == 1);

            case PieceType.Gyoku: // 玉：全方向1マス
                return (Mathf.Abs(dx) <= 1 && Mathf.Abs(dy) <= 1);

            case PieceType.Hisha: // 飛：縦横どこまでも
                return (dx == 0 || dy == 0);

            case PieceType.Kaku: // 角：斜めどこまでも
                return (Mathf.Abs(dx) == Mathf.Abs(dy));

            default:
                return false;
        }
    }

    public static bool IsPathBlocked(GamePiece[] boardData, int fromX, int fromY, int toX, int toY)
    {
        int dx = toX - fromX;
        int dy = toY - fromY;

        // 1マスの移動なら、途中のマスは存在しないのでブロックされない
        if (Mathf.Abs(dx) <= 1 && Mathf.Abs(dy) <= 1) return false;

        // 進む方向を特定（1, 0, -1 のいずれかになる）
        int stepX = System.Math.Sign(dx);
        int stepY = System.Math.Sign(dy);

        int checkX = fromX + stepX;
        int checkY = fromY + stepY;

        // 移動先に到達する直前まで1マスずつ確認
        while (checkX != toX || checkY != toY)
        {
            int index = CalcPanelNum(checkX, checkY);
            if (index != -1 && boardData[index] != null)
            {
                return true; // 途中に駒があった！
            }

            checkX += stepX;
            checkY += stepY;
        }

        return false; // 途中に駒はなかった
    }
}
