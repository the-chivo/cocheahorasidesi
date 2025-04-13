using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float aceleration;
    [SerializeField] float maxAceleration;
    [SerializeField] Rigidbody rb;
    [SerializeField] float friction;
    [SerializeField] float rotationSpeed;
    [SerializeField] float handBrakeRotationSpeed;
    [SerializeField] float brakeForce;
    [SerializeField] float handBrakeForce;
    [SerializeField] float backwardsAcceleration;
    [SerializeField] float backwardsMaxAcceleration;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Light carLight1;
    [SerializeField] Light carLight2;
    [SerializeField] TrailRenderer trailRenderer;
    bool handBrakeIsOn;
    bool carIsAcelerating = false;
    bool carIsGoindDown;
    bool endGameIsOn;
    Vector3 localDirection;
    int direction;
    void Start()
    {
        gameObject.transform.position = initialPosition;
        GameEvents.EndGame.AddListener(EndGame);
        GameEvents.ResetGame.AddListener(ResetGame);
        carLight2.gameObject.SetActive(false);
        carLight1.gameObject.SetActive(false);
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            carIsAcelerating = true;
            carLight1.gameObject.SetActive(true);
            carLight2.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            carIsAcelerating = false;
            carLight1.gameObject.SetActive(false);
            carLight2.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            carIsGoindDown = true;
            carLight1.gameObject.SetActive(true);
            carLight2.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            carIsGoindDown = false;
            carLight1.gameObject.SetActive(false);
            carLight2.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            direction = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            direction = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            handBrakeIsOn = true;
            trailRenderer.emitting = true;
        }
         if (Input.GetKeyUp(KeyCode.Space))
        {
            handBrakeIsOn = false;
            trailRenderer.emitting = false;
        }

       

    }
    private void FixedUpdate()
    {
        if (endGameIsOn)
            return;

        //Friccion
        if (carIsAcelerating == false && carIsGoindDown == false)
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - friction * Time.fixedDeltaTime), rb.velocity.y, rb.velocity.z * (1 - friction * Time.fixedDeltaTime));
           
        }

        rb.AddTorque(0, direction * rotationSpeed, 0);
        if(carIsAcelerating == true)
        {
            if(rb.velocity.magnitude <= maxAceleration)
            {
                rb.velocity += transform.forward.normalized * aceleration * Time.fixedDeltaTime;
            }
        }
        if (carIsGoindDown == true)
        {
            localDirection = transform.InverseTransformDirection(rb.velocity); //Se guarda el transform de inverse en localDirection
            if(localDirection.z > 0)                                          
            {
                // Frenado
                rb.velocity = new Vector3(rb.velocity.x * (1 - brakeForce * Time.fixedDeltaTime), rb.velocity.y, rb.velocity.z * (1 - brakeForce * Time.fixedDeltaTime));
                
            }
            else
            {
                //marcha atras
                if(localDirection.z <= 0 && rb.velocity.magnitude <= backwardsMaxAcceleration)
                rb.velocity -= transform.forward.normalized * backwardsAcceleration * Time.fixedDeltaTime;
                
            }
        }

        if(handBrakeIsOn == true) // Freno de mano
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - handBrakeForce * Time.fixedDeltaTime), rb.velocity.y, rb.velocity.z * (1 - handBrakeForce * Time.fixedDeltaTime)); //Aplica freno de mano
            if( direction == 1)
            {
                rb.angularVelocity = Vector3.up * handBrakeRotationSpeed; //Aplica angularvelovity a la derecha
            }
            if( direction == -1)
            {
                rb.angularVelocity = Vector3.down * handBrakeRotationSpeed; //Aplica angularvelovity a la izquierda
            }
        }

    }
    private void EndGame()
    {
        endGameIsOn = true;
    }
    private void ResetGame()
    {
        if (endGameIsOn)
        {
            transform.position = initialPosition; 
            transform.rotation = Quaternion.Euler(0,0,0);
            endGameIsOn = false;
        }
    }
}