//-----------------------------------------------------------------------
// <copyright file="AndyPlacementManipulator.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.ObjectManipulation
{
    using GoogleARCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controls the placement of Andy objects via a tap gesture.
    /// </summary>
    public class AndyPlacementManipulator : Manipulator
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject Prefab1;
        public GameObject Prefab2;
        public GameObject Prefab3;
        public GameObject Prefab4;

        /// <summary>
        /// Manipulator prefab to attach placed objects to.
        /// </summary>
        public GameObject ManipulatorPrefab;
        GameObject prefab;
        

        private List<GameObject> sceneObject = new List<GameObject>();
        private List<GameObject> myPrefab = new List<GameObject>();
      

        private List<float> totalPrice = new List<float>();
        float realPrice;
        float one = 100.00f;
        float two = 649.99f;
        float three = 150.00f;
        float four = 250.00f;

        float total =0;
        public Text TPrice;



        private void Start()
        {
            myPrefab.Add(Prefab1);
            myPrefab.Add(Prefab2);
            myPrefab.Add(Prefab3);
            myPrefab.Add(Prefab4);

        

            

        }
        int index;
        public void firstClicked()
        {
            index = myPrefab.IndexOf(Prefab1);
            
        }
        public void secondClicked()
        {
            index = myPrefab.IndexOf(Prefab2);
        }

        public void thirdClicked()
        {
            index = myPrefab.IndexOf(Prefab3);
        }
        public void forthClicked()
        {
            index = myPrefab.IndexOf(Prefab4);
        }


        public void ClearScene()
        {
            foreach(var mm in sceneObject)
            {
                Destroy(mm);
            }
            sceneObject.Clear();
            totalPrice.Clear();
        }

        public void countPrice()
        {
            total = totalPrice.Sum();
            TPrice.text = "RM " + total.ToString(); 
            
        }
      
        
        /// <summary>
        /// Returns true if the manipulation can be started for the given gesture.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        /// <returns>True if the manipulation can be started.</returns>
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (gesture.TargetObject == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Function called when the manipulation is ended.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.WasCancelled)
            {
                return;
            }

            // If gesture is targeting an existing object we are done.
            if (gesture.TargetObject != null)
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;


           
                if (Frame.Raycast(gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
                {
                    // Use hit pose and camera pose to check if hittest is from the
                    // back of the plane, if it is, no need to create the anchor.
                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else
                    {
                        
                    if(index == 0)
                    {
                        prefab = Prefab1;
                        realPrice = one;

                    }
                    else if( index == 1)
                    {
                        prefab = Prefab2;
                        realPrice = two;
                    }
                    else if(index ==2)
                    {
                        prefab = Prefab3;
                        realPrice = three;
                    }
                    else
                    {
                        prefab = Prefab4;
                        realPrice = four;
                    }
                   
                            // Instantiate Andy model at the hit pose.
                            var andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                            sceneObject.Add(andyObject);
                    totalPrice.Add(realPrice);
                            // Instantiate manipulator.
                            var manipulator = Instantiate(ManipulatorPrefab, hit.Pose.position, hit.Pose.rotation);

                            // Make Andy model a child of the manipulator.
                            andyObject.transform.parent = manipulator.transform;

                            // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                            // world evolves.
                            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                            // Make manipulator a child of the anchor.
                            manipulator.transform.parent = anchor.transform;

                            // Select the placed object.
                            manipulator.GetComponent<Manipulator>().Select();
                        
                    }
                }


            }
        }

       
    }

