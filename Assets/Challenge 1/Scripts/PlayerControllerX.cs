using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private GameObject coinParent;
    [SerializeField] private UIController uıController;
    
    
    private void Update()
    {
        Move();
        Rotate();

        if (uıController.isTimeUp())
        {
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            UIController.WinCoin();
        }
    }


    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * moveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * moveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity = -transform.right * (rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rb.angularVelocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity = transform.right * (rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void Restart()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        
        for (int i = 0; i < coinParent.transform.childCount; i++)
        {
            coinParent.transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }

}
