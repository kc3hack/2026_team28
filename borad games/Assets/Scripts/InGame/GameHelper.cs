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
    

}
