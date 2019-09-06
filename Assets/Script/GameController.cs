using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //public AudioClip RightSound;
    //public AudioClip WrongSound;
    //private AudioSource Source;
    //[SerializeField]
    //private Text score;
    [SerializeField]
    private Sprite backgroundImg;
    private List<Button> BtnList = new List<Button>();
    [SerializeField]
    public Sprite[] SourceSprite;
    public List<Sprite> GameSprite = new List<Sprite>();

    private bool firstGuess, secondGuess;
    string firstName, secondName;
    int firstIndex, secondIndex, ToTalGuess, CorrectGuess; //NOG = number of guess

    void Awake()
    {
        SourceSprite = Resources.LoadAll<Sprite>("Sprites/Images/PNG");
    }
    // Use this for initialization
    void Start()
    {
        GetButtons();
        ToTalGuess = BtnList.Count / 2; //so lan doan dung de win
        AddListener();
        AddSprite();
        Shuffle(GameSprite);
        //score.text = "Score: " + CorrectGuess.ToString();
        //Source = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

    void AddSprite()
    {
        int size = BtnList.Count;
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            if (i == size / 2)
                index = 0;
            GameSprite.Add(SourceSprite[index]);
            index++;
        }
    }

    void GetButtons()
    {
        //lay cac button hien co them vao list
        GameObject[] obj = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < obj.Length; i++)
        {
            BtnList.Add(obj[i].GetComponent<Button>());
            BtnList[i].image.sprite = backgroundImg;
        }
    }

    void AddListener()
    {
        foreach (Button btn in BtnList)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    void PickPuzzle()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (!firstGuess)
        {
            firstGuess = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstName = GameSprite[firstIndex].name;
            BtnList[firstIndex].image.sprite = GameSprite[firstIndex];
            Debug.Log("1st index: " + firstIndex + " 1st name: " + firstName);
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondName = GameSprite[secondIndex].name;
            BtnList[secondIndex].image.sprite = GameSprite[secondIndex];
            Debug.Log("2nd index: " + secondIndex + " 2nd name: " + secondName);
            StartCoroutine(CheckSamePicture());
            CheckSamePicture();
        }
    }
    IEnumerator CheckSamePicture()
    {
        yield return new WaitForSeconds(0.2f); //delay time hien. anh?
        if (firstName == secondName && firstIndex != secondIndex)
        {
            //Source.PlayOneShot(RightSound);
            CorrectGuess++;
            BtnList[firstIndex].interactable = false; //ko click dc tiep 2 lan cung 1 o
            BtnList[secondIndex].interactable = false; //nhu tren
            BtnList[firstIndex].image.color = new Color(0, 0, 0, 0);
            BtnList[secondIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfFinished();
        }
        else
        {
            //Source.PlayOneShot(WrongSound);
            BtnList[firstIndex].image.sprite = backgroundImg;
            BtnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false;
        //score.text = "Score: " + CorrectGuess.ToString();
    }

    void CheckIfFinished() //ham kiem tra win
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (CorrectGuess == ToTalGuess)
        {
            if (currentScene != "Level3")
            {
                SceneManager.LoadScene("Level" + currentScene);
            }
            else if (currentScene == "Level3")
            {
                SceneManager.LoadScene("EndGame");
            }
        }
    }

    void Shuffle(List<Sprite> list)
    {
        Sprite temp;
        for (int i = 0; i < list.Count; i++)
        {
            temp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = temp;
        }
    }
}
