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
        AnimateScore(); // 値が変わった時にアニメーションを実行
    }

    private void AnimateScore()
    {
        // 1. スケール（大きさ）のアニメーション
        // 一瞬1.2倍に膨らんでから元のサイズ(1.0)に戻る「パンチ」演出
        scoreText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f);

        // 2. 色のアニメーション（例：一瞬黄色にしてから白に戻す）
        scoreText.DOColor(Color.yellow, 0.1f).OnComplete(() => {
            scoreText.DOColor(Color.white, 0.2f);
        });
    }
}
