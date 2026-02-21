using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private class InputData
    {
        public bool Continue;  //  コンテニュー入力
        public bool Trigger;   //  トリガ入力
        public bool Repeat;    //  リピート入力

        public bool BeforeContinue;  //  前回入力

        public void Init()
        {
            Continue = false;
            Trigger = false;
            Repeat = false;
            BeforeContinue = false;
        }
    };

    private InputData InputOk;
    private InputData InputCancel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputOk = new InputData();
        InputOk.Init();
        InputCancel = new InputData();
        InputCancel.Init();
        //お試し
        sceneManager = FindAnyObjectByType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        
        if(keyboard.fKey.wasPressedThisFrame)
        {
            // Fキーが押されたときの処理
            Ok();
            Debug.Log("Fキーが押されました！");
        }
        InputRun(InputOk);
        if(keyboard.fKey.wasReleasedThisFrame)
        {
            // Fキーが離されたときの処理
            ReleaseOk();
            Debug.Log("Fキーが離されました！");
        }

        if(keyboard.spaceKey.wasPressedThisFrame)
        {
            // スペースキーが押されたときの処理
            Cancel();
            Debug.Log("スペースキーが押されました！");
        }
        InputRun(InputCancel);
        if(keyboard.spaceKey.wasReleasedThisFrame)
        {
            // スペースキーが離されたときの処理
            ReleaseCancel();

            //Object.FindFirstObjectByType<PointManager>().AddPoint(10);
            //ポイントをつかする際は上記の文章を書くことで解決する

            Debug.Log("スペースキーが離されました！");
        }
    }

    //  入力の開始
    void InputBegin(InputData InputData)
    {
        InputData.Continue = true;
        InputData.Trigger = true;
        InputData.Repeat = true;
        InputData.BeforeContinue = false;
    }

    //  入力中
    void InputRun(InputData InputData)
    {
        if (InputData.BeforeContinue)
        {
            InputData.Trigger = false;
        }
        InputData.BeforeContinue = InputData.Continue;
    }

    //  入力リリース
    void InputRelease(InputData InputData)
    {
        InputData.Continue = false;
        InputData.Trigger = false;
        InputData.Repeat = false;
        InputData.BeforeContinue = false;
    }
    public void Ok()
    {
        InputBegin(InputOk);
    }

    public void ReleaseOk()
    {
        InputRelease(InputOk);
    }

    public bool IsOk()
    {
        return InputOk.Continue;
    }

    //  決定入力のトリガ
    public bool IsOkTrigger()
    {
        return InputOk.Trigger;
    }

    public void Cancel()
    {
        InputBegin(InputCancel);
    }

    public void ReleaseCancel()
    {
        InputRelease(InputCancel);
    }

    public bool IsCancel()
    {
        return InputCancel.Continue;
    }

    //  キャンセル入力のトリガ
    public bool IsCancelTrigger()
    {        
        return InputCancel.Trigger;    
    }

}
