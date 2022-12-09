using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Player : MonoBehaviour, IDamageable
{
    public Transform Camera;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float deathForce = 1000;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private int hp;
    private Rigidbody rb;
    private Animator animator;
    private float vert = 0;
    private float hor = 0;
    private Vector3 moveDirection;
    private Collider mainCollider;
    
    public bool isAttacked { get; set; }
    public bool isDead{ get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        mainCollider = GetComponent<Collider>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.isTrigger) { continue; }
            col.enabled = false;
        }
        mainCollider.enabled = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || isAttacked) { return; }
        
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        Vector3 forwardDirection = Vector3.Scale(new Vector3(1, 0, 1), Camera.transform.forward);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, forwardDirection.normalized);
        moveDirection = forwardDirection.normalized * vert + rightDirection.normalized * hor;
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection.normalized, Vector3.up), rotationSpeed * Time.deltaTime);
        }
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        //bool isMoving = hor != 0 || vert != 0;
        
        //ChangeAnimation(isMoving ? "Walk Crouch" : "Crouch Idle", isMoving ? 0.05f : 0.15f);
    }

    public void TakeDamage(Transform attacker, int damage)
    {
        animator.enabled = false;

        hp -= damage;
        
        if (hp < 0)
        {
            //Dying
            var cols = GetComponentsInChildren<Collider>();
            foreach (Collider col in cols)
            {
                col.enabled = true;
            }
            mainCollider.enabled = false;
        
            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rigidBodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(Vector3.Scale(new Vector3(1,0.5f,1),(transform.position - attacker.position).normalized * deathForce));
            }
            ragdoll.transform.SetParent(null);
            gameObject.SetActive(false);
            isDead = true;
        }
    }

    /*private void GetComponentsRecursively<T>(GameObject obj, ref List<T> components)
    {
        T component = obj.GetComponent<T>();
        if(component != null)
        {
            components.Add(component);
        }
        foreach(Transform t in obj.transform)
        {
            if(t.gameObject == obj) { continue; }
            GetComponentsRecursively<T>(t.gameObject, ref components);
        }
    }*/

    /*private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !animator.IsInTransition(0))
        {
            animator.CrossFade(animationName, fadeTime);
        }
    }*/
}
