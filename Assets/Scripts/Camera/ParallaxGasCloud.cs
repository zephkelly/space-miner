using UnityEngine;
using System.Collections;

namespace zephkelly
{
  public class ParallaxGasCloud : MonoBehaviour {

    private ParticleSystem startfieldParticleSystem;
    private ParticleSystem.Particle[] stars;
    private Transform cameraTransform;

    [SerializeField] Transform parallaxLayer;
    [SerializeField] int starsMax = 100;
    [SerializeField] float starSizeMin = 0.15f;
    [SerializeField] float starSizeMax = 0.4f;
    [SerializeField] float starSpawnRadius = 100;
    [SerializeField] float parallaxFactor = 0.9f;

    private Vector2 lastPosition = Vector2.zero;

    private float starDistanceSqr;
    private float starClipDistanceSqr;
    [SerializeField] private int particleZ = 2;
    
    //----------------------------------------------------------------------------------------------

    private void Awake()
    {
      cameraTransform = Camera.main.transform;
      starDistanceSqr = starSpawnRadius * starSpawnRadius;
      startfieldParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Start () 
    {
      CreateCloud();
    }

    private void CreateCloud()
    {
      stars = new ParticleSystem.Particle[starsMax];

      for (int i = 0; i < starsMax; i++)
      {
        stars[i].position = lastPosition + (Vector2)(((Vector3)Random.insideUnitCircle * starSpawnRadius) + cameraTransform.position) / 2;
        stars[i].position = new Vector3(stars[i].position.x, stars[i].position.y, particleZ);
        stars[i].rotation3D = new Vector3(0, 0, Random.Range(0, 360));
        //orange startColor
        stars[i].startColor = new Color(1, 1, 1, Random.Range(0.05f, 0.2f));
        stars[i].startSize = Random.Range(starSizeMin, starSizeMax);

        lastPosition = stars[i].position;
      }

      startfieldParticleSystem.SetParticles(stars, stars.Length);
    }
 
    private void Update() 
    {
      Vector3 cameraParallaxDelta = (Vector2)(cameraTransform.position - parallaxLayer.position);

      for (int i = 0; i < starsMax; i++)
      {
        Vector2 starPosition = (Vector2)(stars[i].position + parallaxLayer.position);

        if((starPosition - (Vector2)cameraTransform.position).sqrMagnitude > starDistanceSqr) 
        {
          stars[i].position = (Vector3)(Random.insideUnitCircle.normalized * starSpawnRadius) + cameraParallaxDelta;
        }
      }
    }

    public void Parallax(Vector3 cameraLastPosition)   //called on camera controller
    {
      Vector3 cameraDelta = (Vector2)(cameraTransform.position - cameraLastPosition); 

      parallaxLayer.position = Vector3.Lerp(
        parallaxLayer.position, 
        parallaxLayer.position + cameraDelta, 
        parallaxFactor * Time.deltaTime
      );

      startfieldParticleSystem.SetParticles(stars, stars.Length);
    }
  }
}
