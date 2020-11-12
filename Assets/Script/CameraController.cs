using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;

public class CameraController : MonoBehaviour
{
    public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float pixsize = 1f;                  // The smallest orthographic size the camera can be.
    public GameObject[] players; 
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;
    private int[] dists;
    private PixelPerfectCamera campix;// The position the camera is moving towards.
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
            campix = this.GetComponent<PixelPerfectCamera>();
            players = GameObject.FindGameObjectsWithTag("Player");
            m_DesiredPosition = GetMeanVector(players);
            this.transform.position = m_DesiredPosition;
            Zoom();
        
    }
    
    private void Zoom ()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        
            float requiredSize = FindRequiredSize();
            campix.assetsPPU = (int)(requiredSize);

    }
    
    private Vector3 GetMeanVector(GameObject[] positions)
    {
        if(positions.Length == 0)
        {
            return Vector3.zero;
        }
     
        Vector3 meanVector = Vector3.zero;
     
        foreach(GameObject pos in positions)
        {
            meanVector += pos.transform.position;
        }
     
        return (new Vector3(meanVector.x / positions.Length, meanVector.y / positions.Length, meanVector.z / positions.Length));
    }

    
    private float FindRequiredSize ()
    {
        if (players.Length == 1)
            return 32;
        // Find the position the camera rig is moving towards in its local space.
        // Start the camera's size calculation at zero.
        float size = 0f;
        // Go through all the targets...
        for (int i = 0; i < players.Length; i++)
        {
            dists[i] = (int)Vector3.Distance(players[i].transform.position, this.transform.position);
            
        }

        // Add the edge buffer to the size.


        // Make sure the camera's size isn't below the minimum.
        if (players != null && dists != null)
            size = 32 - Mathf.Max(dists);
       
        return size;
    }
}
