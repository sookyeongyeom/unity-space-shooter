using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{

    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = _color;
        // 구체 모양의 기즈모 생성 (생성 위치, 반지름)
        Gizmos.DrawSphere(transform.position, _radius);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
