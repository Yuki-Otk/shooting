using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class CubeCamera : MonoBehaviour
{
    // Use this for initialization
    bool SelectPressed=false;
    InteractionSourceState state;
    void Start()
    {
        XRSettings();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 vector = Camera.main.transform.position;
        //Vector3 nextVector = vector + Camera.main.transform.forward.normalized * 10;

        Debug.DrawRay(vector, Camera.main.transform.forward.normalized * 10, Color.red, 1.0f, false);
        if (Physics.Raycast(vector, Camera.main.transform.forward.normalized, out hit, 100))
        {
            if (hit.collider.gameObject.name.IndexOf(":") >= 0)
            {
                //SelectPressed = state.selectPressed;
                //if (SelectPressed)
                //{
                    Debug.Log(hit.collider.gameObject.name);
                    GameObject temp = GameObject.Find(hit.collider.gameObject.name);
                    temp.SetActive(false);
                //}
            }
        }
    }

    private void XRSettings()
    {
        InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
        InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
        InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
    }


    private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        
        state = obj.state;
        if (state.source.kind == InteractionSourceKind.Controller)
        {
            Vector3 PointerPosition;
            Quaternion PointerRotation;
            Vector3 GripPosition;
            Quaternion GripRotation;
            //コントローラのポインタの情報を取得
            state.sourcePose.TryGetPosition(out PointerPosition, InteractionSourceNode.Pointer);
            state.sourcePose.TryGetRotation(out PointerRotation, InteractionSourceNode.Pointer);
            //コントローラ本体の情報を取得
            state.sourcePose.TryGetPosition(out GripPosition, InteractionSourceNode.Grip);
            state.sourcePose.TryGetRotation(out GripRotation, InteractionSourceNode.Grip);
            SelectPressed = state.selectPressed;
            //Debug.Log(SelectPressed);
        }
    }
    private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
    {
        throw new NotImplementedException();
    }

    private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
    {
        //入力ソースの種類を取得
        InteractionSourceKind kind = obj.state.source.kind;
        //検出時の入力ソースのID（消失するまで同じID)
        var id = obj.state.source.id;
    }
}