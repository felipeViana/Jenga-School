using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Stacks;
    [SerializeField] private GameObject TestMyStackButton;
    [SerializeField] private float speed = 1f;

    private int selectedStackIndex = -1;

    private Vector3 mouseDownPosition;
    private Vector3 cameraStartPosition;
    private Vector3 cameraStartRotation;

    private Transform cameraTransform;

    void Start()
    {
        TestMyStackButton.SetActive(false);
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
        if (selectedStackIndex != -1)
        {
            cameraTransform.LookAt(Stacks[selectedStackIndex].transform.position);

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
            TestMyStackButton.SetActive(true);
            selectedStackIndex++;
            if (selectedStackIndex >= Stacks.Length)
            {
                selectedStackIndex = 0;
            }
            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("left"))
        {
            TestMyStackButton.SetActive(true);
            selectedStackIndex--;
            if (selectedStackIndex < 0)
            {
                selectedStackIndex = Stacks.Length - 1;
            }

            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("space"))
        {
            TestMyStackButton.SetActive(false);
            selectedStackIndex = -1;
            ResetCamera();
        }
    }

    private void MoveCameraToStack()
    {
        cameraTransform.position = Stacks[selectedStackIndex].transform.position + Vector3.back * 15 + Vector3.up * 6;
    }

    private void ResetCamera()
    {
        cameraTransform.position = cameraStartPosition;
        cameraTransform.eulerAngles = cameraStartRotation;
    }

    public void TestMyStack()
    {
        Physics.simulationMode = SimulationMode.FixedUpdate;

        EliminateGlassesBlocks();
    }

    private void EliminateGlassesBlocks()
    {
        GameObject selectedStack = Stacks[selectedStackIndex];

        for (int i = 0; i < selectedStack.transform.childCount; i++)
        {
            GameObject currentRow = selectedStack.transform.GetChild(i).gameObject;

            for (int j = 0; j < currentRow.transform.childCount; j++)
            {
                GameObject currentPosition = currentRow.transform.GetChild(j).gameObject;

                if (currentPosition.transform.childCount > 0)
                {
                    GameObject block = currentPosition.transform.GetChild(0).gameObject;

                    if (block.tag == "glass")
                    {
                        Destroy(block);
                    }
                }
            }

        }

        
    }
}
