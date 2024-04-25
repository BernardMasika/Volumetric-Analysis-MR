using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;

public class MRSurfaceSpawner : MonoBehaviour
{
    public GameObject debugCube;
    Action debugAction = null;
    //public Text logs;
     public void GetLargestSurfaceDebugger()
        {
            try
            {
              
                    string surfaceType = OVRSceneManager.Classification.Table; // using table as the default value
                    /*if (surfaceTypeDropdown)
                    {
                        surfaceType = surfaceTypeDropdown.options[surfaceTypeDropdown.value].text.ToUpper();
                    }*/
                    MRUKAnchor largestSurface = MRUK.Instance?.GetCurrentRoom()?.FindLargestSurface(surfaceType);
                    if (largestSurface != null)
                    {
                        if (debugCube != null)
                        {
                            Vector3 anchorSize = largestSurface.VolumeBounds.HasValue ? largestSurface.VolumeBounds.Value.size : new Vector3(0,0,0.01f);
                            if (largestSurface.PlaneRect.HasValue)
                            {
                                anchorSize = new Vector3(largestSurface.PlaneRect.Value.x, largestSurface.PlaneRect.Value.y, 0.01f);
                            }
                            debugCube.transform.localScale = anchorSize;
                            debugCube.transform.localPosition = largestSurface.transform.position;
                            debugCube.transform.localRotation = largestSurface.transform.rotation;
                        }
                        /*SetLogsText("\n[{0}]\nAnchor: {1}\nType: {2}",
                            nameof(GetLargestSurfaceDebugger),
                            largestSurface.name,
                            largestSurface.AnchorLabels[0]
                        );*/
                        print("The TYPE IS: " + largestSurface.AnchorLabels[0]);
                    
                   
                        /*SetLogsText("\n[{0}]\n No surface of type {1} found.",
                            nameof(GetLargestSurfaceDebugger),
                            surfaceType
                        );*/
                        print("No surface of type: " + surfaceType);
                    
                }
                else
                {
                    debugAction = null;
                }
                if (debugCube != null)
                {
                    debugCube.SetActive(true);
                }
            }
            catch (Exception e)
            {
                SetLogsText("\n[{0}]\n {1}\n{2}",
                    nameof(GetLargestSurfaceDebugger),
                    e.Message,
                    e.StackTrace
                );
            }
        }
     
        void SetLogsText(string logsText, params object[] args)
        {
            /*if (logs)
            {
                logs.text = String.Format(logsText, args);
            }*/
        }
}
