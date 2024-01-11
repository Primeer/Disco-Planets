using UnityEngine;

namespace View
{
    /// <summary>
    /// Вью бросателя. Устанавливает положение бросателя и длинну прицельной линии
    /// </summary>
    public class ThrowerView : MonoBehaviour
    {
        [Tooltip("Длинна прицельной линии по-умолчанию")]
        [SerializeField] private float aimLineLength = 8f;
        [SerializeField] private SpriteRenderer aimLine;
        
        private Transform cachedTransform;
        private float defaultPosition;
        

        private void Awake()
        {
            cachedTransform = transform;
            defaultPosition = cachedTransform.position.x;
        }

        public void SetPosition(float newPosition)
        {
            var currentPosition = cachedTransform.position;
            currentPosition = new Vector3(newPosition, currentPosition.y, currentPosition.z);
            cachedTransform.position = currentPosition;
        }

        public void ResetPosition()
        {
            SetPosition(defaultPosition);
        }

        public void SetAimLine(bool isActive)
        {
            aimLine.gameObject.SetActive(isActive);

            if (isActive)
            {
                aimLine.size = new Vector2(aimLine.size.x, GetAimLineLength());
            }
        }
        
        private float GetAimLineLength()
        {
            Vector2 origin = aimLine.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, aimLineLength, LayerMask.GetMask("Default"));

            if (hit.collider != null && hit.distance > 0)
            {
                return hit.distance;
            }
            
            return aimLineLength;
        }
    }
}