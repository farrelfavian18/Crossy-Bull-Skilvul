using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepText;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource dieAudio;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight = 0.5f;

    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;
    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel; }
    public bool IsDie { get => this.enabled == false; }
    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    private void Update()
    {
        var moveDir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir += new Vector3(-1, 0, 0);
        }

        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);

    }

    private void Jump(Vector3 targetDirection)
    {
        //Atur rotasi 
        Vector3 targetPosition = transform.position + targetDirection;
        transform.LookAt(targetPosition);

        //Loncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));

        //agar tidak bisa lewati Tree
        if (targetPosition.z <= backBoundary ||
            targetPosition.x <= leftBoundary ||
            targetPosition.x >= rightBoundary)

            return;


        if (Tree.AllPositions.Contains(targetPosition))
            return;

        //gerak maju/mundur/samping
        transform.DOMoveX(targetPosition.x, moveDuration);
        transform.DOMoveZ(targetPosition.z, moveDuration).OnComplete(UpdateTravel);
        jumpAudio.Play(0);
    }
    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;
        if (currentTravel > maxTravel)
            maxTravel = currentTravel;

        stepText.text = "STEP:" + maxTravel.ToString();
    }
    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false)
            return;
        //di execute sekali pada frame ketika nempel pertamakali
        var car = other.GetComponent<Car>();
        if (car != null)
        {
            AnimateCrash();
        }
        // if (other.tag == "Car")
        // {
        //     AnimateCrash();
        // }
    }

    private void AnimateCrash()
    {
        dieAudio.Play(0);
        //Gepeng
        transform.DOScaleY(0.2f, 0.2f);
        transform.DOScaleX(3, 0.2f);
        transform.DOScaleZ(2, 0.2f);
        this.enabled = false;
        dieParticles.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        //di execute setiap frame selama masih nempel
        //Debug.Log("Stay);
    }
    private void OnTriggerExit(Collider other)
    {
        //di execute sekali pada frame ketika tidak
        //Debug.log("Exit);
    }
}

