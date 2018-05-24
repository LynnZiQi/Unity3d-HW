using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myParticle
{

    public float radius { get; set; }
    public float speed { get; set; }
    public float angle { get; set; }

    public myParticle(float r)
    {
        this.radius = r;
        this.speed = Random.value * Mathf.Sqrt(radius);
        this.angle = Random.value * 2 * Mathf.PI;

    }
    public Vector3 GetPosition()
    {
        return radius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    public void Rotate()
    {
        this.angle += Time.deltaTime * speed / 10;
        if (this.angle > 2 * Mathf.PI)
            this.angle -= 2 * Mathf.PI;
        this.radius += Random.value * 0.2f - 0.1f;
        if (this.radius > ParticleSea.MaxRadius)
            this.radius = ParticleSea.MaxRadius;
        if (this.radius < ParticleSea.MinRadius)
            this.radius = ParticleSea.MinRadius;
    }
}