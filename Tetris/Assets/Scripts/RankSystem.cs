using UnityEngine;
using TMPro;

public class RankSystem : MonoBehaviour
{
    [SerializeField]
    private int        maxRankCount = 5;   // �ִ� ��ũ ǥ�� ����
    [SerializeField]
    private GameObject textPrefab;         // ��ũ ������ ����ϴ� Text UI ������
    [SerializeField]
    private Transform  panelRankInfo;      // Text�� ��ġ�Ǵ� �θ� Panel Transform

    private RankData[] rankDataArray;      // ��ũ ������ �����ϴ� RankData Ÿ���� �迭
    private int        currentIndex = 0;

    private void Awake()
    {
        rankDataArray = new RankData[maxRankCount];
        currentIndex = maxRankCount;

        // 1. ������ ��ũ ���� �ҷ�����
        LoadRankData();
        // 2. 1����� ���ʷ� ���� ������������ ȹ���� ������ ��
        CompareRank();
        // 3. ��ũ ���� ���
        PrintRankData();
        // 4. ���ο� ��ũ ���� ����
        SaveRankData();
    }

    private void LoadRankData()
    {
        for (int i = 0; i < maxRankCount; ++i)
        {
            rankDataArray[i].score = PlayerPrefs.GetInt("RankScore" + i);
        }
    }

    private void SpawnText(string print, Color color)
    {
        // Instantiate()�� textPrefab ����ü�� �����ϰ�, clone ������ ����
        GameObject clone = Instantiate(textPrefab);
        // clone�� TextMeshProUGUI ������Ʈ ������ ���� text ������ ����
        TextMeshProUGUI text = clone.GetComponent<TextMeshProUGUI>();

        // ������ Text UI ������Ʈ�� �θ� panelRankInfo ������Ʈ�� ����
        clone.transform.SetParent(panelRankInfo);
        // �ڽ����� ��ϵǸ鼭 ũ�Ⱑ ��ȯ�� �� �ֱ� ������ ũ�⸦ 1�� ����
        clone.transform.localScale = Vector3.one;
        // Text UI�� ����� ����� ��Ʈ ���� ����
        text.text = print;
        text.color = color;
    }

    private void CompareRank()
    {
        // ���� ������������ �޼��� ����
        RankData currentData = new RankData();
        currentData.score = PlayerPrefs.GetInt("CurrentScore");

        // 1~10���� ������ ���� ������������ �޼��� ���� ��
        for (int i = 0; i < maxRankCount; ++i)
        {
            if (currentData.score > rankDataArray[i].score)
            {
                // ��ũ�� �� �� �ִ� ������ �޼������� �ݺ��� ����
                currentIndex = i;
                break;
            }
        }

        if ( currentIndex == maxRankCount ) return;

        // currentData�� ��� �Ʒ��� ������ ��ĭ�� �о ����
        for (int i=maxRankCount-1; i>0; --i)
        {
            rankDataArray[i] = rankDataArray[i - 1];

            if (currentIndex == i - 1)
            {
                break;
            }
        }

        // ���ο� ������ ��ũ�� ����ֱ�
        rankDataArray[currentIndex] = currentData;
    }

    private void PrintRankData()
    {
        Color color = Color.white;

        for (int i = 0; i < maxRankCount; ++i)
        {
            // ��� �÷����� ������ ��ũ�� ��ϵǸ� ������ ��������� ǥ��
            color = (currentIndex != i) ? Color.white : Color.yellow;

            // Text- TextMeshPro ���� �� ���ϴ� ������ ���
            SpawnText((i + 1).ToString(), color);
            SpawnText(rankDataArray[i].score.ToString(), color);
        }
    }

    private void SaveRankData()
    {
        for (int i = 0; i < maxRankCount; ++i)
        {
            PlayerPrefs.SetInt("RankScore" + i, rankDataArray[i].score);
        }
    }
}

[System.Serializable]
public struct RankData
{
    public int score;
}
