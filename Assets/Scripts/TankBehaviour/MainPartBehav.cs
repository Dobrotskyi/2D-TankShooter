using Photon.Pun;
using UnityEngine;

public class MainPartBehav : MonoBehaviourPun
{
    public float Acceleration => _data.Acceleration;
    public float RotationSpeed => _data.AngularSpeed;

    private const int ACCELERATION_MULT = 500;

    private Rigidbody2D _rb;
    private MainPartData _data;

    public void Move(float direction)
    {
        _rb.AddForce(_rb.transform.up * direction * _data.Acceleration * ACCELERATION_MULT * Time.fixedDeltaTime, ForceMode2D.Force);
        if (_rb.velocity.magnitude > _data.MaxSpeed)
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _data.MaxSpeed);
    }

    public void Rotate(float side)
    {
        _rb.AddTorque(-side * _data.AngularSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    public void SetData(MainPartData data)
    {
        if (_data == null)
            _data = data;
        else
            throw new System.Exception("Data was already set for this object");
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        ParticleSystem engineSmoke = Instantiate(FindObjectOfType<EffectsContainer>().EngineSmoke, transform);
        engineSmoke.transform.rotation = Quaternion.identity;
        engineSmoke.transform.localPosition = new Vector3(0, 0 - GetComponent<BoxCollider2D>().size.y / 2, 0);
    }
}
