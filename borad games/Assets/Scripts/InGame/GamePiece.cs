using UnityEngine;

public enum PieceType { Gyoku, Hisha, Kaku, Kin, Gin, Keima, Kyosha, Fu }
public enum PlayerType { Player1, Player2 }
public class GamePiece : MonoBehaviour
{
    public PieceType type;
    public PlayerType player;
    public int X { get; set; }
    public int Y { get; set; }

    // 駒を移動させるメソッド
    public void MoveTo(Vector2 targetLocation)
    {
        transform.localPosition = targetLocation;
    }
}
