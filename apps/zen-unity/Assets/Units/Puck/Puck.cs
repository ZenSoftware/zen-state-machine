using UnityEngine;

namespace Zen
{
    public class Puck : Unit
    {
        public float Speed = 1f;
        public int BaseDamage = 25;

        private Rigidbody _body;
        private Vector3 _currentDirection;
        private float _heightOffset;
        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();

            _body = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
            _heightOffset = _body.transform.localScale.y / 2;

            Vector2 randDirection = Random.insideUnitCircle.normalized;
            _currentDirection = new Vector3(randDirection.x, 0, randDirection.y).normalized;
            _body.velocity = Speed * _currentDirection;
        }

        private void FixedUpdate()
        {
            Vector3 lockHeight = _body.transform.position;
            lockHeight.y = _heightOffset;
            _body.transform.position = lockHeight;

            _body.velocity = Speed * _currentDirection;

            Debug.DrawLine(_body.transform.position, _body.transform.position + 5 * _currentDirection, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_audioSource.resource != null && collision.impulse.magnitude != 0)
            {
                float collisionVolume = Mathf.Clamp(collision.impulse.magnitude / 40, 0, 0.5f);
                //Debug.Log("Volume " + collisionVolume);
                _audioSource.pitch = 1;
                _audioSource.pitch += Mathf.Clamp(collision.impulse.magnitude / 500, 0, 0.2f);
                _audioSource.PlayOneShot(_audioSource.clip, collisionVolume);
            }

            IDamageable damagable = collision.collider.GetComponent<IDamageable>();
            if (damagable != null)
            {
                int damage = (int)collision.impulse.magnitude;
                damage += BaseDamage;
                //Debug.Log("Damage " + damage);
                damagable.Damage(new() { Origin = this, Value = damage });
            }

            _currentDirection = Vector3.Reflect(_currentDirection, collision.contacts[0].normal);
            _currentDirection.y = 0;
            _currentDirection = _currentDirection.normalized;
        }
    }
}