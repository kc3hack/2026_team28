using UnityEngine;

public class PieceKing : GamePiece
{
    public override bool CanMove(int toX, int toY)
    {
        int dx = toX - X;
        int dy = toY - Y;
        // 全方向に1マス以内
        return (dx <= 1 && dy <= 1);
    }
}
