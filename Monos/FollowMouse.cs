using UnityEngine;

namespace UnityUtility.Monos {
    public class FollowMouse: MonoBehaviour {
        void Update() {
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}