using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public UserPreferences preferences;
    List<Branch> branches;
    public Text scoreText;
    private int score = 0;
    [SerializeField] GameObject monkey;
    int currentMonkeys = 0;

    int monkeyLimit = 3;

    // Start is called before the first frame update
    void Start()
    {
        monkeyLimit = GetDifficulty();
        branches = FindObjectsOfType<Branch>().ToList<Branch>();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        while (true) {
            yield return new WaitForSeconds(2f);            
            List<Branch> emptyBranches = new List<Branch>();
            foreach(Branch b in branches) {
                if(b.enemyOn == null) {
                    emptyBranches.Add(b);
                }
            }
            if (emptyBranches != null && currentMonkeys < monkeyLimit) {
                Branch branch = emptyBranches[Random.Range(0, emptyBranches.Count)];
                Vector3 position = new Vector3(branch.center.position.x, branch.center.position.y /*+ monkey.transform.localScale.y+*/, branch.center.position.z);
                GameObject m = Instantiate(monkey, position, Quaternion.identity);
                branch.SetMonkey(m.GetComponent<Enemy>());
                m.GetComponent<Enemy>().enemyDeadEvent += EnemyDead;
                currentMonkeys++;
            }
        }
    }

    public void EnemyDead() {        
        scoreText.text = "Monkeys down: " + ++score;
        currentMonkeys--;
    }

    private int GetDifficulty() {
        switch (preferences.difficulty) {
            case 0: return 3; 
            case 1: return 6;
            case 2: return 10;
            default: return 3;
        }
    }
}
