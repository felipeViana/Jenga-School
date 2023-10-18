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
        
    }

    private GameObject CreateBlockAt(Transform transform, SchoolConcept concept)
    {
        switch (concept.mastery)
        {
            case 0:
            default:
                return Instantiate(GlassBlock, transform);

            case 1:
                return Instantiate(WoodBlock, transform);

            case 2:
                return Instantiate(StoneBlock, transform);
        }
    }
}
