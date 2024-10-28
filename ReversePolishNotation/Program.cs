namespace ReversePolishNotation
{
    internal class Program
    {
        public class Converter()
        {
            enum Op 
            {   plus,
                minus,
                mult,
                div
            }
            /// <summary>
            /// конвертирует входное выражение в обратную польскую запись
            /// 
            /// </summary>
            /// <param name="inp"> входная формула в инфиксной записи  операнлы и знаки операций разделениы пробелами</param>
            /// <returns></returns>
           public string[] convert(string inp) 
            {
               var ops= inp.Split(' ');
                List<string> ret = new List<string>();
                Dictionary<String,int> opPrior= new Dictionary<string, int>()//приоритеты операций
                {
                    { "(",0},
                    { ")",1},
                    { "+",2},
                    { "-",2},
                    { "*",3},
                    { "/",3},
                    { "**",4}
                };
                Stack<string> operations = new Stack<string>();//стэк соперациясми +-*/(
                for (int i = 0; i < ops.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(ops[i])) continue;
                    string operation = ops[i].Trim();// в этой строке операция или операнд
                    if (!opPrior.ContainsKey(operation)) // это не операция  ,считаем операндом
                    {
                        ret.Add(operation);
                    }
                    else 
                    {
                        
                        if ((operations.Count() == 0)|| operation=="(") operations.Push(operation); 
                        else
                        {
                            if (operation == ")")
                            {
                                while (
                                (operations.Count() > 0) &&
                                (operations.Peek() != "(")
                                ) ret.Add(operations.Pop());
                                operations.Pop();//remove  '(' from stack
                            }
                            else
                            {
                                while (
                                    (operations.Count() > 0) &&
                                    (opPrior[operations.Peek()] >= opPrior[operation])
                                    )
                                    ret.Add(operations.Pop());
                                operations.Push(operation);
                            }
                        }
                    }
                }
                return ret.Union(operations).ToArray();
            } 
        }
        static void Main(string[] args)
        {
            var c = new Converter();
            Console.WriteLine(string.Join(' ',c.convert("( A + B )  * ( C + D ) - E")));
        }
    }
}
