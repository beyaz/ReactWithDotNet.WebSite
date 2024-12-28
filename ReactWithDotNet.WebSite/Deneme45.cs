using System.Runtime.InteropServices;
using static ReactWithDotNet.WebSite.Deneme46;

namespace ReactWithDotNet.WebSite;

class Test45
{
    
    
    
    
    
    
    static void AutomaticallyLoadType()
    {
        var instance = new InvalidProgramException("abc");
        
        var instance2 = new InvalidComObjectException("a", instance);

        if (instance2.Message =="a" && instance2.InnerException?.Message == "abc")
        {
            console.log("success");
            return;
        }
        
        console.log("fail");
    }



    public struct MyStruct
    {
        public int F0;
        public string F1;  
    }
    
    public struct MyStructGeneric<A,B>
    {
        public int F0;
        public string F1; 
        public A F2;
        public B F3;
    }
    
    public struct MyClassGeneric<A,B>
    {
        public int F0;
        public string F1; 
        public A F2;
        public B F3;
    }
   
    
    public static string Abc5()
    {
        SnakeGame.Start();
        
        ArrayIndexAccess();
        MultidimensionalArray();
        
        for (int i = 0; i < 50; i++)
        {
            if (i == 1)
            {
                console.time("ALL_TIME");
            }

            

            TupleTests();
            
            StaticFieldAccess();

            Conv_I();

            DynamicLoadTypeAndCreateInstance();

            GenericCallTest();

            AutomaticallyLoadMethod();

            BoxTest1();

            StructCreationTest();
            StructCreationGenericTest();
            GenericClassCreationTest();


            ExternalCallTest.Static_Void_Call();
            ExternalCallTest.Static_NonVoid_Call();
            LdInd();
            NullableIntTest();
            TryCatch_0();
            TryCatch_1();
            TryCatch_HandlerType();
            TryCatchFinaly_0();
            TryCatchFinaly_1();
            AutomaticallyLoadType();
            
            if (i == 1)
            {
                console.timeEnd("ALL_TIME");
            }

        }

        
        return "E N D";
    }

    static void DynamicLoadTypeAndCreateInstance()
    {
        var instance = new Deneme46.ClassA("a");
        if (instance.F1 != "a")
        {
            console.log("fail");
            return;
        }
        
        if (instance.F0 != 4)
        {
            console.log("fail");
            return;
        }
        
        console.log("success");
    }
   
   
    static void AutomaticallyLoadMethod()
    {
        var value = AddOne(35);
        
        if (value == 36)
        {
            console.log("success");
            return;
        }
        
        console.log("fail");
    }
    
   
}


class Deneme46
{
    public static void MultidimensionalArray()
    {
        var matrix = new int[3, 2];

        // v00   v01
        // v10   v11
        // v20   v21

        matrix[0, 1] = 2;
        matrix[1, 1] = 4;
        matrix[2, 1] = 5;



        if (matrix.Length != 6)
        {
            console.log("fail");
            return;
        }
        
        if (matrix[1, 1] != 4)
        {
            console.log("fail");
            return;
        }
        
        if (matrix[0,0] != 0)
        {
            console.log("fail");
            return;
        }



        console.log("success");
    }
    public static void NullableIntTest()
    {
        int? nullableInt = 5;

        if (nullableInt.HasValue == false)
        {
            console.log("fail");    
            return;
        }
        
        if (nullableInt == null)
        {
            console.log("fail");    
            return;
        }
        
        if (nullableInt != 5)
        {
            console.log("fail");    
            return;
        }
        
        console.log("success");
        
    }
    public static void GenericCallTest()
    {
        var result = GetNames<int, string>("-");
        if (result == "String-Int32")
        {
            console.log("success");
            return;
        }
        console.log("fail");

        static string GetNames<T1, T2>(string seperator)
        {
            return typeof(T2).Name + seperator + typeof(T1).Name;
        }
    }
    
