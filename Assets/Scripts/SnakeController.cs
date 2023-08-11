using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;

    public GameObject BodyPrefab;
    public Transform ParentObject;

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    [SerializeField] private UIManager uiManager;
    [SerializeField] private EggSpawner eggSpawner;
    private int EggsCollected = 0;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    void Update() 
    {
        if (isGameOver)
            return;

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        float steerDirection = Input.GetAxis("Horizontal");
        float steerDirection2 = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * steerDirection2 * SteerSpeed * Time.deltaTime);

        PositionsHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);

            index++;
        }
    }

    private void GrowSnake(GameObject fruit) 
    {
        GameObject body = Instantiate(BodyPrefab, fruit.transform.position,Quaternion.identity, ParentObject);
        BodyParts.Add(body);
        Destroy(fruit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver)
            return;

        if(other.gameObject.tag == "Egg")
        {
            GrowSnake(other.gameObject);
            uiManager.IncreasePoint();
            EggsCollected += 1;
            if (EggsCollected == eggSpawner.NoOfGoodEggs)
            {
                uiManager.ActivateGameOverUI();
                isGameOver = true;
            }
        }
        if(other.gameObject.tag == "BadEgg")
        {

            Destroy(other.gameObject);
            for(int i =0;i<2;i++)
            {
                if (BodyParts.Count < 2)
                {
                    isGameOver = true;
                    uiManager.ActivateGameOverUI();
                    return;
                }

                var part = BodyParts[BodyParts.Count - 1];
                BodyParts.Remove(part);
                Destroy(part);
            }
            uiManager.DecreasePoint();
        }

        if(other.gameObject.tag == "Wall")
        {
            isGameOver = true;
            uiManager.ActivateGameOverUI();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver)
            return;

        if(collision.gameObject.tag == "Wall")
        {
            isGameOver = true;
            uiManager.ActivateGameOverUI();
        }
    }
}