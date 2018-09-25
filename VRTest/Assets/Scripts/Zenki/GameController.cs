using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance_ = null;
    public static GameController Instance { get { return instance_; } }
    private TextMesh text_;
    private float timer_ = 30f;
    private bool end_ = false;

    public void GameOver()
    {
        if (end_ == true) return;
        end_ = true;
        Time.timeScale = 0f;
        text_.text = "Game Over";
    }

    // Use this for initialization
    void Start ()
    {
        // インスタンスが生成されてるかどうかをチェックする
        if (null == instance_)
        {
            // ないなら自分を渡す
            instance_ = this;
        }
        else
        {
            // すでにあるなら自分を破棄する
            DestroyImmediate(gameObject);
            return;
        }

        text_ = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer_ -= Time.deltaTime;
        if(timer_ <= 0f)
        {
            timer_ = 0f;
            if(end_ == false)
            {
                end_ = true;
                Time.timeScale = 0f;
                text_.text = "Clear!!!";
            }
        }

        if(end_ == false)
        {
            text_.text = timer_.ToString("00.00");
        }
	}
}
