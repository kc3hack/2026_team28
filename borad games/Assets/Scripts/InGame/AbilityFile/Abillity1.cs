using UnityEngine;

public class Abillity : AbilityManager
{

    // ボタンが押された時に実行されるメソッド
    public override void OnButtonClick()
    {
        Debug.Log("player1のボタンが正しく押されました！");
    }
}
