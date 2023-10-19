using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] Stacks;
    [SerializeField] private GameObject TestMyStackButton;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject CameraCentralPoint;
    
    private int selectedStackIndex = -1;
    private Transform cameraTransform;
    private Vector3 cameraStartPosition;
    private Vector3 cameraStartRotation;

    void Start()
    {
        TestMyStackButton.SetActive(false);
        RestartButton.SetActive(false);

        cameraTransform = Camera.main.transform;
        cameraStartPosition = cameraTransform.position;
        cameraStartRotation = cameraTransform.eulerAngles;
    }

    void Update()
    {
        ChangeSelectedStack();
    }

    private void ChangeSelectedStack()
    {
        if (Input.GetKeyUp("right"))
        {
            SetButtonsActive(true);
            selectedStackIndex++;
            if (selectedStackIndex >= Stacks.Length)
            {
                selectedStackIndex = 0;
            }
            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("left"))
        {
            SetButtonsActive(true);
            selectedStackIndex--;
            if (selectedStackIndex < 0)
            {
                selectedStackIndex = Stacks.Length - 1;
            }

            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("space"))
        {
            SetButtonsActive(false);
            selectedStackIndex = -1;
            ResetCamera();
        }
    }

    private void SetButtonsActive(bool active)
    {
        TestMyStackButton.SetActive(active);
        RestartButton.SetActive(active);
    }

    private void MoveCameraToStack()
    {
        for (int i = 0; i < Stacks.Length; i++)
        {
            Stacks[i].GetComponent<Stack>().SetActiveLight(false);
        }

        switch (selectedStackIndex)
        {
            case 0:
            default:
                CameraCentralPoint.transform.position = new Vector3(-20, CameraCentralPoint.transform.position.y, CameraCentralPoint.transform.position.z);
                break;
            case 1:
                CameraCentralPoint.transform.position = new Vector3(0, CameraCentralPoint.transform.position.y, CameraCentralPoint.transform.position.z);
                break;
            case 2:
                CameraCentralPoint.transform.position = new Vector3(20, CameraCentralPoint.transform.position.y, CameraCentralPoint.transform.position.z);
                break;
        }

        cameraTransform.position = Stacks[selectedStackIndex].transform.position + Vector3.back * 17 + Vector3.up * 6;
        CameraCentralPoint.transform.Rotate(Vector3.zero);
        cameraTransform.LookAt(CameraCentralPoint.transform.position);

        Stacks[selectedStackIndex].GetComponent<Stack>().SetActiveLight(true);
    }

    private void ResetCamera()
    {
        cameraTransform.position = cameraStartPosition;
        cameraTransform.eulerAngles = cameraStartRotation;
    }

    public GameObject GetSelectedStack()
    {
        if (selectedStackIndex < 0) return null;

        return Stacks[selectedStackIndex]; 
    }
}
