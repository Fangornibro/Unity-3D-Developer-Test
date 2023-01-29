using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fruit : MonoBehaviour
{
    public enum FruitType { apple, orange, lemon }
    public FruitType fruitType;
    public Material outline;
    private Material defMaterial;
    private MeshRenderer mr;
    private AudioSource collectSound;
    private GlobalVariables gv;
    [HideInInspector]
    public bool isSelected = false;
    private TextPopup textPopup;
    [SerializeField]
    private TextPopup textPrefab;
    public ParticleSystem particlePrefabs;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defMaterial = mr.material;
        collectSound = GameObject.Find("CollectAudioSource").GetComponent<AudioSource>();
        gv = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
    }
    private void Update()
    {
        if (transform.position.z >= 6.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        mr.material = outline;
        isSelected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mr.material = defMaterial;
        isSelected = false;
    }

    public void Collect()
    {
        collectSound.pitch = Random.Range(0.9f, 1.1f);
        collectSound.Play();
        CreateText("+1");
        ParticleSystem particle = Instantiate(particlePrefabs, transform.position, Quaternion.identity);
        particle.startColor = defMaterial.color;
        particle.Play();
        gv.AddFruit(fruitType);
        Destroy(gameObject);
    }

    private void CreateText(string text)
    {
        textPopup = Instantiate(textPrefab, new Vector3(transform.position.x + transform.localScale.x, transform.position.y + transform.localScale.y, transform.position.z + Random.Range(0, transform.localScale.z)), Quaternion.Euler(new Vector3(0, 90, 0)));
        TextMeshPro textMesh = textPopup.GetComponent<TextMeshPro>();
        textMesh.SetText(text);
        textPopup.textColor = defMaterial.color;
    }

    public void KinematicActivation()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
