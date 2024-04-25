using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class DistanceToTheNearestTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private OVRCameraRig _cameraRig;
    [SerializeField] private GameObject apparatusPrefab;

    private OVRSceneManager _ovrSceneManager;

    private OVRSceneRoom _sceneRoom;

    private List<GameObject> _tables;
    private Vector3 rigPosition;

    private void Awake()
    {
        _tables = new List<GameObject>();
        _ovrSceneManager = FindObjectOfType<OVRSceneManager>();
        _ovrSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
    }

    private void SceneLoaded()
    {
        if (_cameraRig != null) {
             rigPosition = _cameraRig.transform.position;
        }
        _sceneRoom = FindObjectOfType<OVRSceneRoom>();
        OVRSemanticClassification[] allClassifications = FindObjectsOfType<OVRSemanticClassification>();
        
        
        foreach (var table in allClassifications)
        {
            if (table.Labels[0] == OVRSceneManager.Classification.Table)
            {
                _tables.Add(table.gameObject);
            }
           
        }
        
    }

    private void Update()
    {
        if (_sceneRoom != null)
        {

            GameObject nearestTableToCameraRig = FindNearestTable(rigPosition);

            if (nearestTableToCameraRig != null)
            {
                float distanceToController = CalculateDistanceToTable(rigPosition, nearestTableToCameraRig);
                distanceText.text = "Distance: " + distanceToController.ToString("F2");
            }
        }
        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            OnSpawnApparatus();
        }
    }

    private GameObject FindNearestTable(Vector3 position)
    {
        GameObject nearestTable = null;
        float nearestDistance = float.MaxValue;

        foreach (var tableVolume in _tables)
        {
            float distance = CalculateDistanceToTable(position, tableVolume);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTable = tableVolume;
            }
        }

        return nearestTable;
    }

    private float CalculateDistanceToTable(Vector3 position, GameObject tableVolume)
    {
        Vector3 tableNormal = tableVolume.transform.forward;

        float tableDistance = -Vector3.Dot(tableNormal, tableVolume.transform.position);
        float distance = Mathf.Abs(Vector3.Dot(tableNormal, position) + tableDistance) / tableNormal.magnitude;

        return distance;
    }

    private void OnSpawnApparatus()
    {
        GameObject nearestTableToCameraRig = FindNearestTable(rigPosition);

       

        if (nearestTableToCameraRig != null)
        {
            // Calculate the top surface of the table by taking the table's position and adding half of its height
            Vector3 spawnPosition = nearestTableToCameraRig.transform.position;
            
            spawnPosition += -Vector3.forward;

            Instantiate(apparatusPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
