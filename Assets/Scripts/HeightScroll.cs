using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider heightbar;
    public Slider velocitybar;

    public GameObject centerAngleAttack;
    public GameObject targetAngleAttack;

    public GameObject centerSideAttack;
    public GameObject targetSideAttack;

    public GameObject centerAeroHor;
    public GameObject targetAeroHor;

    public GameObject centerDegCircle;
    public GameObject targetDegCircle;
    public GameObject targetSideAttackDegCircle;

    private float angleAttackInit;
    private float angleSideInit;

    private float rollInit;
    private float pitchInit;
    private float yawInit;

    void Start()
    {
        angleAttackInit = 0;
        angleSideInit = 0;
        rollInit = 0;
        pitchInit = 0;
        yawInit = 0;

        targetAngleAttack.transform.RotateAround(centerAngleAttack.transform.position, Vector3.back, 90);
        targetSideAttack.transform.RotateAround(centerSideAttack.transform.position, Vector3.back, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.positionY > heightbar.maxValue)
            heightbar.value = heightbar.maxValue;
        else
        {
            if (Data.positionY < heightbar.minValue)
                heightbar.value = heightbar.minValue;
            else
                heightbar.value = Data.positionY;
        }

        GameObject.Find("HeightBar/Mark/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = System.MathF.Round(Data.positionY, 2).ToString();

        float velocity = System.MathF.Sqrt(Data.velocityX * Data.velocityX +
            Data.velocityY * Data.velocityY + Data.velocityZ * Data.velocityZ);

        if (velocity > velocitybar.maxValue)
            velocitybar.value = velocitybar.maxValue;
        else
        {
            if (velocity < velocitybar.minValue)
                velocitybar.value = velocitybar.minValue;
            else
                velocitybar.value = velocity;
        }

        GameObject.Find("ValueBar/Mark/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = System.MathF.Round(velocity, 2).ToString();

        targetAngleAttack.transform.RotateAround(centerAngleAttack.transform.position, Vector3.back, 360 / 40 * (-Data.angleAttack + angleAttackInit) / System.MathF.PI * 180.0f);
        targetSideAttack.transform.RotateAround(centerSideAttack.transform.position, Vector3.back, 360 / 40 * (-Data.sidingAttack + angleSideInit) / System.MathF.PI * 180.0f);
        targetAeroHor.transform.RotateAround(centerAeroHor.transform.position, Vector3.back, (Data.roll - rollInit) / System.MathF.PI * 180.0f);
        targetDegCircle.transform.RotateAround(centerDegCircle.transform.position, Vector3.back, (-Data.yaw + yawInit) / System.MathF.PI * 180.0f);
        targetSideAttackDegCircle.transform.RotateAround(centerDegCircle.transform.position, Vector3.back, (-Data.sidingAttack + angleSideInit) / System.MathF.PI * 180.0f);

        if (Data.velocityX != 0 & Data.velocityY != 0)
            GameObject.Find("Pitch").transform.position += new Vector3(0, 165 / 40 * (pitchInit - Data.pitch) / System.MathF.PI * 180.0f, 0);

        angleAttackInit = Data.angleAttack;
        angleSideInit = Data.sidingAttack;
        rollInit = Data.roll;
        pitchInit = Data.pitch;
        yawInit = Data.yaw;

        GameObject.Find("SidingAtack/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = System.MathF.Round(Data.sidingAttack / System.MathF.PI * 180.0f, 2).ToString();
        GameObject.Find("AangleAtack/Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = System.MathF.Round(Data.angleAttack / System.MathF.PI * 180.0f, 2).ToString();
    }
}
