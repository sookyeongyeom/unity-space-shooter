using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    // 델리게이트 선언
    delegate float SumHandler(float a, float b);
    // 델리게이트 타입의 변수 선언
    SumHandler sumHandler;

    void Start()
    {
        // 델리게이트 변수에 함수(메서드) 연결(할당)
        sumHandler = Sum;
        // 델리게이트 실행
        float sum = sumHandler(10.0f, 5.0f);
        // 결괏값 출력
        Debug.Log($"Sum = {sum}");

        // 델리게이트 변수에 무명 메서드 연결
        sumHandler = delegate (float a, float b) { return a + b; };
        float sum2 = sumHandler(2.0f, 3.0f);
        Debug.Log($"Sum2 = {sum2}");

        // 델리게이트 변수에 람다식 선언
        sumHandler = (float a, float b) => (a + b);
        float sum3 = sumHandler(10.0f, 5.0f);
        Debug.Log($"Sum3 = {sum3}");
    }

    void Update()
    {

    }

    // 덧셈 연산을 하는 함수
    float Sum(float a, float b)
    {
        return a + b;
    }
}
