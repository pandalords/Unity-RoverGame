    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;
    using System.Threading.Tasks;

    public class MotorController : MonoBehaviour
    {
        
        [SerializeField] float carSpeed = 2000f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float regularTimeRotationMultiplier = 200f;
        [SerializeField] float slowMotionRotationMultiplier = 20f;
        [SerializeField] float flipBoost = 20f;
        [SerializeField] float bouncePadForce = 3f;
        [SerializeField] float zoomPadForce = 3f;
        [SerializeField] float timeSpeedReduction = 0.3f;
        [SerializeField] float timeSpeedIncrease = 5f;
        [SerializeField] float noClipSpeed = 25f;
        [SerializeField] float flipDelay = 1f;
        [SerializeField] float flipAllowance = 0.3f;
        public bool isInAir = false;
        public bool isInSlowMo = false;
        public bool isInAntiGrav = false;
        public bool isInSpeed = false;
        public bool testingMode = false;
        public bool levelFinished = false;
        public bool hasActivatedACheckpoint = false;
        public bool hasDied = false;


        public WheelJoint2D backWheel;
        public WheelJoint2D frontWheel;
        public Rigidbody2D rb;
        public BackTireOffGround backTire;
        public FrontTireOffGround frontTire;
        public BoxCollider2D carFrame;
        public CapsuleCollider2D car;
        public Rigidbody2D carPrefab; 

        CheckPointManager cPM;

        Vector2 moveInput;
        Vector2 torqueInput;
        Vector2 noClipInput;
        
        float previousCarRotation = 0;
        float currentCarRotation = 0;
        float totalAngleRotated;
        float carMovement = 0f;
        float carRotation = 0f;
        Vector2 carNoClip = new Vector2 (0,0);
        
        bool isCarFrameTouchingGround;
        bool hasBegunFlip = false;
        public bool hasCompletedFlip = false;
        bool doNotActivateAntiGrav = false;
        bool hasBoostedRecently = false;

        void Start()
        {
            cPM = GameObject.FindGameObjectWithTag("CPM").GetComponent<CheckPointManager>();
        }

        void Update()
        {
            Drive();
            Rotate();
            NoClip();
            CountFlips();
            Pads();
            CarIsInAir();
            CarFrameTouchingGround();
        }

        void FixedUpdate() 
        {
            UseMotor();
            RotateCar();
            UseNoClip();
        }

        void Drive()
        {
            carMovement = moveInput.x * -carSpeed;
        }

        void Rotate()
        {
            carRotation = moveInput.y * rotationSpeed;
        }

        void NoClip()
        {
            carNoClip.x = noClipInput.x * noClipSpeed;
        }

        void UseMotor()
        {
            if (carMovement == 0f || levelFinished || isInAir)
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
            if (levelFinished)
            {
                return;
            }
            else if (!isInSlowMo && carRotation != 0f)
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
        
        void UseNoClip()
        {
            rb.AddForce(new Vector2 (carNoClip.x, 0));
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
                if (theta > 300 || theta < -300)
                {
                    theta = 0;
                }
                totalAngleRotated += theta;

                if ((transform.eulerAngles.z > 170 && 190 > transform.eulerAngles.z))
                {
                    hasBegunFlip = true;
                    //Debug.Log("Flip begun");
                }
                if (totalAngleRotated > 260 && hasBegunFlip == true && hasBoostedRecently == false)
                {
                    hasCompletedFlip = true;
                    totalAngleRotated = -100;
                    hasBegunFlip = false;
                    //Debug.Log("Flip ready");
                }
                previousCarRotation = currentCarRotation;
            }
            else if (!isInAir)
            {
                Invoke("FlipAllowance", flipAllowance);
            }
        }

        void FlipAllowance()
        {
            if (!isInAir)
            {
                totalAngleRotated = 0;
                hasBegunFlip = false;
                hasCompletedFlip = false;
                hasBoostedRecently = false;
            }
        }
    

        public void Die()
        {
                if (testingMode || levelFinished)
                {
                    return;
                }
                else
                {
                Debug.Log("Dead");
                hasDied = true;
                //gameObject.active = false;
                transform.position = cPM.lastCheckPointPos;
                rb.velocity = new Vector3(0,0,0);
                transform.rotation = Quaternion.identity;
                ExitAllPickups();
                Invoke("hasNotDied", 0.1f);
                //gameObject.active = true;
                }
                //Destroy(GameObject);
                //Instantiate(carPrefab, cPM.lastCheckPointPos, Quaternion.identity);
                //Invoke("DieProcedure" , 0.3f);
                //isAlive = false;
                
                //add quaternion to delay one frame to stop car still moving when spawning

                
                
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void hasNotDied()
        {
            hasDied = false;
        }

        //void DieProcedure()
        //{
        //    transform.position = cPM.lastCheckPointPos;
        //    rb.velocity = new Vector3(0,0,0);
        //    transform.rotation = Quaternion.identity;
        //    ExitAllPickups();
        //}

        void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        void OnFlipBoost(InputValue value)
        {
            if (levelFinished)
            {
                return;
            }
            if (hasCompletedFlip && isInAir)
            {
            rb.AddForce(transform.right * flipBoost, ForceMode2D.Impulse);
            hasCompletedFlip = false;
            hasBoostedRecently = true;
            Invoke("FlipBoostDelay", flipDelay);
            }
        }

        void FlipBoostDelay()
        {
            hasBoostedRecently = false;
        }

        void OnDeveloperMode(InputValue value)
        {
            noClipInput = value.Get<Vector2>();
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

        void SlowMo()
        {
            Time.timeScale = timeSpeedReduction;
            isInSlowMo = true;
        }

        void ExitPowerUp()
        {
            Time.timeScale = 1f;
            isInSlowMo = false;
            isInSpeed = false;
        }

        void AntiGravity()
        {
           if (!(isInAntiGrav && doNotActivateAntiGrav))
           {
                rb.gravityScale = -2f;
                isInAntiGrav = true;
                doNotActivateAntiGrav = true;
                Invoke("AntiGravDelay", 0.5f);
           }
           else if (isInAntiGrav && doNotActivateAntiGrav)
           {
               rb.gravityScale = 1f;
               isInAntiGrav = false;
               doNotActivateAntiGrav = true;
               Invoke("AntiGravDelay", 0.5f);
           }
           else
           {
               return;
           }
           
        }

        void AntiGravDelay()
        {
            doNotActivateAntiGrav = false;
        }

        void SpeedUp()
        {
            Time.timeScale = timeSpeedIncrease;
            isInSpeed = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (car.IsTouchingLayers(LayerMask.GetMask("Zombie", "Spike", "Deathbox")))
            {
                Die();
            }
            else if (car.IsTouchingLayers(LayerMask.GetMask("SlowMoPickup")))
            {
                SlowMo();
                Invoke("ExitPowerUp", 1f);
            }
            else if (car.IsTouchingLayers(LayerMask.GetMask("AntiGravityPickup")))
            {
                AntiGravity();
            }
            else if (car.IsTouchingLayers(LayerMask.GetMask("SpeedPickup")))
            {
                SpeedUp();
                Invoke("ExitPowerUp", 60f);
            }
        }

        void ExitAllPickups()
        {
            ExitPowerUp();
            doNotActivateAntiGrav = false;
            isInAntiGrav = false;
            rb.gravityScale = 1.2f;
        }

    }
