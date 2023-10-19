using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject textBackground;
    [SerializeField] private GameObject blockInfoText;
    [SerializeField] private float timeToShowInfoText = 2f;
    private float timeShowingInfoText = 0f;

    [SerializeField] private Material highlightMaterial;

    public static GameController Instance { get; private set; }

    void Start()
    {
        Instance = this;

        SetActiveInfoText(false);
    }

    void Update()
    {
        timeShowingInfoText += Time.deltaTime;
        if (timeShowingInfoText > timeToShowInfoText || ClickOutSideBlocks())
        {
            SetActiveInfoText(false);
        }
    }

    private bool ClickOutSideBlocks()
    {
        if (Input.GetMouseButtonDown(1))
        {
            return true;
        }

        return false;
    }

    public void SetActiveInfoText(bool active)
    {
        timeShowingInfoText = 0;

        textBackground.SetActive(active);
        blockInfoText.SetActive(active);
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
