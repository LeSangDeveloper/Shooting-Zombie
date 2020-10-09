using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AssualtRiffleScript : MonoBehaviour
{
        Animator animator;

    bool isOutOfAmmo = false;
    bool isWalking = false;
    bool isAiming = false;
    bool isReloading = false;
    string FireButton = "Fire1";
    int currentAmmo = 6;

    public AudioSource shootAudioSource;
    public AudioSource mainAudioSource;
    [Header("Gun camera")]
    public Camera fpsCam;

    [Header("Gun attribute")]
    [SerializeField]
    float damage = 10f;
    // [SerializeField]
    //float range = 100f;
    [SerializeField]
    int ammo = 6;

    [Header("Canvas Information Gun")]
    public Canvas UIGunInformation;
    public Text ammoQuantity;
    public Text GunName;

    [Header("Particle Effect")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem SparkFlash;

    [Header("Bullet & its attributes")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject SpawnBulletPoint;
    
    [System.Serializable]
	public class SoundClips
	{
        [Header("Audio Clips")]
		public AudioClip shootSound;
		public AudioClip silencerShootSound;
		// public AudioClip takeOutSound;
		// public AudioClip holsterSound;
		public AudioClip reloadSoundOutOfAmmo;
		public AudioClip reloadSoundAmmoLeft;
		// public AudioClip aimSound;
	}
	public SoundClips soundClips;

    // Start is called before the first frame update
   void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (GameManager.Instance.gameState != GameState.Playing)
            return;

        AnimationCheck();

        if (Input.GetButtonDown(FireButton) && !isReloading)
        {
            Shoot();
        }

        if (Input.GetMouseButton(1) && !isReloading)
        {
            isAiming = true;
            fpsCam.fieldOfView = Mathf.Lerp (fpsCam.fieldOfView, 25.0f, 4f * Time.deltaTime);
            animator.SetBool("Aim", true);
        }
        else
        {
            isAiming = false;
            fpsCam.fieldOfView = Mathf.Lerp (fpsCam.fieldOfView, 40.0f, 4f * Time.deltaTime);
            animator.SetBool("Aim", false);
        }

		//Walking when pressing down WASD keys
		if (Input.GetKey (KeyCode.W) || 
			Input.GetKey (KeyCode.A) || 
			Input.GetKey (KeyCode.S) || 
			Input.GetKey (KeyCode.D)) 
		{
            isWalking  = true;
			animator.SetBool ("Walk", true);
		} else {
			animator.SetBool ("Walk", false);
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            Reload();

        }

    }

    void Shoot()
    {

        if (!isOutOfAmmo)
        {
            shootAudioSource.clip = soundClips.shootSound;
            shootAudioSource.Play();
            currentAmmo--;
            ammoQuantity.text = currentAmmo.ToString();
            GameObject bullet = Instantiate(bulletPrefab, SpawnBulletPoint.transform.position, SpawnBulletPoint.transform.rotation);
            bullet.transform.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 1000;
            // RaycastHit hit;

            // if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            // {
            //     Debug.Log(hit.transform.name);
            // }

            

            if (!isAiming)
            {
                animator.Play ("Fire", 0, 0f);
            }

            else
            {
                animator.Play ("Aim Fire", 0, 0f);
            }

            if (!isWalking)
            {
                muzzleFlash.Emit(10);
                Debug.Log("Mezzle");
                SparkFlash.Emit(Random.Range(1, 6));
            }
        }

        if (currentAmmo <= 0)
            isOutOfAmmo = true;
    }

    void Reload()
    {
        if (isOutOfAmmo)
        {
            animator.Play ("Reload Out Of Ammo", 0, 0f);
            mainAudioSource.clip = soundClips.reloadSoundOutOfAmmo;
            mainAudioSource.Play();
        }

        else 
		{
			//Play diff anim if ammo left
			animator.Play ("Reload Ammo Left", 0, 0f);
            mainAudioSource.clip = soundClips.reloadSoundAmmoLeft;
            mainAudioSource.Play();
        }

		currentAmmo = ammo;
        ammoQuantity.text = currentAmmo.ToString();
        isOutOfAmmo = false;
    }

    private void AnimationCheck () 
	{
		//Check if reloading
		//Check both animations
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Reload Out Of Ammo") || 
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Reload Ammo Left")) 
		{
			isReloading = true;
		} 
		else 
		{
			isReloading = false;
		}

		//Check if inspecting weapon
		// if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Inspect")) 
		// {
		// 	isInspecting = true;
		// } 
		// else 
		// {
		// 	isInspecting = false;
		// }
    }

}
