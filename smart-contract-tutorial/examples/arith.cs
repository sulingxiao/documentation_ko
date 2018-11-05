using Ont.SmartContract.Framework;
using Ont.SmartContract.Framework.Services.Ont;
using Ont.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Ont.SmartContract
{
    public class contract1 : Framework.SmartContract
    {
        public static Object Main(string operation, params object[] args)
        {
            if (operation == "Add")
            {
                if (args.Length != 2) return false;
                int a = (int)args[0];
                int b = (int)args[1];
                return Add(a, b);
            }
            return false;
        }

        public static int Add(int a, int b)
        {
            Runtime.Notify(a + b);
            return a + b;
        }
    }
}
