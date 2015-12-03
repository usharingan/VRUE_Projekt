/* =====================================================================================
 * ARTiFICe - Augmented Reality Framework for Distributed Collaboration
 * ====================================================================================
 * Copyright (c) 2010-2012 
 * 
 * Annette Mossel, Christian Schönauer, Georg Gerstweiler, Hannes Kaufmann
 * mossel | schoenauer | gerstweiler | kaufmann @ims.tuwien.ac.at
 * Interactive Media Systems Group, Vienna University of Technology, Austria
 * www.ims.tuwien.ac.at
 * 
 * ====================================================================================
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *  
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *  
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * =====================================================================================
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// Class to select and manipulate scene objects with gogo interaction technique (IT). 
/// 
/// GoGo is a 1st person view IT
/// </summary>
public class GoGoInteraction : ObjectSelectionBase
{
	/* ------------------ VRUE Tasks START -------------------
	* 	Implement GoGo interaction technique
	----------------------------------------------------------------- */
	
	public float D = 3.0f;
	public float k = 0.1f;
	
	public float distance;
	public float newDistance;
	
	
	GameObject tracker = null;
	GameObject torso = null;
	GameObject origin = null;
	
	void start()
	{
		tracker = GameObject.Find("TrackerObject");
		torso = GameObject.Find("VirtualCamera");
		
	}
	
	protected override void UpdateSelect()
	{
		torso = GameObject.Find("VirtualCamera");
		tracker = GameObject.Find("TrackerObject"); // TEST: "arm_wrist_left" geht nicht...
		origin = GameObject.Find ("spine_upper");    // TEST!  - "spine_upper"... geht nicht 
//		origin = GameObject.Find ("Mickey_rotated2");
//		origin = GameObject.Find ("head");

		if (D <= 0) {
			D = 0.1f;
		}
		if (k <= 0) {
			k = 0.1f;
		}
		if (k >= 1) {
			k = 0.9f;
		}

		if (tracker) {
			// INTERACTION TECHNIQUE THINGS ------------------------------------------------
			if (tracker.transform.parent.GetComponent<TrackBase> ().isTracked ()) {
				
				tracker.transform.parent.GetComponent<TrackBase>().setVisability(gameObject, true);

				distance = Mathf.Sqrt((tracker.transform.position.x - origin.transform.position.x) * (tracker.transform.position.x - origin.transform.position.x) + 
				                      (tracker.transform.position.y - origin.transform.position.y) * (tracker.transform.position.y - origin.transform.position.y) + 
				                      (tracker.transform.position.z - origin.transform.position.z) * (tracker.transform.position.z - origin.transform.position.z));
				
				float normX = (tracker.transform.position.x - origin.transform.position.x) / distance;
				float normY = (tracker.transform.position.y - origin.transform.position.y) / distance;
				float normZ = (tracker.transform.position.z - origin.transform.position.z) / distance;
				
				
				
				Vector3 normalizedVector = new Vector3(normX, normY,normZ);
				//Vector3 normalizedVector = (transform.position - torso.transform.position) / distance;
				
				newDistance = 0.0f;
				if(distance >= D)
				{
					newDistance = distance + k * ((distance - D) * (distance - D));
					//this.transform.position = new Vector3(normalizedVector.x * newDistance, normalizedVector.y * newDistance, normalizedVector.z * newDistance);
					
					this.transform.position = tracker.transform.position + (normalizedVector * newDistance);
					this.transform.rotation = tracker.transform.rotation;
					
				}
				// Transform (translate and rotate) selected object depending on of virtual hand's transformation
				if (selected)
				{
					this.transformInter(this.transform.position, this.transform.rotation);
				}
				
			}else{
				tracker.transform.parent.GetComponent<TrackBase>().setVisability(gameObject, false);
			}
		}
	}
	
	// ------------------ VRUE Tasks END ----------------------------

	
    /// <summary>
    /// Callback
    /// If our selector-Object collides with anotherObject we store the other object 
    /// 
    /// For usability purpose change color of collided object
    /// </summary>
    /// <param name="other">GameObject giben by the callback</param>
    public void OnTriggerEnter(Collider other)
    {		
        if (isOwnerCallback())
        {
            GameObject collidee = other.gameObject;

            if (hasObjectController(collidee))
            {

                collidees.Add(collidee.GetInstanceID(), collidee);
                //Debug.Log(collidee.GetInstanceID());

                // change color so user knows of intersection
                collidee.renderer.material.SetColor("_Color", Color.blue);
            }
        }
    }

    /// <summary>
    /// Callback
    /// If our selector-Object moves out of anotherObject we remove the other object from our list
    /// 
    /// For usability purpose change color of collided object
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (isOwnerCallback())
        {
            GameObject collidee = other.gameObject;

            if (hasObjectController(collidee))
            {
                collidees.Remove(collidee.GetInstanceID());

                // change color so user knows of intersection end
                collidee.renderer.material.SetColor("_Color", Color.white);
            }
        }
    }

	public Hashtable getCollidees(){
		return collidees;
	}
}