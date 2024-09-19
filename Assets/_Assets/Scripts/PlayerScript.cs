using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour,IKitchenObjectHolder {

    public static PlayerScript Instance { get; private set;}

    public event EventHandler OnPickUp;
    //public event EventHandler OnWalk;
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private Transform HoldingPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameInput input;
    [SerializeField] private LayerMask counterLayerMask;


    private BaseCounter SelectedCounter = null;
    private float rotationSpeed = 12f;
    private bool isWalking = false;
    private KitchenObject KitchenObject;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is more than one PlayerScript in the scene");
        }
        Instance = this;
    }

    private Vector3 lastInteractDirection = Vector3.zero;

    private void Start() {
        input.OnInteractAction += Input_OnInteractAction;
        input.OnInteractAlternateAction += Input_OnInteractAlternateAction; ;
    }

    private void Input_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!GameHandler.Instance.IsPlaying()) return;
        if (SelectedCounter != null) {
            SelectedCounter.InteractAlternate(this);
        }
    }

    private void Input_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameHandler.Instance.IsPlaying()) return;
        if (SelectedCounter != null) {
            SelectedCounter.Interact(this);
        }
    }

    private void Update() {
        movementHandler();
        interactionHandler();
    }

    public bool IsWalking() {
        return isWalking;
    }
    /** 
    public bool IsWalking() {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
    }
     */

    private void interactionHandler() {
        Vector2 inputVector = input.GetMovementVectorNorm();

        Vector3 movementDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(movementDir != Vector3.zero) {
            lastInteractDirection = movementDir;
        }
        float interactDistance = 2f;

        if(Physics.Raycast(transform.position, lastInteractDirection,out RaycastHit raycastHit, interactDistance,counterLayerMask)) {
            if(raycastHit.collider.TryGetComponent(out BaseCounter Counter)) {
                if (Counter != SelectedCounter) {
                    SetSelectedCounter(Counter);
                }
            }
            else {
                SetSelectedCounter(null);   
            }
        }
        else {
            SetSelectedCounter(null);
        }
    }

    private void movementHandler() {
        Vector2 inputVector = input.GetMovementVectorNorm();

        Vector3 movementDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = movementDir != Vector3.zero;

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 0.9f;
        float playerHight = 2f;

        bool canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, movementDir, moveDistance);

        if (!canmove) {
            // try to move in x direction
            Vector3 xDirection = new Vector3(movementDir.x, 0f, 0f).normalized;
            canmove = movementDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, xDirection, moveDistance);

            if (canmove) {
                movementDir = xDirection;

            }

            else {

                Vector3 zDirection = new Vector3(0f, 0f, movementDir.z).normalized;
                canmove = movementDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, zDirection, moveDistance);

                if (canmove) {
                    movementDir = zDirection;
                }
                else {

                }
            }
        }


        if (canmove) {
            transform.position += movementDir * moveDistance;
        }


        //transform.rotation = Quaternion.LookRotation(-movementDir);
        //transform.forward = -movementDir;
        if (movementDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, -movementDir, Time.deltaTime * rotationSpeed);
        }
    }
    private void SetSelectedCounter(BaseCounter clearCounter) {
        SelectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            SelectedCounter = SelectedCounter
        });
    }

    public Transform GetHoldingPoint() {
        return HoldingPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.KitchenObject = kitchenObject;
        if(kitchenObject != null) {
            OnPickUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return KitchenObject;
    }

    public void ClearKitchenObject() {
        KitchenObject = null;
    }

    public bool HasKitchenObject() {
        return KitchenObject != null;
    }

}
