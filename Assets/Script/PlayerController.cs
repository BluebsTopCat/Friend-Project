using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Class
{
    Ranged, Healer, Mage, Tank
}
public class PlayerController : MonoBehaviour
{
    public bool showsword = true;
    public bool showitem = true;
    public bool showvirtualcursor = true;
    public Class playerclass;
    public int playerclassint;
    public string[] _options   = {"Ranged", "Mage", "Healer", "Tank"};
    
    public int maxhp;
    public int currenthp;

    public int mana;
    public int maxmana;

    public int currentbullets;

    public int rage;
    
    private Rigidbody2D _player;
    
    public float speed;
    private Vector2 movement;
    
    public GameObject sword;
    public Animator swordanim;
    private bool _stabbing;
    public GameObject swordhitbox;
    
    //Gunstuff
    public ItemLib itemlist;
    private List<GunProperties> _gunarray;
    public int gun;
    public SpriteRenderer gunimg;
    
    //virtual cursor stuff
    private Vector3 _target;
    public GameObject line;
    public Transform shootpoint;
    public Transform gunrotatepoint; 
    public GameObject virtualcursor;
    
    private Vector3 _direction;
    private Vector2 _inputs;
    
    private bool _right;

    private static readonly int Isswinging = Animator.StringToHash("Isswinging");

    // Start is called before the first frame update
    void Start()
    {
        _gunarray = itemlist.gunarray;
        _player = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
        _player.velocity = movement;
        var position = this.transform.position;
        virtualcursor.transform.position = new Vector3(position.x + _inputs.x* 7f, position.y + _inputs.y* 7f, 0f);

        if (Ongp())
        {
            if (virtualcursor.transform.position.x > this.transform.position.x)
                _right = true;
            else
                _right = false;
        }
        else
        {
            Vector3 mousepoint = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
            if (mousepoint.x > this.transform.position.x)
                _right = true;
            else
                _right = false;
        }

        if (_right)
            this.transform.localScale = new Vector3(6,6,6);
        else
            this.transform.localScale = new Vector3(-6,6,6);
        
        float AngleRad;
        
        if (_right)
        {
            var transform1 = gunrotatepoint.transform.position;
            AngleRad = Mathf.Atan2(_direction.y - transform1.y, _direction.x - transform1.x);
        }
        else
        {
            var transform1 = gunrotatepoint.transform.position;
            AngleRad = Mathf.Atan2(-_direction.y + transform1.y, -_direction.x + transform1.x);
        }
        // Get Angle in Degrees
        float angleDeg = (180 / Mathf.PI) * AngleRad;
        gunrotatepoint.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        // Rotate Object
        gunimg.sprite = _gunarray[gun].sprite;
    }
    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>() * speed;
    }
    void OnInteract()
    {
        if (!_stabbing)
            StartCoroutine(Stabby());
    }
    public IEnumerator Stabby()
    {
        _stabbing = true;
        swordanim.SetBool(Isswinging, true);
        swordhitbox.SetActive(true);
        sword.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        sword.SetActive(false);
        swordanim.SetBool(Isswinging, false);
        swordhitbox.SetActive(false);
        _stabbing = false;
    }
    void OnSpecial()
    {
        Shoot();
    }
    void Shoot()
    {
        int bullets = _gunarray[gun].bullets;
        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(line);
            var position = shootpoint.position;
            bullet.transform.position = new Vector3(position.x, position.y, 0);
            bullet.transform.eulerAngles = Vector3.zero;
            Trail bulletscript = bullet.GetComponent<Trail>();
            if (Vector3.Distance(this.transform.position, virtualcursor.transform.position) > 30)
            {
                _target = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
            }
            else
            {
                _target = virtualcursor.transform.position;
            }

            bulletscript.destination = new Vector3(_target.x + Random.Range(-_gunarray[gun].spread,_gunarray[gun].spread) , _target.y + Random.Range(-_gunarray[gun].spread,_gunarray[gun].spread), 0);
            bulletscript.damage = _gunarray[gun].damage;
        }

    }
    void OnLookDir(InputValue input)
    {
        Debug.Log(Gamepad.current);
        _inputs = input.Get<Vector2>();
        if (!Ongp())
        {
            _direction = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
        }
        else
        {
            var position = this.transform.position;
            _direction = new Vector3(position.x + input.Get<Vector2>().x, position.y + input.Get<Vector2>().y, 0f);
        }
       
    }

    public bool Ongp()
    {
        if (Vector3.Distance(this.transform.position, virtualcursor.transform.position) > 30)
            return false;
        else
            return true;
    }

    
}

