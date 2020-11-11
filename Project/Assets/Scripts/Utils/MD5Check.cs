using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class MD5Check : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string path = @"C:\Users\v_cqqcchen\Desktop\Test.xlsx";
            FileStream stream = new FileStream(path, FileMode.Open);
            MD5 mD = new MD5CryptoServiceProvider();

            byte[] datas = mD.ComputeHash(stream);
            stream.Close();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var data in datas)
            {
                stringBuilder.Append(data.ToString("x2"));
            }

            print(stringBuilder.ToString());
        }
    }
}
