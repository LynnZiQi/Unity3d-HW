using UnityEngine;
using System.Collections;

public class ParticleSea : MonoBehaviour
{

    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    private myParticle[] particlesAtr;

    public int seaResolution;
    public static float MaxRadius = 100f;
    public static float MinRadius = 50f;

    public float radius = 100.0f;
    public Gradient colorGradient;

    void Start()
    {
        particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
        particlesAtr = new myParticle[seaResolution * seaResolution];
        particleSystem.Emit(seaResolution * seaResolution);
        particleSystem.GetParticles(particlesArray);
        Init();

    }
    void Update()
    {
        ChangePosition();
        ChangeColor();
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }

    void Init()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                particlesAtr[i * seaResolution + j] = new myParticle(radius);
                particlesArray[i * seaResolution + j].position = particlesAtr[i * seaResolution + j].GetPosition();
            }
        }
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }

    void ChangePosition()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                particlesAtr[i * seaResolution + j].Rotate();
                particlesArray[i * seaResolution + j].position = particlesAtr[i * seaResolution + j].GetPosition();
            }
        }
    }
    void ChangeColor()
    {
        float colorValue;
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                colorValue = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
                colorValue += particlesAtr[i * seaResolution + j].angle / 2 / Mathf.PI;
                while (colorValue > 1)
                    colorValue--;

                //Debug.Log(colorValue);
                particlesArray[i * seaResolution + j].startColor = colorGradient.Evaluate(colorValue);

            }
        }
    }
}
