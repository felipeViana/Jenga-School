using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Stacks;
    [SerializeField] private GameObject TestMyStackButton;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private float speed = 1f;

    [SerializeField] private GameObject textBackground;
    [SerializeField] private GameObject blockInfoText;
    [SerializeField] private float timeToShowInfoText = 2f;
    private float timeShowingInfoText = 0f;

    [SerializeField] private Material highlightMaterial;

    private int selectedStackIndex = -1;

    private Vector3 mouseDownPosition;
    private Vector3 cameraStartPosition;
    private Vector3 cameraStartRotation;

    private Transform cameraTransform;

    public static GameController Instance { get; private set; }

    void Start()
    {
        Instance = this;

        TestMyStackButton.SetActive(false);
        RestartButton.SetActive(false);
        SetActiveInfoText(false);

        cameraTransform = Camera.main.transform;

        cameraStartPosition = cameraTransform.position;
        cameraStartRotation = cameraTransform.eulerAngles;
    }

    void Update()
    {
        timeShowingInfoText += Time.deltaTime;
        if (timeShowingInfoText > timeToShowInfoText)
        {
            SetActiveInfoText(false);
        }

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
            RestartButton.SetActive(true);
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
            RestartButton.SetActive(true);
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
            RestartButton.SetActive(false);
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
        selectedStack.GetComponent<Stack>().DestroyGlassesBlocks();
    }

    public void RestartStacks()
    {
        Physics.simulationMode = SimulationMode.Script;
        RestoreStacks();
    }

    private void RestoreStacks()
    {
        DestroyAllBlocks();
        RecreateAllBlocks();
    }

    private void DestroyAllBlocks()
    {
        GameObject selectedStack = Stacks[selectedStackIndex];
        selectedStack.GetComponent<Stack>().DestroyAllBlocks();
    }

    private void RecreateAllBlocks()
    {
        GameObject selectedStack = Stacks[selectedStackIndex];
        selectedStack.GetComponent<Stack>().CreateBlocks();
    }

    public void SetActiveInfoText(bool active)
    {
        timeShowingInfoText = 0;

        textBackground.SetActive(active);
        blockInfoText.SetActive(active);
    }

    public bool IsInfoTextActive()
    {
        return textBackground.activeSelf;
    }

    public void SetBlockInfoText(string newText)
    {
        blockInfoText.GetComponent<TMP_Text>().text = newText;
    }

    public Material GetHighlightMaterial()
    {
        return highlightMaterial;
    }
}
