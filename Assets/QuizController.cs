using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizController : MonoBehaviour {

    public Image currentdoc;
    public Text currentquestion;
    public Text currenta;
    public Text currentb;
    public Text currentc;
    public Text currentd;

    public GameObject wipe;

    public Sprite[] level_doc;
    public Quiz[] level_quiz;
    int index;
    bool indocs;
    bool[] correct;

    public Sprite[] CherokeeDocuments;
    public Sprite[] ChoctawDocuments;
    public Sprite[] ChickasawDocuments;
    public Sprite[] SeminoleDocuments;
    public Sprite[] CreekDocuments;

    public Quiz[] CherokeeQuizzes;
    public Quiz[] ChoctawQuizzes;
    public Quiz[] ChickasawQuizzes;
    public Quiz[] SeminoleQuizzes;
    public Quiz[] CreekQuizzes;    

	// Use this for initialization
	void Start () {
        correct = new bool[] { false, false, false, false, false };
	}

    public void NewQuizSession(int character)
    {
        Sprite[][] docs = new Sprite[][] { CreekDocuments, SeminoleDocuments, ChickasawDocuments, CherokeeDocuments, ChoctawDocuments };
        Quiz[][] quizzes = new Quiz[][] { CreekQuizzes, SeminoleQuizzes, ChickasawQuizzes, CherokeeQuizzes, ChoctawQuizzes };
        level_doc = docs[character];
        level_quiz = quizzes[character];
        correct = new bool[] { false, false, false, false, false };
    }

    void OnEnable()
    {
    }

    public void toggle()
    {
        indocs = !indocs;
        if (!indocs && correct[index]) {
            Next();
        }
    }
    
    public void Next()
    {
        index++;
        if (index > 4) {
            index = 0;
        }
        if (!indocs && correct[index]) {
            Next();
        }
    }

    public void Prev()
    {
        index--;
        if (index < 0) {
            index = 4;
        }
    }

    public void AnswerQuestion(string ans)
    {
        if (ans == level_quiz[index].correct) {
            correct[index] = true;
        }
        foreach( bool i in correct ) {
            if (!i) {
                Next();
                return;
            }
        }
        wipe.GetComponent<huh>().ItsTime();
    }

	// Update is called once per frame
	void Update () {
        currentdoc.sprite = level_doc[index];
        currentquestion.text = level_quiz[index].question;
        currenta.text = level_quiz[index].a;
        currentb.text = level_quiz[index].b;
        currentc.text = level_quiz[index].c;
        currentd.text = level_quiz[index].d;
	}
}
