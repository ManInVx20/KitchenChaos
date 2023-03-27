using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPickedSomething;

    public static Player LocalInstance { get; private set; }

    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }


    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickedSomething;

    private const float MIN_MOVEMENT_THRESHOLD = 0.5f;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 7.0f;
    [SerializeField]
    private float turnSpeed = 10.0f;

    [Header("Interaction")]
    [SerializeField]
    private float interactDistance = 2.0f;
    [SerializeField]
    private LayerMask countersLayerMask;
    [SerializeField]
    private LayerMask collisionsLayerMask;
    [SerializeField]
    private Transform kitchenObjectHoldPoint;

    [Header("Spawning")]
    [SerializeField]
    private Vector3[] spawnPostionArray;

    private bool isWalking;
    private Vector3 lastInteractionDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        HandleMovement();
        HandleInteractions();
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        transform.position = spawnPostionArray[(int)OwnerClientId];
        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectHoldPoint()
    {
        return kitchenObjectHoldPoint;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = GetMoveDirection();

        isWalking = moveDirection.magnitude > MIN_MOVEMENT_THRESHOLD;

        if (isWalking)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, turnSpeed * Time.deltaTime);
        }

        bool canMove = CheckMovement(moveDirection);

        if (!canMove)
        {
            // Check horizontal movement
            Vector3 moveHorizontalDirection = new Vector3(moveDirection.x, 0.0f, 0.0f);
            canMove = moveHorizontalDirection.magnitude > MIN_MOVEMENT_THRESHOLD && CheckMovement(moveHorizontalDirection);

            if (canMove)
            {
                moveDirection = moveHorizontalDirection;
            }
            else
            {
                // Check vertical movement
                Vector3 moveVericalDirection = new Vector3(0.0f, 0.0f, moveDirection.z);
                canMove = moveVericalDirection.magnitude > MIN_MOVEMENT_THRESHOLD && CheckMovement(moveVericalDirection);

                if (canMove)
                {
                    moveDirection = moveVericalDirection;
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void HandleInteractions()
    {
        Vector3 moveDirection = GetMoveDirection();

        if (moveDirection != Vector3.zero)
        {
            lastInteractionDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractionDirection, out RaycastHit hit, interactDistance, countersLayerMask))
        {
            if (hit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private Vector3 GetMoveDirection()
    {
        Vector2 moveInput = GameInput.Instance.GetMoveInput();
        moveInput.Normalize();

        return new Vector3(moveInput.x, 0.0f, moveInput.y);
    }

    private bool CheckMovement(Vector3 direction)
    {
        Vector3 center = transform.position;
        float radius = 0.7f;
        Vector3 point2 = Vector3.one * radius;
        float maxDistance = moveSpeed * Time.deltaTime;

        return !Physics.BoxCast(center, point2, direction, Quaternion.identity, maxDistance, collisionsLayerMask);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = selectedCounter
        });
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId && HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs args)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        selectedCounter?.Interact(this);
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs args)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        selectedCounter?.InteractAlternate(this);
    }
}
