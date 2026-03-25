/*using UnityEngine;
using YG; // ��������� ���� namespace

public class Ads : MonoBehaviour
{
    public Hero script;

    private void Start()
    {
        script = GetComponent<Hero>();
    }

    private void OnEnable()
    {
        // ������������� �� ������� (����� ������ ��������� string)
        YG2.onRewardAdv += Rewarded;
    }

    private void OnDisable()
    {
        // ����������� ������������, ����� �������� ������ ������
        YG2.onRewardAdv -= Rewarded;
    }

    // ���������� �������
    void Rewarded(string id)
    {
        // ���� � ��� ��������� ����� ������, ����� ��������� id
        script.money += 500;
        Debug.Log("����� ������� �������!");
    }

    // ����� ��� ������ ����� ������� (��������� � ������)
    public void ShowAd()
    {
        // ����� ���� � �������� �� ��������������
        YG2.RewardedAdvShow("�������_1");
    }
}*/