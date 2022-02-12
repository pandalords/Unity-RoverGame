    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;

    public class MotorController : MonoBehaviour
    {
        
        [SerializeField] float carSpeed = 2000f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float regularTimeRotationMultiplier = 200f;
        [SerializeField] float slowMotionRotationMultiplier = 20f;
        [SerializeField] float flipBoost = 20f;
        [SerializeField] float bouncePadForce = 3f;
        [SerializeField] float zoomPadForce = 3f;
        public float TimeSpeedReduction = 0.3f;
        public bool isInAir = false;
        public bool isInSlowMo;

        public bool isAlive;

        public WheelJoint2D backWheel;
        public WheelJoint2D frontWheel;
        public Rigidbody2D rb;
        public BackTireOffGround backTire;
        public FrontTireOffGround frontTire;
        public BoxCollider2D carFrame;
        public CapsuleCollider2D car;

        CheckPointManager cPM;

        
        Vector2 moveInput;
        Vector2 torqueInput;
        
        float previousCarRotation = 0;
        float currentCarRotation = 0;
        float totalAngleRotated;
        float carMovement = 0f;
        float carRotation = 0f;
        
        bool isCarFrameTouchingGround;
        bool hasBegunBackflip = false;
        bool hasCompletedBackflip = false;

        //bool hasBegunFrontflip = false;

        void Start()
        {
            cPM = GameObject.FindGameObjectWithTag("CPM").GetComponent<CheckPointManager>();
            
            if (!isAlive)
            {
                transform.position = cPM.lastCheckPointPos;
                isAlive = true;
            }
        }

        void Update()
        {
            Drive();
            Rotate();
            CountFlips();
            Pads();
            CarIsInAir();
            CarFrameTouchingGround();
        }

        void FixedUpdate() 
        {
            UseMotor();
            RotateCar();
        }

        void Drive()
        {
            carMovement = moveInput.x * -carSpeed;
        }

        void Rotate()
        {
            carRotation = moveInput.y * rotationSpeed;
        }


        void UseMotor()
        {
            if (carMovement == 0f)
            {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
            }
            else
            {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D {motorSpeed = carMovement, maxMotorTorque = 10000};
            backWheel.motor = motor;
            frontWheel.motor = motor;
            }
        }

        void RotateCar()
        {
            if (!isInSlowMo && carRotation != 0f)
            {
                float totalRotation = (carRotation * rotationSpeed * Time.deltaTime) / regularTimeRotationMultiplier;
                transform.Rotate (0,0,totalRotation);
            }
            else if (isInSlowMo && carRotation != 0f)
            {
                transform.Rotate (0,0,(carRotation * rotationSpeed * Time.deltaTime) / slowMotionRotationMultiplier);
            }
            else if (isInAir)
            {
                rb.angularVelocity = 0;
            }
            else
            {
                return;
            }
        }
        
        void CountFlips()
        {
            if(isInAir)
            {
                //vocab
                //transform.eulerAngles = the current car rotation from 0 - 360, starting at the right hand side of the circle, returned in a Vector3. z is the rotation of the object
                //Mathf.Abs = Finds the absolute number of the equation
                
                currentCarRotation = transform.eulerAngles.z;
                float theta = Mathf.Abs(currentCarRotation - previousCarRotation);
                if (theta > 300)
                {
                    theta = 0;
                }

                totalAngleRotated += theta;

                //below if statement not working correctly
                if ((totalAngleRotated > 180) && (transform.eulerAngles.z > 220 && 240 < transform.eulerAngles.z))
                {
                    hasBegunBackflip = true;
                    //hasBegunFrontflip = true;
                }

                if (totalAngleRotated > 300 && hasBegunBackflip == true)
                {
                    Debug.Log("Flip");
                    hasCompletedBackflip = true;
                    totalAngleRotated = -60;
                    hasBegunBackflip = false;
                }

                previousCarRotation = currentCarRotation;

                //Debug.Log(totalAngleRotated);
            }
            else if (!isInAir)
            {
                totalAngleRotated = 0;
            }
        }

        public void Die()
        {
            //isAlive = false;
            transform.position = cPM.lastCheckPointPos;
            rb.velocity = new Vector3(0,0,0);
            transform.rotation = Quaternion.identity;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        void OnFlipBoost(InputValue value)
        {
            if (hasCompletedBackflip && isInAir)
            {
            rb.AddForce(transform.right * flipBoost, ForceMode2D.Impulse);
            hasCompletedBackflip = false;
            }
        }

        void CarIsInAir()
        {
            if (backTire.backWheelIsTouchingGround == false && frontTire.frontTireColliderIsTouchingGround == false && isCarFrameTouchingGround == false)
            {
                isInAir = true;
            }
            else
            {
                isInAir = false;
            }
        }

        void CarFrameTouchingGround()
        {
        if (!carFrame.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isCarFrameTouchingGround = false;
        }
        else
        {
            isCarFrameTouchingGround = true;
        }
        }

        void Pads()
        {
            if (frontTire.frontTireColliderIsTouchingBouncePad && backTire.backTireColliderIsTouchingBouncePad)
            {
                Vector3 carAppliedBouncePadForce = new Vector3 (0, bouncePadForce, 0);
                rb.AddForce(carAppliedBouncePadForce, ForceMode2D.Impulse);
            }

            if (frontTire.frontTireColliderIsTouchingZoomPad && backTire.backTireColliderIsTouchingZoomPad)
            {
                Vector3 carAppliedZoomPadForce = new Vector3 (zoomPadForce, 0, 0);
                rb.AddForce(carAppliedZoomPadForce, ForceMode2D.Impulse); 
            }
            else
            {
                return;
            }

        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (car.IsTouchingLayers(LayerMask.GetMask("Zombie", "Spike", "Deathbox")))
            {
                Die();
            }
        }
    }
