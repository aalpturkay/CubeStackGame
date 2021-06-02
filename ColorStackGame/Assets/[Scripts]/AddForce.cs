using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private Transform stackPointTransform;

    private void Start()
    {
        //AddForceCubes();
        //MoveToStackPoint();
        StartCoroutine(MoveCubesToStackPointIE());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
            }
        }
    }

    private void AddForceCubes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void MoveToStackPoint()
    {
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            transform.GetChild(i).DOMove(stackPointTransform.position, 5f);
        }
    }

    IEnumerator MoveCubesToStackPointIE()
    {
        var cubeScale = 0;
        var targetPos = new Vector3(stackPointTransform.position.x, cubeScale, stackPointTransform.position.z);
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i)
                .DOMove(
                    new Vector3(stackPointTransform.position.x, stackPointTransform.position.y + cubeScale,
                        stackPointTransform.position.z), .2f);
            cubeScale += 1;
            yield return new WaitForSeconds(.1f);
        }
    }
}