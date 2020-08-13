using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogeha5v3
{
    class Program
    {
        const int NMAX = 5;        //maximum size of each name string
        const int LSIZE = 5;       //actual number of infix strings in the data array
        const int NOPNDS = 10;     //number of operand symbols in the operand array
        static int IDX;                   //index used to implement conversion stub
        static char[] opnd = new char[NOPNDS] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }; //operands symbols
        static double[] opndval = new double[NOPNDS] { 3, 1, 2, 5, 2, 4, -1, 3, 7, 187 };           //operand values
        static List<double> OPNDstack = new List<double>();


        static void Main()
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = Console.WindowWidth * 9 / 25;
            /*************************************************************************
                                      KEY DECLARATIONS            
            *************************************************************************/
            string[] infix = new string[LSIZE] { "C$A$E",    //array of infix strings
                             "(A+B)*(C-D)",
                             "A$B*C-D+E/F/(G+H)",
                             "((A+B)*C-(D-E))$(F+G)",
                             "A-B/(C*D$E)"  };



            /*************************************************************************
                   PRINT OUT THE OPERANDS AND THEIR VALUES            
             *************************************************************************/
            Console.WriteLine("\nOPERAND SYMBOLS USED:\n");   //title
            for (int i = 0; i < NOPNDS; i++)
                Console.Write(opnd[i].ToString().PadLeft(5));

            Console.WriteLine("\n\n\nCORRESPONDING OPERAND VALUES:\n");   //title
            for (int i = 0; i < NOPNDS; i++)
                Console.Write(opndval[i].ToString().PadLeft(5));

            Console.WriteLine("\n\n");

            /*************************************************************************
                                            OUTPUT LINES
            *************************************************************************/
            Console.WriteLine("Infix Expression".PadRight(30) + "Postfix Expression".PadRight(30) + "Value".PadRight(20));
            OutLine(70, '=');

            for (IDX = 0; IDX < LSIZE; IDX++)
            {
                string postfix = ConvertToPostfix(infix[IDX]);
                Console.WriteLine(infix[IDX].PadRight(30) + postfix.PadRight(30) + EvaluatePostfix(postfix));
            }

            Console.ReadLine();
        }



        /***************************************************************************************** 
                FUNCTION OutLine:   formatting function to print n repetitions of char ch
        ******************************************************************************************/
        static void OutLine(int n, char ch)
        {
            for (int q = 0; q < n; q++)
                Console.Write(ch.ToString());

            Console.WriteLine("\n");
        }
        /*************************************************************************
                                CONVERSION FUNCTION     
        *************************************************************************/
        static string ConvertToPostfix(string infix)
        {
            string[] postfix = new string[LSIZE] { "CAE$$",  //array of postfix strings
                             "AB+CD-*",
                             "AB$C*D-EF/GH+/+",
                             "AB+C*DE--FG+$",
                             "ABCDE$*/-"  };
            return postfix[IDX];
        }
        /*************************************************************************
                                EVALUATION FUNCTION  
        *************************************************************************/
        static double EvaluatePostfix(string postfix)
        {
            char s;
            double op1, op2, val = 0;

            foreach(char c in postfix)
            {
                s = c;

                if (isOPND(s))
                {
                    double OPND = getOPND(s);
                    OPNDpush(OPND);
                }
                else
                {
                    op2 = OPNDpop();
                    op1 = OPNDpop();
                    val = applyOPND(s, op1, op2);
                    OPNDpush(val);
                }
            }

            val = OPNDpop();

            return val;
        }

        static bool isOPND(char c)
        {
            return (c != '+' && c != '-' && c != '*' && c != '/' && c != '$');
        }

        static double applyOPND(char c, double op1, double op2)
        {
            switch (c)
            {
                case '+':
                    return op1 + op2;

                case '-':
                    return op1 - op2;

                case '*':
                    return op1 * op2;

                case '/':
                    return op1 / op2;

                case '$':
                    return Math.Pow(op1, op2);

                default:
                    return 0;
            }
        }

        static double getOPND(char c)
        {
            for(int i = 0; i < NOPNDS - 1; i++)
            {
                if (c == opnd[i])
                    return opndval[i];
            }

            return 0;
        }

        /*************************************************************************
                            STACK FUNCTIONS:
		- the global object "OPNDstack" is an instance of the class "List"
		- ihe contents of "OPNDstack" are doubles
		- see its declaration immediately before the Main block

        *************************************************************************/
        static void OPNDpush(double opnd)
        {
            OPNDstack.Add(opnd);
        }

        static double OPNDpop()
        {
            double last = OPNDstack[OPNDstack.Count - 1];
            OPNDstack.RemoveAt(OPNDstack.Count - 1);
            return last;
        }

        static void dumpOPNDstack()
        {
            foreach (double value in OPNDstack)
                Console.Write(value + " | ");
            if (OPNDstack.Count == 0)
                Console.Write("EMPTY");
        }
    }
}