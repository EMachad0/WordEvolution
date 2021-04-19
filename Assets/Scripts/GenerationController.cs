using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class GenerationController : MonoBehaviour
{
    public bool load;
    public bool running = true;
    public string target;
    public int pop;
    public double mutationRate;
    public int framesPerGen;
    public int generation = 0;
    public int counter;
    public int fps;

    public GameObject world;
    public GameObject creature;
    public TextMeshProUGUI bestCreatureText;
    public TextMeshProUGUI genText;

    private static GenerationController _instance;
    public static GenerationController Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this) throw new Exception("Multiple Singleton");
        _instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = fps;
        
        if (load) LoadPrefs();
        
        for (var i = 0; i < pop; i++)
        {
            var c = Instantiate(creature, world.transform);
            c.GetComponent<Creature>().InitRandom(target.Length);
        }
        UpdateTexts();
    }

    private void LoadPrefs()
    {
        target = PlayerPrefs.GetString("Target");
        mutationRate = PlayerPrefs.GetFloat("MutationRate");
        framesPerGen = fps / PlayerPrefs.GetInt("Speed");
        pop = PlayerPrefs.GetInt("PopSize");
    }

    private void Update()
    {
        counter = (counter + 1) % framesPerGen;
        if (counter == 0 && running) NextGen();
    }

    public void NextGen()
    {
        var creatures = new Creature[pop];
        var fit = new long[pop];

        for (var i = 0; i < pop; i++)
        {
            creatures[i] = world.transform.GetChild(0).GetComponent<Creature>();
            world.transform.GetChild(0).SetParent(gameObject.transform);
            fit[i] = Utils.StringEquality(creatures[i].Word, target);
            fit[i] = (long) Math.Pow(fit[i], 4);
        }
        
        for (var i = 1; i < pop; i++) fit[i] += fit[i - 1];

        var up = fit[fit.Length - 1];
        for (var i = 0; i < pop; i++)
        {
            var p1 = Utils.BinarySearch(ref fit, (long) Random.Range(0, up));
            var p2 = Utils.BinarySearch(ref fit, (long) Random.Range(0, up));
            if (p1 < 0 || p2 < 0 || p1 >= pop || p2 >= pop) Debug.Log(up + " " + p1 + " " + p2);
            var filho = Reproduce(creatures[p1], creatures[p2]);
        }

        for (var i = 0; i < pop; i++) Destroy(gameObject.transform.GetChild(i).gameObject);
        
        UpdateTexts();
        CheckDone();
    }

    private void CheckDone()
    {
        if (bestCreatureText.text == target) running = false;
    }

    private GameObject Reproduce(Creature a, Creature b)
    {
        var aux = "";
        for (var i = 0; i < target.Length; i++)
        {
            if (Random.value <= mutationRate) aux += Utils.GetRandomLetter();
            else if (i % 2 == 0) aux += a.Word[i];
            else aux += b.Word[i];
        }

        var filho = Instantiate(creature, world.transform);
        filho.GetComponent<Creature>().Word = aux;
        return filho;
    }

    private void UpdateTexts()
    {
        genText.text = (generation++).ToString();

        var ma = 0;
        for (var i = 0; i < pop; i++)
        {
            var c = world.transform.GetChild(i).GetComponent<Creature>();
            var fit = Utils.StringEquality(c.Word, target);
            if (fit <= ma) continue;
            ma = fit;
            bestCreatureText.text = c.Word;
        }
    }
}