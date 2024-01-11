using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Вьюха шара
    /// </summary>
    public class BallView : MonoBehaviour
    {
        [Tooltip("true - размер и масса шара не будут выставлены автоматически")]
        [SerializeField] private bool hasCustomValue;
        
        [Space]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private new Transform transform;

        private Vector3 initialScale;
        private float initialWeight;
        
        private Vector2 gravitationPoint;
        private float gravitationForce;
        private float dragIncreaseRatio;
        private bool hasGravitation;
        private bool inCollision;
        
        public Vector2 Position => rigidbody.position;
        public Vector2 Velocity => rigidbody.velocity;
        public bool HasCustomValue => hasCustomValue;
        public bool IsSimulated => rigidbody.simulated;
        
        public Action<BallView, BallView> BallsCollided;

        private void Awake()
        {
            initialScale = transform.localScale;
            initialWeight = rigidbody.mass;
        }

        public void OnThrow(Vector2 velocity, float dragDelta)
        {
            dragIncreaseRatio = dragDelta;
            EnableSimulation(true);
            SetVelocity(velocity);
        }
        
        public void EnableSimulation(bool isEnable)
        {
            rigidbody.simulated = isEnable;
        }
        
        public void SetGravitation(bool isEnable, Vector2 point, float force)
        {
            hasGravitation = isEnable;
            gravitationPoint = point;
            gravitationForce = force;
        }
        
        public void SetDrag(float drag)
        {
            rigidbody.drag = drag;
            rigidbody.angularDrag = drag;
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbody.velocity = velocity;
        }

        public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
        {
            rigidbody.AddForce(mode == ForceMode2D.Force ? force : force / 50, mode);
        }
        
        public void MarkAsInCollision()
        {
            inCollision = true;
        }
        
        public void SetScale(float scale)
        {
            transform.localScale = initialScale * scale;
        }

        public void SetWeight(float weight)
        {
            rigidbody.mass = initialWeight * weight;
        }

        public void SetSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        public void Reset()
        {
            EnableSimulation(false);
            SetGravitation(false, Vector2.zero, 0f);
            SetDrag(0);
            SetVelocity(Vector2.zero);
            inCollision = false;
        }
        
        private void FixedUpdate()
        {
            if (hasGravitation)
            {
                var direction = (gravitationPoint - rigidbody.position).normalized;
                rigidbody.AddForce(direction * gravitationForce);
            }
            else if(rigidbody.simulated) // Стадия свободного полета шара: гравитации нет, трение постепенно увеличивается
            {
                float dragDelta = dragIncreaseRatio * Time.fixedDeltaTime;
                rigidbody.drag += dragDelta;
                rigidbody.angularDrag += dragDelta;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (inCollision)
            {
                return;
            }
            
            if (other.gameObject.TryGetComponent(out BallView otherView))
            {
                BallsCollided?.Invoke(this, otherView);
            }
        }
    }
}