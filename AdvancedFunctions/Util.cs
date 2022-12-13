using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AdvancedFunctions
{
    public static partial class Util
    {
        private static byte[] RollDice(int passLenght)
        {
            byte[] randomNumber = new byte[passLenght];
            byte[] password = new byte[passLenght];


            // Создайте новый экземпляр RNGCryptoServiceProvider
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

            // Заполняем массив случайными значениями
            Gen.GetBytes(randomNumber);

            for (int i = 0; i < randomNumber.Length; i++)
            {
                if ((int)randomNumber[i] > 48 && (int)randomNumber[i] < 57 ||
                    (int)randomNumber[i] > 65 && (int)randomNumber[i] < 90 ||
                    (int)randomNumber[i] > 97 && (int)randomNumber[i] < 122)
                {
                    password[i] = randomNumber[i];
                }
                else
                {
                    Gen.GetBytes(randomNumber);
                    i--;
                }
            }
            return password;
        }

        private static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private static int RandomInt(int size)
        {
            Random random = new Random();
            int result = 0;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число от 0 до 9, заполняем им разряд.
                result = (int)((result * 10) + (random.NextDouble() * 9));

                //Целое число не может начинаться с 0, если его разрядность больше 1
                if (size > 1 && result == 0)
                    result++;
            }
            return result;
        }

        public static string GeneratePassword(int size)
        {
            string generate_str = "";
            if (size % 3 == 0)
            {
                string generate_str_part = "";
                byte[] temp = RollDice(size / 3);
                for (int i = 0; i < temp.Length; i++)
                {
                    generate_str_part += (char)temp[i];
                }
                generate_str = RandomString(size / 3) + generate_str_part + RandomInt(size / 3);
            }
            else
            {
                MessageBox.Show("Размер значения не кратен 3");
            }
            return generate_str;
        }

        public static string GetCurrentIP()
        {
            string IP = "";
            string serviceURL = "http://2ip.ru/";
            string serviceTag = "BIG";
            try
            {
                WebClient wc = new WebClient();
                string requestResult = Encoding.UTF8.GetString(wc.DownloadData(serviceURL));
                if (!string.IsNullOrEmpty(requestResult))
                {
                    string[] split1 = requestResult.ToUpper().Split(new string[] { serviceTag }, new StringSplitOptions());
                    string[] split2 = split1[1].Split('<', '>');
                    IP = split2[1];
                }
            }
            catch
            {
                IP = null;
                //MessageBox.Show(ex.Message);
            }
            return IP;
        }

        /// <summary>
        /// Функция, переводящая число в представление прописью
        /// </summary>
        public static string shiclo_stroka(string h)
        {
            // массив для сравнения и присваивания результата:
            string[,] des =
            {
                {"1", "один", "одиннадцать", "десять", "сто"},
                {"2", "два", "двенадцать", "двадцать", "двести"},
                {"3", "три", "тринадцать", "тридцать", "триста"},
                {"4", "четыре", "четырнадцать", "сорок", "четыреста"},
                {"5", "пять", "пятнадцать", "пятьдесят", "пятьсот"},
                {"6", "шесть", "шестнадцать", "шестьдесят", "шестьсот"},
                {"7", "семь", "семнадцать", "семьдесят", "семьсот"},
                {"8", "восемь", "восемнадцать", "восемьдесят", "восемьсот"},
                {"9", "девять", "девятнадцать", "девяносто", "девятьсот"},
            };
            string new_h = "";
            // все цифры отделяются друг от друга кавычками:
            foreach (char c in h)
            {
                if (new_h != "")
                {
                    new_h += ",";
                }
                new_h += "" + c.ToString() + "";
            }
            string s = "";
            // создается массив с цифрами:
            string[] split = new_h.Split(new char[] { ',' });
            // теперь цифры раскидываются по переменным - единичные, десятки, сотни, тысячи:
            string one = "", two = "", three = "", four = "", five = "";
            if (split.Length >= 1)
            {
                one = split[split.Length - 1];
            }
            if (split.Length >= 2)
            {
                two = split[split.Length - 2];
            }
            if (split.Length >= 3)
            {
                three = split[split.Length - 3];
            }
            if (split.Length >= 4)
            {
                four = split[split.Length - 4];
            }
            if (split.Length >= 5)
            {
                five = split[split.Length - 5];
            }
            // присваивается значение десятитысячной, если такой разряд имеется:
            for (int i = 0; i < des.GetLength(0); i++)
            {
                if (five != "")
                {
                    if (five == des[i, 0])
                    {
                        if (five == "1" && four == des[i, 0])
                        {
                            s += des[i, 2] + " тысяч ";
                            break;
                        }
                        //else
                        //{
                        if (five != "1" && five == des[i, 0])
                        {
                            s += des[i, 3] + " ";
                            if (four == "0")
                            {
                                s += "тысяч ";
                            }
                            break;
                        }
                        if (five == "1" && four == "0")
                        {
                            s += des[0, 3] + " тысяч ";
                            break;
                        }
                        //}
                    }
                }
            }
            // присваивается значение тысячным, если такой разряд имеется:
            for (int i = 0; i < des.GetLength(0); i++)
            {
                if (four != "")
                {
                    if (four == des[i, 0] && Convert.ToInt32(four) > 4 && five != "1")
                    {
                        s += des[i, 1] + " тысяч ";
                        break;
                    }
                    else
                    {
                        if (four == des[i, 0] && four == "1" && five != "1")
                        {
                            s += "одна тысяча ";
                            break;
                        }
                        if (four == des[i, 0] && four == "2" && five != "1")
                        {
                            s += "две тысячи ";
                            break;
                        }
                        if (Convert.ToInt32(four) > 1 && Convert.ToInt32(four) < 5 && four == des[i, 0] && five != "1")
                        {
                            s += des[i, 1] + " тысячи ";
                            break;
                        }
                    }

                }
            }
            // присваивается значение сотым, если такой разряд имеется:
            for (int i = 0; i < des.GetLength(0); i++)
            {
                if (three != "")
                {
                    if (three == des[i, 0])
                    {
                        s += des[i, 4] + " ";
                    }
                }
            }
            // присваивается значение десяткам, если такой разряд имеется:
            for (int i = 0; i < des.GetLength(0); i++)
            {
                if (two != "")
                {
                    if (two == "1" && one == des[i, 0])
                    {
                        s += des[i, 2] + " ";
                    }
                    else
                    {
                        if (two != "1" && two == des[i, 0] && two != "0")
                        {
                            s += des[i, 3] + " ";
                        }
                        if (two == "1" && one == "0")
                        {
                            s += " десять ";
                            break;
                        }
                    }
                }
            }
            // присваивается значение единичным, если такой разряд имеется:
            for (int i = 0; i < des.GetLength(0); i++)
            {
                if (one != "")
                {
                    if (one == des[i, 0] && two != "1")
                    {
                        s += des[i, 1] + " ";
                        break;
                    }
                }
            }
            return s;
        }


        /// <summary>
        /// Получение строки только с числами
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDigit(string str, bool usePunctuationMarks = false)
        {
            try
            {
                string newStr = "";
                foreach (char c in str)
                {
                    if (char.IsDigit(c))
                    {
                        newStr += c;
                    }
                    else if (usePunctuationMarks)
                    {
                        if (c == '.' || c == ',' || c == '-')
                        {
                            newStr += c;
                        }
                    }
                }
                return newStr;
            }
            catch
            {
                return str;
            }
        }

        public static string GetOnlyLetter(string str)
        {
            try
            {
                string newStr = "";
                foreach (char c in str)
                {
                    if (char.IsLetter(c))
                    {
                        newStr += c;
                    }
                }
                return newStr;
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// Проверка, содержит ли строка числа
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isDigit(string str)
        {
            int digitCount = 0;
            digitCount = str.Count(s => Char.IsDigit(s) );
            if (digitCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static bool isDecimal(string str)
        {
            int digitCount = 0;
            digitCount = str.Count(s => Char.IsDigit(s) || s == '.' || s == ',');
            if (digitCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsOnlyDigit(string str)
        {
            bool result = str.Trim().All(s => Char.IsDigit(s));
            return result;
        }
    }
}
