using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야 할 대상을 연결할 변수
    public Transform targetTr;
    // Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    // 따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)]    // 다음 라인에 선언한 변수의 입력 범위를 제한 + 인스펙터 뷰에 슬라이드바 표시
    public float distance = 10.0f;

    // Y축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // 반응 속도
    public float damping = 10.0f;

    // 카메라 LootAt의 Offset 값
    public float targetOffset = 2.0f;

    // SmoothDamp에서 사용할 변수
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = GetComponent<Transform>();
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        // 추적해야 할 대상의 뒤쪽으로 distance만큼 이동
        // 높이를 height만큼 이동
        // camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // 타겟과 적정 거리를 유지한 캠의 새로운 위치
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // 구면 선형 보간 함수를 사용해 부드럽게 캠위치를 변경 (시작 위치, 목표 위치, 시간 t)
        // camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);

        // SmoothDamp를 이용한 위치 보간 (시작 위치, 목표 위치, 현재 속도, 도달 시간)
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

        // Camera를 피벗 좌표를 향해 회전 (전방 시야 확보 위해 오프셋 설정)
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
