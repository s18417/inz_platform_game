using System;
using UnityEngine;

namespace enemy_ranged
{
    public class rangedStateManager : MonoBehaviour
    {
        
        [HideInInspector]public Rigidbody2D rb;
        [HideInInspector] public Animator anim;
        [HideInInspector]public float dir;
        [HideInInspector]public Transform tr;
        public float jumpForce;
        [HideInInspector] public GameObject player;
        public HealthSystem hp;
        public GameObject bulletPrefab;
        public Transform firePoint;
    
        rangedBaseState currentState;
        public rangedIdleState IdleState = new rangedIdleState();
        public rangedShootState ShootState = new rangedShootState();
        public rangedDodgeState DodgeState = new rangedDodgeState();
        public rangedDyingState DyingState = new rangedDyingState();
        

        
        [SerializeField] internal playerDetector _dodgeSensor;
        [SerializeField] internal playerDetector _shootSensor;
        [SerializeField] internal playerDetector _groundSensor;
        [SerializeField] internal playerDetector _wallSensor;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            dir = 1;
            tr = transform;
            currentState = IdleState;
            currentState.EnterState(this);
            hp = GetComponent<HealthSystem>();

        }

        private void Update()
        {
            currentState.UpdateState(this);
            if(hp._health<=0) SwitchState(DyingState);
        }
        
        public void SwitchState(rangedBaseState state)
        {
            currentState = state;
            state.EnterState(this);
        }

        public void Rotate()
        {
            if (dir == 1) dir = -1;
            else dir = 1;

            tr.right = new Vector2(dir,0);
        }
        public void Die()
        {
            Destroy(this.gameObject);
        }

        void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}