[CustomEditor(typeof(PlayerController), true)] public class Disp: Editor {
    override public void OnInspectorGUI()
    {
        
        var myScript = target as PlayerController;
        myScript.speed = EditorGUILayout.FloatField("Speed", myScript.speed);
        myScript.playerclassint = EditorGUILayout.Popup("Class", myScript.playerclassint, myScript._options);
        EditorGUILayout.Space();
        switch (myScript.playerclassint)
        {
            case 0:
                myScript.playerclass = Class.Ranged;
                myScript.currentbullets = EditorGUILayout.IntField("Ammo", myScript.currentbullets);
                break;
            case 1:
                myScript.playerclass = Class.Mage;
                myScript.maxmana = EditorGUILayout.IntSlider("MaxMana",myScript.maxmana, 0, 100);
                myScript.mana = EditorGUILayout.IntSlider("Mana",myScript.mana, 0, myScript.maxmana);
                break;
            case 2:
                myScript.playerclass = Class.Healer;
                myScript.maxmana = EditorGUILayout.IntSlider("MaxMana",myScript.maxmana, 0, 100);
                myScript.mana = EditorGUILayout.IntSlider("Mana",myScript.mana, 0, myScript.maxmana);
                break;
            case 3:
                myScript.playerclass = Class.Tank;
                myScript.rage = EditorGUILayout.IntSlider("Rage",myScript.maxmana, 0, 100);
                break;
        }
        EditorGUILayout.Space();
        myScript.maxhp = EditorGUILayout.IntSlider("MaxHp",myScript.maxhp, 0, 100);
        myScript.currenthp = EditorGUILayout.IntSlider("Hp",myScript.currenthp, 0, myScript.maxhp);
        
        EditorGUILayout.Space();
        
        myScript.showsword = EditorGUILayout.BeginFoldoutHeaderGroup(myScript.showsword, "Sword Stuff", null);
        if (myScript.showsword)
        {
            myScript.sword = (GameObject) EditorGUILayout.ObjectField("Sword Prefab", myScript.sword, typeof(GameObject), true);
            myScript.swordanim = (Animator) EditorGUILayout.ObjectField("Sword Animator", myScript.swordanim, typeof(Animator), true);
            myScript.swordhitbox = (GameObject) EditorGUILayout.ObjectField("Sword Hitbox", myScript.swordhitbox, typeof(GameObject), true);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        myScript.showitem = EditorGUILayout.BeginFoldoutHeaderGroup(myScript.showitem, "Item Libs/Weapons", null);
        if (myScript.showitem)
        {
            myScript.itemlist = (ItemLib) EditorGUILayout.ObjectField("Item Library", myScript.itemlist, typeof(ItemLib), true);
            myScript.gun = EditorGUILayout.IntField("Gun id ", myScript.gun);
            myScript.gunimg = (SpriteRenderer) EditorGUILayout.ObjectField("Gun Sprite", myScript.gunimg, typeof(SpriteRenderer), true);

        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        myScript.showvirtualcursor = EditorGUILayout.BeginFoldoutHeaderGroup(myScript.showvirtualcursor, "Virtual Cursor", null);
        if (myScript.showvirtualcursor)
        {
            myScript.line = (GameObject)EditorGUILayout.ObjectField("Line", myScript.line, typeof(GameObject), true);
            myScript.shootpoint = (Transform)EditorGUILayout.ObjectField("Shoot Point", myScript.shootpoint, typeof(Transform), true);
            myScript.gunrotatepoint = (Transform)EditorGUILayout.ObjectField("Gun Rotation Point", myScript.gunrotatepoint , typeof(Transform), true);
            myScript.virtualcursor = (GameObject)EditorGUILayout.ObjectField("Virtual Cursor", myScript.virtualcursor, typeof(GameObject), true);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

    }
    
}
