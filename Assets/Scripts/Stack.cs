using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class Stack : MonoBehaviour
{
    [SerializeField] private GameObject GlassBlock;
    [SerializeField] private GameObject WoodBlock;
    [SerializeField] private GameObject StoneBlock;

    [SerializeField] private GameObject Label;

    [SerializeField] private int grade;

    private bool hasLoaded = false;

    private List<SchoolConcept> concepts;

    private void Start()
    {
        UpdateLabelText();
    }

    private void UpdateLabelText()
    {
        Label.GetComponent<TMP_Text>().text = grade + "th grade";
    }

    void Update()
    {
        if (!hasLoaded && DataFetch.Instance.HasLoaded())
        {
            hasLoaded = true;
            CreateBlocks();
        }    
    }

    public void CreateBlocks()
    {
        List<SchoolConcept> concepts = DataFetch.Instance.GetConceptsForGrade(grade);

        int conceptIndex = 0;
        int levels = concepts.Count / 6 + 1;

        for (int levelIndex = 0; levelIndex < levels; levelIndex++)
        {
            GameObject RowX = transform.GetChild(0 + levelIndex * 2).gameObject;
            for (int i = 0; i < 3; i++)
            {
                if (conceptIndex >= concepts.Count) continue;
                var positionX = RowX.transform.GetChild(i).gameObject;
                CreateBlockAt(positionX.transform, concepts[conceptIndex]);
                conceptIndex++;
            }

            GameObject RowZ = transform.GetChild(1 + levelIndex * 2).gameObject;
            for (int i = 0; i < 3; i++)
            {
                if (conceptIndex >= concepts.Count) continue;
                var positionZ = RowZ.transform.GetChild(i).gameObject;
                CreateBlockAt(positionZ.transform, concepts[conceptIndex]);
                conceptIndex++;
            }
        }
    }

    private GameObject CreateBlockAt(Transform transform, SchoolConcept concept)
    {
        GameObject newBlock;
        switch (concept.mastery)
        {
            case 0:
            default:
                newBlock = Instantiate(GlassBlock, transform);
                break;

            case 1:
                newBlock = Instantiate(WoodBlock, transform);
                break;

            case 2:
                newBlock = Instantiate(StoneBlock, transform);
                break;
        }

        newBlock.GetComponent<Block>().SetConcept(concept);
        return newBlock;
    }

    public void DestroyGlassesBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentRow = transform.GetChild(i).gameObject;

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

    public void DestroyAllBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentRow = transform.GetChild(i).gameObject;

            for (int j = 0; j < currentRow.transform.childCount; j++)
            {
                GameObject currentPosition = currentRow.transform.GetChild(j).gameObject;

                if (currentPosition.transform.childCount > 0)
                {
                    GameObject block = currentPosition.transform.GetChild(0).gameObject;
                    Destroy(block);
                }
            }
        }
    }
}
