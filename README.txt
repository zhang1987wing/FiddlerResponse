Alter Fiddler ResponseBody

request.php?fn=funName&es=urlencode(param1&param2&param3...)&sign=md5Sign
sign=md5(funName+param1&param2&param3+md5Key)


            string[] requestPar = jsonht.Split(new char[5] { ',', '{', '}', '[', ']' });

            for (int i = 0 ; i < requestPar.Length; i++)
            {
                string ii = requestPar[i];

                if (ii.Contains("irv"))
                {
                    //ii.Split(new char[1] { '"' })[2] = "117";
                    ii = ii.Replace(ii.Split(new char[1] { '"' })[3], "117");
                    jsonht = jsonht.Replace(requestPar[i], ii);
                    Console.WriteLine(jsonht);

                    break;
                }
            }