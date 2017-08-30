using UnityEngine;
using System.Collections;

namespace  PC2D
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

		void Start()
		{
			target = GameObject.FindWithTag("Player").transform;
		}
        
        void Update()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;

            transform.position = pos;
        }
    }
}
