using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kalashnikov
{
    class Program
    {
        static void Result(int S, int G, int T, bool check, ref int time, ref int strike_time) //----------------------
        {
            int n;
            string M;
            var rnd = new Random();
            if (check)
            {
                n = rnd.Next(3, 10);
                time += n; //----------------------
                Thread.Sleep(n * 1000);
                M = "Занял направление!";
                Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");
            }
            n = rnd.Next(2, 6);
            time += n; //----------------------
            Thread.Sleep(n * 1000);
            M = "Подготовиться к стрельбе!";
            Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");

            n = rnd.Next(1, 4);
            time += n; //----------------------
            Thread.Sleep(n * 1000);
            M = "К стрельбе готов!";
            Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");

            n = rnd.Next(1, 2);
            time += n; //----------------------
            Thread.Sleep(n * 1000);
            M = "Произвести стрельбу!";
            Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");

            n = rnd.Next(5, 15);
            time += n; //----------------------
            strike_time += n;
            Thread.Sleep(n * 1000);
            M = "Стрельбу окончил!";
            Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");

        }

        async static void First6Ready(int S, int G, int T, int n)
        {
            string M;
            var rnd = new Random();
            await Task.Delay(n * 1000);
            M = "Занял направление!";
            Console.WriteLine($"Направление {G}, инструктор {T}, стрелок {S}: {M}");

        }
        static void Instructor1(Queue<int> Shooter, Queue<int> Counter, out int all_time, out int strike_time, out bool exit) //----------------------
        {
            int T = 1;
            exit = false;
            all_time = 0; //----------------------
            strike_time = 0;
            int[] instructor = new int[3];
            int[] counter = new int[3];
            int[] wait = new int[3];
            bool[] checking_cycle = new bool[3] { true, true, true };
            int S, G;
            bool check = false;
            var rnd = new Random();
            int attempt = 5;
            for (int i = 0; i < 3; i++)
            {
                instructor[i] = Shooter.Dequeue();
                counter[i] = Counter.Dequeue();
                wait[i] = rnd.Next(3, 10);
                all_time += wait[i]; //----------------------
                S = instructor[i];
                G = i + 1;
                //Console.WriteLine($" Cтрелок {S} идет на направление {G}");
                First6Ready(S, G, T, wait[i]);

            }

            while (Shooter.Count >= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (checking_cycle[i] == true)
                    {
                        S = instructor[i];
                        G = i + 1;
                        if ((i == 0) && (check == false))
                        {
                            Thread.Sleep(wait[i] * 1000);
                        }
                        Result(S, G, T, check, ref all_time, ref strike_time); //----------------------
                        checking_cycle[i] = false;

                        if (counter[i] < attempt)
                        {
                            Shooter.Enqueue(instructor[i]);
                        }
                        Counter.Enqueue(counter[i] + 1);

                        if (Shooter.Count > 0)
                        {
                            instructor[i] = Shooter.Dequeue();
                            counter[i] = Counter.Dequeue();
                            //S = instructor[i];
                            //G = i + 1;
                            //Console.WriteLine($" Cтрелок {S} идет на направление {G}");
                            checking_cycle[i] = true;
                        }
                    }
                }
                check = true;
                if (checking_cycle[0] == checking_cycle[1] == checking_cycle[2] == false)
                {
                    exit = true;
                    break;
                }
            }
        }

        static void Instructor2(Queue<int> Shooter, Queue<int> Counter, out int all_time, out int strike_time, out bool exit) //----------------------
        {
            int T = 2;
            exit = false;
            all_time = 0; //----------------------
            strike_time = 0;
            int[] instructor = new int[3];
            int[] counter = new int[3];
            int[] wait = new int[3];
            bool[] checking_cycle = new bool[3] { true, true, true };
            int S, G;
            bool check = false;
            var rnd = new Random();
            int attempt = 5;
            for (int i = 0; i < 3; i++)
            {
                instructor[i] = Shooter.Dequeue();
                counter[i] = Counter.Dequeue();
                wait[i] = rnd.Next(3, 10);
                all_time += wait[i];//----------------------
                S = instructor[i];
                G = i + 4;
                //Console.WriteLine($" Cтрелок {S} идет на направление {G}");
                First6Ready(S, G, T, wait[i]);
            }

            while (Shooter.Count >= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (checking_cycle[i] == true)
                    {
                        S = instructor[i];
                        G = i + 4;
                        if ((i == 0) && (check == false))
                        {
                            Thread.Sleep(wait[i] * 1000);
                        }
                        Result(S, G, T, check, ref all_time, ref strike_time); //----------------------
                        checking_cycle[i] = false;

                        if (counter[i] < attempt)
                        {
                            Shooter.Enqueue(instructor[i]);
                        }
                        Counter.Enqueue(counter[i] + 1);

                        if (Shooter.Count > 0)
                        {
                            instructor[i] = Shooter.Dequeue();
                            counter[i] = Counter.Dequeue();
                            //S = instructor2[i];
                            //G = i + 4;
                            //Console.WriteLine($" Cтрелок {S} идет на направление {G}");
                            checking_cycle[i] = true;
                        }
                    }
                }
                check = true;
                if (checking_cycle[0] == checking_cycle[1] == checking_cycle[2] == false)
                {
                    exit = true;
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            int all_time1 = 0;
            int all_time2 = 0;
            int strike_time1 = 0;
            int strike_time2 = 0;
            bool exit1 = false;
            bool exit2 = false;
            Queue<int> Shooter = new Queue<int>(13);
            Queue<int> Counter = new Queue<int>(13);
            for (int i = 0; i < 13; i++)
            {
                Shooter.Enqueue(i + 1);
                Counter.Enqueue(1);
            }
            Thread thr1 = new Thread(() => Instructor1(Shooter, Counter, out all_time1, out strike_time1, out exit1));
            Thread thr2 = new Thread(() => Instructor2(Shooter, Counter, out all_time2, out strike_time2, out exit2));
            thr1.Start();
            thr2.Start();

            while (Shooter.Count >= 0)
            {
                if ((exit1) && (exit2))
                {
                    Console.WriteLine("##########################");
                    Console.WriteLine($"Сумма затраченного времени: {(all_time1 + all_time2) / 60} минуты {(all_time1 + all_time2) % 60} секунды"); //----------------------
                    Console.WriteLine($"Длительность стрельб: {(strike_time1 + strike_time2) / 60} минуты {(strike_time1 + strike_time2) % 60} секунды");
                    Console.WriteLine("##########################");

                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}