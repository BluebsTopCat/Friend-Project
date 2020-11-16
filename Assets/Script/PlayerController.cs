using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Player;
    
    public float speed;
    public Vector2 movement;
    
    public GameObject sword;
    public Animator swordanim;
    private bool stabbing = false;
    public GameObject swordhitbox;
    
    //Gunstuff
    public List<GunProperties> gunarray = new List<GunProperties>();
    public int gun;
    public SpriteRenderer gunimg;
    
    //virtual cursor stuff
    private Vector3 target;
    public GameObject line;
    public Transform shootpoint;
    public GameObject virtualcursor;
    private Vector3 direction;

    private Vector2 inputs;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = this.GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Player.velocity = movement;
        virtualcursor.transform.position = new Vector3(this.transform.position.x + inputs.x* 7f, this.transform.position.y + inputs.y* 7f, 0f);
        float AngleRad = Mathf.Atan2(direction.y - this.transform.position.y, direction.x - this.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        // Rotate Object
        gunimg.sprite = gunarray[gun].sprite;
    }

    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>() * speed;
    }

    void OnInteract()
    {
        if (!stabbing)
            StartCoroutine(Stabby());
    }

    public IEnumerator Stabby()
    {
        stabbing = true;
        swordanim.SetBool("Isswinging", true);
        swordhitbox.SetActive(true);
        sword.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        sword.SetActive(false);
        swordanim.SetBool("Isswinging", false);
        swordhitbox.SetActive(false);
        stabbing = false;
    }

    void OnSpecial()
    {
        Shoot();
    }

    void Shoot()
    {
        int bullets = gunarray[gun].bullets;
        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(line);
            bullet.transform.position = new Vector3(shootpoint.position.x, shootpoint.position.y, 0);
            bullet.transform.eulerAngles = Vector3.zero;
            Trail bulletscript = bullet.GetComponent<Trail>();
            if (Vector3.Distance(this.transform.position, virtualcursor.transform.position) > 30)
            {
                target = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
            }
            else
            {
                target = virtualcursor.transform.position;
            }

            bulletscript.destination = new Vector3(target.x + Random.Range(-gunarray[gun].spread,gunarray[gun].spread) , target.y + Random.Range(-gunarray[gun].spread,gunarray[gun].spread), 0);
            bulletscript.damage = gunarray[gun].damage;
        }

    }

    void OnLookDir(InputValue input)
    {
        Debug.Log(Gamepad.current);
        inputs = input.Get<Vector2>();
        if (Vector3.Distance(this.transform.position, virtualcursor.transform.position) > 30)
        {
            direction = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
        }
        else
        {
            direction = new Vector3(this.transform.position.x + input.Get<Vector2>().x, this.transform.position.y + input.Get<Vector2>().y, 0f);
        }
       
    }

    
}
