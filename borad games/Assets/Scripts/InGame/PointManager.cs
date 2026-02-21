using UnityEngine;
using TMPro; // TextMeshProを使うために必要

public class PointManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // インスペクターからTextを紐付け
    private int _currentPoint = 0;

    // ポイントを変更するためのプロパティ
    public int CurrentPoint
    {
        get { return _currentPoint; }
        set 
        { 
            _currentPoint = value;
            UpdateDisplay(); // 値が変わるたびに表示を更新
        }
    }

    void Start()
    {
        UpdateDisplay(); // 開始時に0を表示
    }

    // 表示を更新するメソッド
    private void UpdateDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {_currentPoint}";
        }
    }

    // 外部からポイントを加算したい時に呼ぶメソッド
    public void AddPoint(int amount)
    {
        CurrentPoint += amount;
    }
}