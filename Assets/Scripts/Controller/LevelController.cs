using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private AbilitiesController ab;
    private WaveSpawner ws;
    private int countAll;
    private int countBoss;
    public bool filmingBoss=false;

    public Animator bannerboss;
    public Animator bannertext;
    public GameObject BossAU;
    public GameObject spawnEffectAU;
    private GameObject _spawneffectAU;
    public bool spawningBoss=false;

    // Start is called before the first frame update
    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();
        ws = FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        countAll = ab.countN + ab.countS + ab.countZ+countBoss;
        if (countAll == 50)
        {
            countBoss += 1;
            ws.enabled = false;
            bannerboss.GetBool("spawningboss");
            bannertext.GetBool("spawningboss");
            bannerboss.SetBool("spawningboss", true);
            bannertext.SetBool("spawningboss", true);
            spawningBoss = true;
            if (spawningBoss == true)
            {
                StartCoroutine(SpawnBoss());

            }
        }
    }
    IEnumerator SpawnBoss()
    {
        filmingBoss = true;
        _spawneffectAU = Instantiate(spawnEffectAU, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        spawningBoss = false;
        Destroy(_spawneffectAU, 0.5f);
        GameObject boss= Instantiate(BossAU, transform.position, Quaternion.identity);
        boss.GetComponent<BossAU>().enabled = false;
        boss.GetComponent<AIShooting>().enabled = false;
        boss.GetComponent<HealthController>().enabled = false;
        yield return new WaitForSeconds(1f);
        filmingBoss = false;
        boss.GetComponent<BossAU>().enabled = true;
        boss.GetComponent<AIShooting>().enabled = true;
        boss.GetComponent<HealthController>().enabled = true;
        ws.Enemies.Add(boss);
        Destroy(bannerboss.gameObject);
        Destroy(bannertext.gameObject);
    }
}
