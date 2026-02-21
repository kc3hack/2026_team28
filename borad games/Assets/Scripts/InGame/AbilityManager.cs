using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボタンが押された時に実行されるメソッド
    public virtual void OnButtonClick()
    {
        Debug.Log("Abilityボタンが押されました！");
    }
}
