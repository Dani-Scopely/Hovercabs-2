using System;
using Hovercabs.Views;
using UnityEngine;

namespace Hovercabs.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(VehicleView))]
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        private Rigidbody _rigidbody;
        private VehicleView _view;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _view = GetComponent<VehicleView>();
        }

        private void Start()
        {
            _view.Render();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rigidbody.AddForce(new Vector3(0,0,1));
                //transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                _rigidbody.AddForce(new Vector3(0,0,-5));
            }else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
            }else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            
            
        }
    }
}