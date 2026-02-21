using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    // ボタンが押された時に実行されるメソッド
    public virtual void OnButtonClick()
    {
        Debug.Log("Abilityボタンが押されました！");
    }
}
