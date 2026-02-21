using UnityEngine;

public class PieceBishop : GamePiece
{
    public override bool CanMove(int toX, int toY)
    {
        int dx = Mathf.Abs(toX - X);
        int dy = Mathf.Abs(toY - Y);
        // 角：斜めどこまでも
        return (Mathf.Abs(dx) == Mathf.Abs(dy));
    }
}