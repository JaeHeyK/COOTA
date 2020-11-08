using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prevScript
{
    public class LightPuzzle : MonoBehaviour
    {
        public GameObject Target0;
        public GameObject Target1;

        public void TargetSetActive()
        {
            Target0.SetActive(!Target0.active);
            Target1.SetActive(!Target1.active);
        }
    }
}
