using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    private List<GameSelectPanel> selectPanelArray = new List<GameSelectPanel>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameSelectPanel[] foundPanels = GetComponentsInChildren<GameSelectPanel>(true);
        selectPanelArray.AddRange(foundPanels);
        Debug.Log($"{selectPanelArray.Count}");

        for(int i = 0; i < selectPanelArray.Count; i++)
        {
            var panel = selectPanelArray[i];
            int x, y;
            GameHelper.CalcPanelPosition(out x, out y, i);

            Vector2 location = GameHelper.CalcPanelLocation(x, y);
            panel.transform.localPosition = location;
            Debug.Log($"{i}: {selectPanelArray[i].name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
