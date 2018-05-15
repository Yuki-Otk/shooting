using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class reticule : MonoBehaviour {
    private GameObject rtc;
    // マウスカーソル座標
    private Vector3 vector;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    // Use this for initialization
    void Start () {
        rtc = GameObject.Find("reticule");
    }

    // Update is called once per frame
    void Update () {
        DoMouseMove();//マウス追従
        DoMouseClick();//マウス押下時
        
    }
    private void DoMouseClick()
    {
        RaycastHit hit;
        Vector3 nextVector = vector;
        nextVector.z = 100;
        if (Physics.Raycast(vector,nextVector,out hit))
        {
            if (hit.collider.gameObject.name.IndexOf(":")>=0){
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    GameObject temp =GameObject.Find(hit.collider.gameObject.name);
                    //Destroy(temp);
                    temp.SetActive(false);
                }

            }
        }
    }
    float min, max;
    bool flag = false;
    private void DoMouseMove()
    {
        Vector3 inputPos;
        // マウス座標を変数に格納
        //inputPos = Input.mousePosition;
        //右のControllerからの座標
        //inputPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightHand);
        //inputPos = GameObject.Find("Main Camera").transform.position + UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightHand);
        //inputPos =UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.Head);
        inputPos = GameObject.Find("Main Camera").transform.position;
        //Debug.Log(inputPos.x + ":" + inputPos.y);
        inputPos.x = inputPos.x * 100+10;
        if (!flag)
        {
            min = inputPos.y;
            max = inputPos.y;
            flag = true;
        }
        if (min > inputPos.y)
        {
            min = inputPos.y;
        }
        if(max < inputPos.y)
        {
            max = inputPos.y;
        }
        //Debug.Log(max+":"+min);

        //inputPos.y = (inputPos.y-6) * 1f;
        //マウスカーソル座標にカメラのZ座標を代入（カメラのZ座標が-の場合は=-にする）
        //inputPos.z = -Camera.main.transform.position.z;
        //-0.5fに固定
        inputPos.z = -0.5f;
        // マウスカーソル座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(inputPos);
        //オブジェクトの座標に変数の値を代入
        transform.position = screenToWorldPointPosition;
        vector = transform.position;
        vector.z = -0.5f;
        //rtc.transform.position = vector;
        rtc.transform.position = inputPos;
        //this.GetComponent<Rigidbody>().AddForce(transform.right * Input.GetAxisRaw("Horizontal") * accel, ForceMode.Impulse);
    }

}