    public static void StructCreationGenericTest()
    {
        var myStruct = new Test45.MyStructGeneric<int,string>
        {
            F0 = 4,
            F1 = "abc",
            F2 = 6,
            F3 = "a"
        };
        
        if (myStruct.F0 != 4)
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F1 != "abc")
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F2 != 6)
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F3 != "a")
        {
            console.log("fail");    
            return;
        }

        console.log("success");
    }

    public static void GenericClassCreationTest()
    {
        var myStruct = new Test45.MyClassGeneric<int,string>
        {
            F0 = 4,
            F1 = "abc",
            F2 = 6,
            F3 = "a"
        };
        
        if (myStruct.F0 != 4)
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F1 != "abc")
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F2 != 6)
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F3 != "a")
        {
            console.log("fail");    
            return;
        }

        console.log("success");
    }
    
   public class ClassA
   {
       public int F0;
       public string F1;

       public ClassA( string f1)
       {
           F0 = 4;
           F1 = f1;
       }
   }
    
    public static void TryCatchFinaly_0()
    {
        string value = "0";
        try
        {
            value += "1";
        }
        catch (Exception)
        {
            value += "2";
        }
        finally
        {
            value += "3";
        }

        if (value == "013")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    public   static class ExternalCallTest
    {
        public static void Static_Void_Call()
        {
            console.log("success");
        }
        
        public static void Static_NonVoid_Call()
        {
            var result = Math.max(1, 2);
            if (result ==2)
            {
                console.log("success");    
            }
            else
            {
                console.log("fail");
            }
            
        }
    }
    public static int AddOne(int value)
    {
        return value + 1;
    }
    
    public static void BoxTest1()
    {
        int a = 5;

        object obj = a;

        if (obj is not 5)
        {
            console.log("fail");
            return;
        }

        int b = (int)obj;
        if (b is not 5)
        {
            console.log("fail");
            return;
        }
        
        console.log("success");
    }
    
   public static void LdInd()
    {
        sbyte v0 = 1;
        byte v1 = 1;
        short v2 = 1;
        int v3 = 1;
        float v4 = 1;
        long v5 = 1;

        var response = refSByte(ref v0);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }
        response = refByte(ref v1);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refShort(ref v2);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refInt32(ref v3);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        response = refFloat(ref v4);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }
        response = refLong(ref v5);
        if (response == "0")
        {
            throw new Exception(nameof(LdInd));
        }

        console.log("success");
        
        return;
        
        static string refSByte(ref sbyte value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        static string refByte(ref byte value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refShort(ref short value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        
        static string refInt32(ref int value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refFloat(ref float value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
        
        static string refLong(ref long value)
        {
            if (value == 0)
            {
                return "0";
            }

            return "1";
        }
    }
   
    public static void StructCreationTest()
    {
        var myStruct = new Test45.MyStruct
        {
            F0 = 4,
            F1 = "abc"
        };
        
        if (myStruct.F1 != "abc")
        {
            console.log("fail");    
            return;
        }
        
        if (myStruct.F0 != 4)
        {
            console.log("fail");    
            return;
        }

        console.log("success");
    }
 
    public     static void TryCatch_0()
    {
        var trace = "0";
        
        try
        {
            trace += "1";

            throw new Exception("-abc-");
        }
        catch (Exception exception)
        {
            trace +=  "2";

            trace += exception.Message;
        }

        
        if (trace == "012-abc-")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    public static void TryCatch_1()
    {
        var trace = "0";
        try
        {
            trace += "1";
            
            trace += Call1455("t");
        }
        catch (Exception exception)
        {
            trace +=  "2";

            trace += exception.Message;
        }

        
        if (trace == "012-abc-")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
        
        return;

        static string Call1455(string a)
        {
            throw new Exception("-abc-");
        }
    }
    
    public static void TryCatchFinaly_1()
    {
        string value = "0";

        try
        {
            try
            {
                value += "1";
            }
            catch (Exception)
            {
                value +="2";
            }
            finally
            {
                value += "3";
            }
        }
        catch (Exception)
        {
            value += "4";
        }
        finally
        {
            value += "5";
        }

        if (value == "0135")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }
    
    public static void TryCatch_HandlerType()
    {
        Exception instance = null;
        
        var trace = "0";
        
        try
        {
            trace += "1";

            instance.Message.ToString();
            
            trace += "2";
        }
        catch (ArgumentOutOfRangeException)
        {
            trace +=  "3";
        }
        catch (ArgumentException)
        {
            trace +=  "4";
        }
        catch (NullReferenceException e)
        {
            trace +=  "5";
        }

        
        if (trace == "015")
        {
            console.log("success");
        }
        else
        {
            console.log("fail");
        }
    }

    public static void ArrayIndexAccess()
    {
        {
            var arr = new int[3];
        
            arr[2] = 9;

            if (arr.Length != 3)
            {
                console.log("fail");
            }
        
            if (arr[2] != 9)
            {
                console.log("fail");
            }
            
            if (arr[0] != 0)
            {
                console.log("fail");
            }
        }

        {
            var arr = new Object[3];
        
            arr[2] = 9;

            if (arr.Length != 3)
            {
                console.log("fail");
            }
        
            if ((int)arr[2] != 9)
            {
                console.log("fail");
            }
        }
        
        {
            var arr =  new (int A,string B)[44];
        
            arr[22] = (95, "H");

            if (arr.Length != 44)
            {
                console.log("fail");
            }
        
            if (arr[22].A != 95)
            {
                console.log("fail");
            }
            
            if (arr[22].B != "H")
            {
                console.log("fail");
            }
        }
        
        console.log("success");
    }

    public static void TupleTests()
    {
        var tuple = (X: "A", 5);

        if (tuple.X != "A")
        {
            console.log("fail");
        }
        
        console.log("success");
    }
    
    public static void StaticFieldAccess()
    {
        if (StaticFieldAccessTestClass1.A != "x0")
        {
            console.log("fail0");
        }
     
        StaticFieldAccessTestClass1.A = "x1";
        if (StaticFieldAccessTestClass1.A != "x1")
        {
            console.log("fail1");
        }
        StaticFieldAccessTestClass1.A = "x0";
        
        
        StaticFieldAccessTestClass2.A = "x";
        if (StaticFieldAccessTestClass2.B != "x0")
        {
            console.log("fail2");
        }
        
        if (StaticFieldAccessTestClass2.A != "x")
        {
            console.log("fail3");
        }
        
        console.log("success");
    }
    
    public static void Conv_I()
    {
        {
            double doubleValue = 123.45; 
            int intValue = (int)doubleValue; // This will use conv.i in MSIL

            if (intValue is not 123)
            {
                console.log("fail");
                return;
            }
        }

        {
            long longValue = 9876543210; 
            int intValue = (int)longValue;
            if (intValue != 1286608618)
            {
                console.log("fail");
                return;
            }
        }

        {
            var trace = "0";
            try
            {
                trace+="1";
                
                long longValue = 9876543210;
                int overflowIntValue = checked((int)longValue);
                
                trace+="2";
            }
            catch (OverflowException ex)
            {
                trace+="3";
            }

            if (trace is not "013")
            {
                console.log("fail");
                return;
            }
        }

        {
            var trace = "0";
            try
            {
                trace+="1";
                
                int intValue = int.MaxValue;
                intValue = checked(intValue + 1);
                
                trace+="2";
            }
            catch (OverflowException ex)
            {
                trace+="3";
            }

            if (trace is not "013")
            {
                console.log("fail");
                return;
            }
        }
      
        
        console.log("success");
    }
}

class StaticFieldAccessTestClass1
{
    public static string A = "x0";
}

class StaticFieldAccessTestClass2
{
    public static string B = "x0";
    public static string A;
}