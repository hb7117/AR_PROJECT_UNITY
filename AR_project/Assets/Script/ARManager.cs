using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManage : MonoBehaviour
{
    public GameObject ObjectToPlace;  //회면 터치시 생성할 오브젝트
    public ARRaycastManager aRRaycastManager; //ARRaycastManager를 참조하여 AR 세션에서 평면을 탐지하고, 터치시 오브젝트를 배치하는 데 사용

    private void Update()
    {
        UpdateCursor();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)   //터치시 오브젝트 생성
        {
            Instantiate(ObjectToPlace, transform.position, Quaternion.identity);
        }
    }

    void UpdateCursor() //회면 증앙의 좌표를 가져와 해당 위치에서 평면 탐지
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);    //평면을 탐지
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

        }
    }
}
