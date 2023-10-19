using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private SchoolConcept concept = null;

    void Start()
    {
        GameController.Instance.SetActiveInfoText(false);
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
            GameController.Instance.SetActiveInfoText(true);
            GameController.Instance.SetBlockInfoText(concept.ToString());
        }
    }
}
