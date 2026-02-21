using UnityEngine;

public class PieceRook : GamePiece
{
    public override bool CanMove(int toX, int toY)
    {
        int dx = Mathf.Abs(toX - X);
        int dy = Mathf.Abs(toY - Y);
        // 飛：縦横どこまでも
        return (dx == 0 || dy == 0);
    }
}
