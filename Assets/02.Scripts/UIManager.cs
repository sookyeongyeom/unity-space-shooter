using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Unity-UI
using UnityEngine.Events;   // UnityEvent 관련 API
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 버튼을 연결할 변수
    public Button startButton;
    public Button optionButton;
    public Button shopButton;

    private UnityAction action;

    void Start()
    {
        // UnityAction을 사용한 이벤트 연결 방식
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        // 무명 메서드를 활용한 이벤트 연결 방식
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });

        // 람다식을 활용한 이벤트 연결 방식
        shopButton.onClick.AddListener(() => OnButtonClick(shopButton.name));
    }

    void Update()
    {

    }

    public void OnButtonClick(string msg)
    {
        Debug.Log($"Button Clicked! : {msg}");
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Level_01");
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }
}
