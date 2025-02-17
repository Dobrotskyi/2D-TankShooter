using UnityEngine;
using System.Globalization;

public class TurretDataBuilder : ObjectFromDBBuilder
{
    private float _rotationSpeed = -1;
    private Vector2 _spread;
    private float _fireRate = -1;
    private float _shotForce = -1;
    private float _durabilityMultiplier = -1;
    private float _damageMult = -1;
    private string _name;
    private Sprite _sprite;
    private ProjectileData _projectileData;
    private int _price;

    public TurretDataBuilder() { }

    public TurretDataBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public TurretDataBuilder SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        return this;
    }

    public TurretDataBuilder SetRotationSpeed(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
        return this;
    }

    public TurretDataBuilder SetSpread(Vector2 spread)
    {
        _spread = spread;
        return this;
    }

    public TurretDataBuilder SetFireRate(float fireRate)
    {
        _fireRate = fireRate;
        return this;
    }

    public TurretDataBuilder SetShotForce(float shotForce)
    {
        _shotForce = shotForce;
        return this;
    }

    public TurretDataBuilder SetDamageMult(float damageMultiplier)
    {
        _damageMult = damageMultiplier;
        return this;
    }

    public TurretDataBuilder SetDM(float dm)
    {
        _durabilityMultiplier = dm;
        return this;
    }

    public TurretDataBuilder SetPrice(int price)
    {
        _price = price;
        return this;
    }

    public override void ParseData(string[] info)
    {
        Vector2 spread = new Vector2(float.Parse(info[3], CultureInfo.InvariantCulture), float.Parse(info[4], CultureInfo.InvariantCulture));
        SetId(int.Parse(info[0], CultureInfo.InvariantCulture));
        this.SetName(info[1]).SetRotationSpeed(float.Parse(info[2], CultureInfo.InvariantCulture))
               .SetSpread(spread).SetFireRate(float.Parse(info[5], CultureInfo.InvariantCulture))
               .SetShotForce(float.Parse(info[6], CultureInfo.InvariantCulture))
               .SetDM(float.Parse(info[7], CultureInfo.InvariantCulture))
               .SetDamageMult(float.Parse(info[8], CultureInfo.InvariantCulture))
               .SetSprite(ImageLoader.MakeSprite(info[9], new Vector2(0.5f, 0.2f)));

        _projectileData = new ProjectileData(int.Parse(info[10], CultureInfo.InvariantCulture), info[11],
            int.Parse(info[12], CultureInfo.InvariantCulture),
            float.Parse(info[13], CultureInfo.InvariantCulture),
            int.Parse(info[14], CultureInfo.InvariantCulture),
            ImageLoader.MakeSprite(info[15], new Vector2(0.5f, 0.5f)));

        if (info.Length > 16)
            SetPrice(int.Parse(info[16], CultureInfo.InvariantCulture));
    }

    protected override PartData MakePart()
    {
        return new TurretData(_id, _name, _rotationSpeed,
                              _spread, _fireRate, _shotForce,
                              _durabilityMultiplier, _damageMult, _sprite, _projectileData, _price);
    }

    protected override bool Verify()
    {
        if (_rotationSpeed == -1)
            return false;
        if (_spread == null)
            return false;
        if (_fireRate == -1)
            return false;
        if (_shotForce == -1)
            return false;
        if (_durabilityMultiplier == -1)
            return false;
        if (_name == null)
            return false;
        if (_sprite == null)
            return false;
        if (_damageMult == -1)
            return false;

        return true;
    }

}
