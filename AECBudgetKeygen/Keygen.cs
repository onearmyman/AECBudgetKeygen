using System;
using System.Text;

namespace AECBudgetKeygen
{
    public class Keygen
    {
        private const string SoftName = "ZaYRBID";
        private string Identifier = "165";
        private string BaseString;
        private string Password;
        private string Text;

        public Keygen()
        {
            SystemInfo.UseBiosVersion = false;
            SystemInfo.UseBaseBoardManufacturer = false;
            SystemInfo.UseBaseBoardProduct = false;
            SystemInfo.UseBiosManufacturer = true;
            SystemInfo.UseBiosVersion = false;
            SystemInfo.UseDiskDriveSignature = false;
            SystemInfo.UsePhysicalMediaSerialNumber = false;
            SystemInfo.UseProcessorID = false;
            SystemInfo.UseVideoControllerCaption = false;
            SystemInfo.UseWindowsSerialNumber = true;
            SystemInfo.CheckVesrsionMB = false;
            SystemInfo.CheckVesrsion = false;
            BaseString = Boring(InverseByBase(SystemInfo.GetSystemInfo(SoftName), 7));
            Password = MakePassword(BaseString, Identifier);
        }

        public String getKey()
        {
            return this.Password;
        }
        
        private static string Boring(string st)
        {
            for (int i = 0; i < st.Length; i++)
            {
                int num = i * (int)Convert.ToUInt16(st[i]);
                num %= st.Length;
                char c = st[i];
                st = st.Remove(i, 1);
                st = st.Insert(num, c.ToString());
            }
            return st;
        }
        
        
        private static string InverseByBase(string st, int MoveBase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < st.Length; i += MoveBase)
            {
                int length;
                if (i + MoveBase > st.Length - 1)
                {
                    length = st.Length - i;
                }
                else
                {
                    length = MoveBase;
                }
                stringBuilder.Append(InverseString(st.Substring(i, length)));
            }
            return stringBuilder.ToString();
        }
        
        private static string InverseString(string st)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(st[i]);
            }
            return stringBuilder.ToString();
        }
        
        private static char ChangeChar(char ch, int[] EnCode)
        {
            ch = char.ToUpper(ch);
            char result;
            if (ch >= 'A' && ch <= 'H')
            {
                result = Convert.ToChar((int)Convert.ToInt16(ch) + 2 * EnCode[0]);
            }
            else if (ch >= 'I' && ch <= 'P')
            {
                result = Convert.ToChar((int)Convert.ToInt16(ch) - EnCode[2]);
            }
            else if (ch >= 'Q' && ch <= 'Z')
            {
                result = Convert.ToChar((int)Convert.ToInt16(ch) - EnCode[1]);
            }
            else if (ch >= '0' && ch <= '4')
            {
                result = Convert.ToChar((int)(Convert.ToInt16(ch) + 5));
            }
            else if (ch >= '5' && ch <= '9')
            {
                result = Convert.ToChar((int)(Convert.ToInt16(ch) - 5));
            }
            else
            {
                result = '0';
            }
            return result;
        }

        private static string MakePassword(string st, string Identifier)
        {
            if (Identifier.Length != 3)
            {
                throw new ArgumentException("Identifier must be 3 character length");
            }
            int[] array = new int[]
            {
                Convert.ToInt32(Identifier[0].ToString(), 10),
                Convert.ToInt32(Identifier[1].ToString(), 10),
                Convert.ToInt32(Identifier[2].ToString(), 10)
            };
            st = Boring(st);
            st = InverseByBase(st, array[0]);
            st = InverseByBase(st, array[1]);
            st = InverseByBase(st, array[2]);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in st)
            {
                stringBuilder.Append(ChangeChar(ch, array));
            }
            return stringBuilder.ToString();
        }
    }
}