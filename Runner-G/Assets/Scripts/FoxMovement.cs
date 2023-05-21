using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FoxMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    Animator _animator;
    private Vector3 _direction;
    private Rigidbody _rigidB;
    SplineFollower _splineFollower;
    private Vector3 scale_fox;
    private Vector3 scaleTo;
    private bool IsGround = false;
    void Start()
    {
        _rigidB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _splineFollower = GetComponent<SplineFollower>();
    }

    private void FixedUpdate()
    {
        FoxMove();
    }

    void FoxMove()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal"),0,0);

        transform.localPosition += _direction * speed * Time.deltaTime;

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x,-2f,2f),
            transform.localPosition.y,
            transform.localPosition.z);
        if(Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            _rigidB.AddForce(transform.up * jumpForce);
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position,-transform.up, out hit,1f))
            {
                IsGround = true;
                _animator.SetBool("CanJump", false);
            }
            else
            {
                IsGround= false;
                _animator.SetBool("CanJump", true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("barrier"))
        {
            _animator.SetBool("IsBarrier", true);
            StartCoroutine(reStart());
        }
        else if (collision.gameObject.CompareTag("flower"))
        {
            collision.gameObject.SetActive(false);
            scale_fox = transform.localScale;
            scaleTo = scale_fox * 2;
            transform.DOScale(scaleTo,1f);
            Debug.Log("Flower");
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            _splineFollower.followSpeed = 0;
        }
    }

    IEnumerator reStart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

}
