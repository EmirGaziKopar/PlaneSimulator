using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDenemeForce : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�lerleme vect�r�n�n y�n�n� objeye g�re almak istedi�inde kullanman gerekenler

        //transform.Translate(transform.forward*Time.deltaTime); //transform.forward bir vectordur (0,0,1)
        //transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        //rb.AddRelativeForce(new Vector3(0, 0, 1));
        //rb.velocity = transform.forward;

        //ilerleme vect�r�n� y�n�n� world'e g�re almak istedi�inde kullanman gerekenler

        //rb.angularVelocity = new Vector3(0, 0, 1);
        //rb.velocity = new Vector3(0, 0, 1); //Burada y�n cisimden ba��ms�zd�r. Sahnedeki x,y,z temellerine g�re cisme hep ayn� do�rultuda kuvvet uygulan�r cismin ne tarafa bakt��� farketmez.
        //rb.AddForce(new Vector3(0, 0, 0.1f));
        //transform.position += new Vector3(0, 0, 1f);
    }
}
