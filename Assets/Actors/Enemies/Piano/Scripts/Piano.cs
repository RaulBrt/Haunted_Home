using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piano : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Note;
    public Animator anim;
    AudioManager audioManager;
    Partiture Part;
    bool notas,partitura;
    int action, health;
    [SerializeField] string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colisao");
        if (collision.gameObject.CompareTag("Attack"))
        {
            health -= 10;
            StartCoroutine(IFrames(0.2f));

        }
    }
    public Nota criaNota(int angle)
    {
        GameObject objetoNovo = Instantiate(Note, new Vector3(0, 0.56f, 0), Quaternion.identity) as GameObject;
        Nota nota = objetoNovo.GetComponent<Nota>();
        nota.setAngle(angle);
        return nota;
    }

    public int getHealth()
    {
        return health;
    }
    IEnumerator IFrames(float time)
    {
        Debug.Log("Ouch");
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<PolygonCollider2D>().enabled = true;
    }


    IEnumerator Partitura()
    {
        int modif;
        partitura = true;
        modif = Random.Range(1, 3);
        if(modif >= 2)
        {
            modif = -1;
        }
        Part.mod = modif;
        Part.GetComponent<Transform>().position = new Vector3(0,0.56f,0);
        yield return new WaitForSeconds(5);
        Part.GetComponent<Transform>().position = new Vector3(-100,-100,0);
        yield return new WaitForSeconds(2);
        partitura = false;
    }

    IEnumerator chuvaDeNotas()
    {
        int mod = UnityEngine.Random.Range(1,3);
        if(mod == 3)
        {
            Debug.Log(3);
        }
        if(mod > 1)
        {
            mod = -1;
        }
        Debug.Log(mod);
        notas = true;
        int angulo = 0;
        for (int j = 0; j < 250; j++) {
            yield return new WaitForSeconds(0.06f);
            criaNota(angulo);
            angulo += mod*37;
            //angulo += mod * Random.Range(1, 360);

        }
        yield return new WaitForSeconds(2.5f);
        notas = false;
    }

    void Start()
    {
        Part = GameObject.FindObjectOfType<Partiture>();
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        partitura = false;
        notas = false;
        action = 0;
        health = 500;
    }

    // Update is called once per frame
    void Update()
    {
        action = Random.Range(0,1001);
        if (action >= 999 && !notas && !partitura)
        {
            StartCoroutine(chuvaDeNotas());
        }
        else if(action >= 997 && action <= 998 && !notas && !partitura)
        {
            StartCoroutine(Partitura());
        }

        if(notas || partitura)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
        if (health <= 0)
        {
            //PlayerStats.setDefeated(1, true);
            gameObject.SetActive(false);
            SceneManager.LoadScene(levelName);
        }
    }
}
