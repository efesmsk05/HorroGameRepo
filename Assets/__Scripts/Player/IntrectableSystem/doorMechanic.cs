using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class doorMechanic  : MonoBehaviour
{
    public static doorMechanic instance;

    [SerializeField] Camera cam;
    Transform selectedDoor;
    GameObject dragPointGameobject;
    GameObject selectedObject;

    [SerializeField] float timer = 0.3f;
    float leftTimer;
    float angle;
    float closeAngle;
    float openAngle;
    float midAngle;

    bool openSound = false;
    bool closeSound = false;
    [SerializeField] float distance = 4;
    [SerializeField] float dragSpeed = 20;
    public bool isOpen;
    public bool isDragging = false;
    [SerializeField] LayerMask doorLayer, doorLayer2, doorLayer3;

    private void Awake()
    {
        instance = this;
        leftTimer = timer;
    }
    void Update()
    {
        
        //Raycast
        RaycastHit hit;
        #region door 1
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, doorLayer2)) // E'ye basarak açýlan kapý
        {
            selectedDoor = hit.collider.gameObject.transform;
            selectedObject = hit.collider.gameObject;
            if (selectedObject != null)
            {
                if (selectedDoor.transform.rotation.y <= 0.35)
                {
                    isOpen = false;
                }
                if (selectedDoor.transform.rotation.y > 0.35)
                {
                    isOpen = true;
                }
            }
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen)
                {
                    

                    HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
                    JointSpring spring = joint.spring;
                    spring.targetPosition = 90f;
                    joint.spring = spring;

                    isOpen = true;
                }
                else if (isOpen)
                {
                    

                    HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
                    JointSpring spring = joint.spring;
                    spring.targetPosition = 0f;
                    joint.spring = spring;

                    isOpen = false;
                }

            }
        }
        #endregion
        #region door 2
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, doorLayer)) //karma kapý
        {
            selectedObject = hit.collider.gameObject;
            angle = selectedObject.GetComponent<HingeJoint>().angle;

            HingeJoint joint1 = selectedObject.GetComponent<HingeJoint>();
            JointLimits limit = joint1.limits;
            openAngle = limit.max - 10f;
            closeAngle = limit.min + 3f;
            midAngle = (limit.max + limit.min) / 2;
            
            if (selectedObject != null)
            {
                if (isDragging)
                {
                    if (Mathf.Abs(angle) <= Mathf.Abs(midAngle))
                    {
                        isOpen = false;
                    }
                    else if (Mathf.Abs(angle) > Mathf.Abs(midAngle))
                    {
                        isOpen = true;
                    }
                }
                else
                {
                    if (Mathf.Abs(angle) <= Mathf.Abs(closeAngle))
                    {
                        isOpen = false;
                    }
                    else if (Mathf.Abs(angle) >= Mathf.Abs(openAngle))
                    {
                        isOpen = true;
                    }
                }

            }
            if (Input.GetKey(KeyCode.E))
            {
                selectedDoor = hit.collider.gameObject.transform;
                if (leftTimer > 0)
                {
                    leftTimer -= Time.deltaTime;
                } 
                else if (leftTimer <= 0)
                {
                    if (!isDragging)
                    {
                        if (selectedDoor != null)
                        {
                            selectedDoor.GetComponent<HingeJoint>().useSpring = false;
                            selectedDoor.GetComponent<HingeJoint>().useMotor = true;
                        }
                        isDragging = true;
                    }
                }
            }
            if (!isDragging && Input.GetKeyUp(KeyCode.E))
            {
                if (selectedDoor != null)
                {
                    selectedDoor.GetComponent<HingeJoint>().useMotor = false;
                    selectedDoor.GetComponent<HingeJoint>().useSpring = true;

                    if (!isOpen)
                    {
                        HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
                        JointSpring spring = joint.spring;
                        JointMotor motor = joint.motor;
                        JointLimits limits = joint.limits;

                        spring.targetPosition = 110f;
                        limits.bounciness = 0.4f;

                        joint.spring = spring;
                        joint.motor = motor;
                        joint.limits = limits;

                        if (!openSound)
                        {
                            StartCoroutine(doorOpen(0.4f));
                            openSound = true;
                        }
                    }
                    else if (isOpen)
                    {
                        HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
                        JointSpring spring = joint.spring;
                        JointMotor motor = joint.motor;
                        JointLimits limits = joint.limits;

                        spring.targetPosition = 0f;
                        limits.bounciness = 0f;

                        joint.spring = spring;
                        joint.motor = motor;
                        joint.limits = limits;

                        if (!closeSound)
                        {
                            StartCoroutine(doorClose(0.4f));
                            closeSound = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region drag
        if (selectedDoor != null)
        {
            HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
            JointMotor motor = joint.motor;

            //Create drag point object for reference where players mouse is pointing
            if (dragPointGameobject == null)
            {
                dragPointGameobject = new GameObject("Ray door");
                dragPointGameobject.transform.parent = selectedDoor;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            dragPointGameobject.transform.position = ray.GetPoint(Vector3.Distance(selectedDoor.position, transform.position));
            dragPointGameobject.transform.rotation = selectedDoor.transform.rotation;


            float delta = Mathf.Pow(Vector3.Distance(dragPointGameobject.transform.position, selectedDoor.position), 3);
            float speedMultiplier = dragSpeed * 1000;

            Vector3 localDragPoint = selectedDoor.InverseTransformPoint(dragPointGameobject.transform.position);
            Vector3 localHingeAxis = selectedDoor.GetComponent<HingeJoint>().axis;

            // Hinge eksenine göre dik ekseni seç
            float direction = 0f;
            if (Mathf.Abs(localHingeAxis.x) > 0.5f)
            {
                direction = localDragPoint.z;
            }
            else if (Mathf.Abs(localHingeAxis.y) > 0.5f)
            {
                direction = localDragPoint.x;
            }     
            else
            {
                direction = localDragPoint.y;
            }

            motor.targetVelocity = Mathf.Sign(direction) * delta * speedMultiplier * Time.deltaTime;
            joint.motor = motor;

            if (isDragging && Input.GetKeyUp(KeyCode.E))
            {
                leftTimer = timer;
                isDragging = false;
                selectedDoor = null;
                motor.targetVelocity = 0;
                joint.motor = motor;
                Destroy(dragPointGameobject);
            }
            else if (!isDragging && Input.GetKeyUp(KeyCode.E))
            {
                leftTimer = timer;
                selectedDoor = null;
                motor.targetVelocity = 0;
                joint.motor = motor;
                Destroy(dragPointGameobject);
            }
        }

    }
    #endregion

    IEnumerator doorClose(float x)
    {
        yield return new WaitForSecondsRealtime(x);
        selectedObject.transform.GetChild(2).GetComponent<AudioSource>().Play();

        yield return new WaitForSecondsRealtime(x + 0.1f);
        closeSound = false;
    }
    IEnumerator doorOpen(float x)
    {
        yield return new WaitForSecondsRealtime(x);
        selectedObject.transform.GetChild(1).GetComponent<AudioSource>().Play();

        yield return new WaitForSecondsRealtime(x + 0.1f);
        openSound = false;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            collision.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    void OnTriggerExit(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            collision.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}

