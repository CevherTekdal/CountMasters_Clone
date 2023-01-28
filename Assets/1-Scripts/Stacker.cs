using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    public int remaining = 0;
    private int counter = 0;
    public List<int> stackingList = new List<int>();
    private bool isTriggered = false;
    public List<PlayerPositioner> playerPositioners = new List<PlayerPositioner>();
    [SerializeField] private GameObject playerSpawner;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject gameCam;
    [SerializeField] private GameObject finishCam;




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
            ChangeCamera();
            StackCalculator();
            StartCoroutine(StackPositioner());
        }
    }

    private void StackCalculator()
    {
        var totalPlayerCount = GameManager.Instance.PlayerCount;

        for (counter = 1; counter < 50; counter++)      //50 sayısı, 50 kattan daha yüksek stack olmaz heralde diyerek verildi.
        {
            remaining = totalPlayerCount - (int)Mathf.Pow(counter, 2) - counter;    //kalan = toplam - (n^2 + n)

            if (remaining < 0)      //yeni stack ekleyemiyor. döngüden çık
            {
                break;
            }
            stackingList.Add(counter);  //insan sayısını yeni stack olarak ekle
            stackingList.Add(counter);  //insan sayısını yeni stack olarak ekle
        }

        var previousRemaining = totalPlayerCount - ((int)Mathf.Pow(counter - 1, 2) + (counter - 1));    //bir önceki stack hesabında arta kalan player
        if (previousRemaining > 0 && previousRemaining < counter)   //arta kalan sayı 0dan büyük ve yeni eklenecek satırdaki insan sayısından küçükse
        {
            stackingList.Add(previousRemaining);    //arta kalan insan sayısını yeni stack olarak ekle.
        }
        if (previousRemaining == counter)       //arta kalan sayı yeni eklenecek satırdaki insan sayısına eşitse
        {
            stackingList.Add(counter);          //insan sayısını yeni stack olarak ekle
        }
        else if (previousRemaining > counter)   //arta kalan sayı yeni eklenecek satırdaki insan sayısından büyükse
        {
            stackingList.Add(counter);          //insan sayısını yeni stack olarak ekle
            stackingList.Add(previousRemaining - counter);      //yeni stack eklediği halde arta kalan insan sayısını yeni stack olarak ekle.
        }
        stackingList.Sort();    //listeyi stacklerdeki insan sayılarına göre küçükten büyüğe sırala
    }


    IEnumerator StackPositioner()
    {
        GetAllPlayerPositioners();

        for (int i = (stackingList.Count - 1); i >= 0; i--)     //(stackingList.Count - 1) = en kalabalık stack
        {
            var yPos = ((stackingList.Count - 1) - i) * 0.85f;     //Sol taraftaki formül 0'dan başlayıp artan şekilde player boyuyla çarpıyor.

            //bu stackteki insanları yan yana sıralayabilmek için ikinci for döngüsüne ihtiyaç bulunmakta.
            for (int j = 0; j < stackingList[i]; j++)
            {
                var mostLeftPos = -GetFloorLength(i) / 2;       //stackte kendisine ayrılan en sol noktayı buldurma
                float xPos;
                if (stackingList[i] > 1)        //stackte 1'den fazla insan varsa pozisyonlarını ayarla
                {
                    var interval = GetFloorLength(i) / (stackingList[i] - 1);       //ilgili stackteki (insan sayısı - 1) kadar aralık olur
                    xPos = mostLeftPos + interval * j;
                }
                else xPos = 0;          //stackte 1 insan varsa zaten tam ortada duracak.

                Vector3 newPosition = new Vector3(xPos, yPos, 0);
                playerPositioners[j].PlayerStackPositioner(newPosition);

                playerPositioners.Remove(playerPositioners[j]);     //pozisyonlanması tamamlanan player'ı listeden çıkar.

                yield return new WaitForSeconds(0.005f);
            }
        }

        PreparetoLadderKinematic();
        StartCoroutine("LevelFinished");
    }

    private void GetAllPlayerPositioners()
    {
        //içinde PlayerPositioner scripti olan tüm ögeleri bulup listeye ekliyoruz.
        var playerPosScripts = playerSpawner.GetComponentsInChildren<PlayerPositioner>();
        foreach (PlayerPositioner tr in playerPosScripts)
        {
            playerPositioners.Add(tr);
        }
    }

    private float GetFloorLength(int index)
    {
        var floorNominalSize = floor.GetComponent<BoxCollider>().bounds.size.x;
        var floorEditedSize = floorNominalSize * stackingList[index] * 0.04f;   //stackteki insan sayısı azaldıkça birbirlerine yaklaşacaklar.
        return floorEditedSize;
    }


    private void PreparetoLadderKinematic()
    {
        var playerObj = playerSpawner.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in playerObj)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = false;
            rb.isKinematic = false;

        }
    }

    IEnumerator LevelFinished()
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.RestartGame();
    }

    private void ChangeCamera()
    {
        gameCam.GetComponent<CameraFollow>().enabled = false;
        gameCam.GetComponent<ChangeCameraTarget>().isSwitching = true;
    }


}
