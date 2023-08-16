using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _cannonBall;
    [SerializeField] private Transform _cannonballSpawnPoint;

    private float _cannonballSpeed = 20f;

    private void Update()
    {
        if (Vector2.Dot((Vector2)transform.position, (Vector2)_target.transform.position) != 1)      
            transform.right = -(_target.transform.position - transform.position);          
    }

    public void Fire() 
    {
        GameObject newCannonball = Instantiate
            (_cannonBall, _cannonballSpawnPoint.position, _cannonballSpawnPoint.rotation);
        newCannonball.GetComponent<Rigidbody2D>().velocity = -_cannonballSpawnPoint.right * _cannonballSpeed;
    }  
}
