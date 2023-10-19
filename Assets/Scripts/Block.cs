using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject blockInfoText;
    private SchoolConcept concept = null;

    void Start()
    {
        blockInfoText = GameObject.Find("BlockInfoText");
    }

    public SchoolConcept GetConcept()
    {
        return concept;
    }

    public void SetConcept(SchoolConcept newValue)
    {
        concept = newValue;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            blockInfoText.GetComponent<TMP_Text>().text = concept.ToString();
        }
    }
}
