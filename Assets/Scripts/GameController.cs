using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] Stacks;

    [SerializeField] float speed = 1f;

    private int selectedStack = -1;

    private Vector3 mouseDownPosition;
    private Vector3 cameraStartPosition;
    private Vector3 cameraStartRotation;

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;

        cameraStartPosition = cameraTransform.position;
        cameraStartRotation = cameraTransform.eulerAngles;
    }

    void Update()
    {
        ChangeSelectedStack();

        ControlCamera();
    }

    private void ControlCamera()
    {
        if (selectedStack != -1)
        {
            cameraTransform.LookAt(Stacks[selectedStack].transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 directionToMove = Input.mousePosition - mouseDownPosition;
                cameraTransform.Translate(directionToMove * speed);
            }

            if (cameraTransform.position.y < 0.5)
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x, 0.5f, cameraTransform.position.z);
            }
        }
    }


    private void ChangeSelectedStack()
    {
        if (Input.GetKeyUp("right"))
        {
            selectedStack++;
            if (selectedStack >= Stacks.Length)
            {
                selectedStack = 0;
            }
            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("left"))
        {
            selectedStack--;
            if (selectedStack < 0)
            {
                selectedStack = Stacks.Length - 1;
            }

            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("space"))
        {
            selectedStack = -1;
            ResetCamera();
        }
    }

    private void MoveCameraToStack()
    {
        cameraTransform.position = Stacks[selectedStack].transform.position + Vector3.back * 15 + Vector3.up * 6;
    }

    private void ResetCamera()
    {
        cameraTransform.position = cameraStartPosition;
        cameraTransform.eulerAngles = cameraStartRotation;
    }
}
