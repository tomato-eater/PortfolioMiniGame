using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerInput管理クラス
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput = null;
    private InputAction _jumpAction = null;

    private void OnValidate()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Awake()
    {
        _jumpAction = _playerInput.actions["Jump"];
    }

    /// <summary>
    /// ジャンプ入力を ? した : してない
    /// </summary>
    public bool JumpTrigger => _jumpAction.triggered;

    public bool JumpRelease => _jumpAction.WasReleasedThisFrame();
}
