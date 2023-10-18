using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;
using TreeEditor;
using System.Collections.Generic;


public static class JsonHelper
{
    public static string FixJson(string value)
    {
        return "{\"Items\":" + value + "}";
    }

    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}

[Serializable]
public class SchoolConcept : IComparable<SchoolConcept>
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;

    public int CompareTo(SchoolConcept other)
    {
        int domainComparison = mastery.CompareTo(other.mastery);
        if (domainComparison != 0) return domainComparison;

        int clusterComparison = cluster.CompareTo(other.cluster);
        if (clusterComparison != 0) return clusterComparison;

        int standardIdComparison = standardid.CompareTo(other.standardid);
        return standardIdComparison;
    }

    public override string ToString()
    {
        string returnValue = grade + ": " + domain;
        returnValue += "\n" + cluster;
        returnValue += "\n" + standardid + ": " + standarddescription;

        return returnValue;
    }
}

public class DataFetch : MonoBehaviour
{
    [SerializeField] private string uri = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private List<SchoolConcept> info;
    private List<SchoolConcept> Concepts6th = new List<SchoolConcept>();
    private List<SchoolConcept> Concepts7th = new List<SchoolConcept>();
    private List<SchoolConcept> Concepts8th = new List<SchoolConcept>();

    private bool hasLoaded = false;

    static public DataFetch Instance { get; private set; }

    void Start()
    {
        Instance = this;

        Physics.simulationMode = SimulationMode.Script;

        StartCoroutine(GetRequest(uri));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string jsonResponse = webRequest.downloadHandler.text;

                    string fixedJson = JsonHelper.FixJson(jsonResponse);
                    info = JsonHelper.FromJson<SchoolConcept>(fixedJson);

                    info.Sort();

                    AddConceptsToLists();
                    hasLoaded = true;

                    break;
            }
        }
    }

    public bool HasLoaded()
    {
        return hasLoaded;
    }

    public List<SchoolConcept> GetInfo()
    {
        return info;
    }

    public List<SchoolConcept> Get6thGradeConcepts()
    {
        return Concepts6th;
    }

    public List<SchoolConcept> Get7thGradeConcepts()
    {
        return Concepts7th;
    }

    public List<SchoolConcept> Get8thGradeConcepts()
    {
        return Concepts8th;
    }

    public List<SchoolConcept> GetConceptsForGrade(int grade)
    {
        switch (grade)
        {
            case 6:
            default:
                return Get6thGradeConcepts();
            case 7:
                return Get7thGradeConcepts();
            case 8:
                return Get8thGradeConcepts();
        }
    }


    private void AddConceptsToLists()
    {
        List<SchoolConcept> concepts = DataFetch.Instance.GetInfo();

        for (int i = 0; i < concepts.Count; i++)
        {
            SchoolConcept concept = concepts[i];

            if (concept.grade == "6th Grade")
            {
                Concepts6th.Add(concept);
            }
            else if (concept.grade == "7th Grade")
            {
                Concepts7th.Add(concept);
            }
            else if (concept.grade == "8th Grade")
            {
                Concepts8th.Add(concept);
            }
        }
    }

    private void PrintConcepts()
    {
        foreach (var concept in Concepts7th)
        {
            Debug.Log(concept.ToString());
        }
    }
}