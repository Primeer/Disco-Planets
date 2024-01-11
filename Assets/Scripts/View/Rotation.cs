using UnityEngine;

namespace View
{
    /// <summary>
    /// Крутит центральный шар
    /// </summary>
    public class Rotation : MonoBehaviour
    {
        [Tooltip("Скорость вращения шара")]
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private new Rigidbody2D rigidbody;

        private void FixedUpdate()
        {
            rigidbody.MoveRotation(rigidbody.rotation + rotationSpeed * Time.fixedDeltaTime);
        }
    }
}