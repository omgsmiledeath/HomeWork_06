using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_06
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Задание
            /// Домашнее задание
            ///
            /// Группа начинающих программистов решила поучаствовать в хакатоне с целью демонстрации
            /// своих навыков. 
            /// 
            /// Немного подумав они вспомнили, что не так давно на занятиях по математике
            /// они проходили тему "свойства делимости целых чисел". На этом занятии преподаватель показывал
            /// пример с использованием фактов делимости. 
            /// Пример заключался в следующем: 
            /// Написав на доске все числа от 1 до N, N = 50, преподаватель разделил числа на несколько групп
            /// так, что если одно число делится на другое, то эти числа попадают в разные руппы. 
            /// В результате этого разбиения получилось M групп, для N = 50, M = 6
            /// 
            /// N = 50
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47
            /// Группа 3: 4 6 9 10 14 15 21 22 25 26 33 34 35 38 39 46 49
            /// Группа 4: 8 12 18 20 27 28 30 42 44 45 50
            /// Группа 5: 16 24 36 40
            /// Группа 6: 32 48
            /// 
            /// M = 6
            /// 
            /// ===========
            /// 
            /// N = 10
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 7 9
            /// Группа 3: 3 4 10
            /// Группа 4: 5 6 8
            /// 
            /// M = 4
            /// 
            /// Участники хакатона решили эту задачу, добавив в неё следующие возможности:
            /// 1. Программа считыват из файла (путь к которому можно указать) некоторое N, 
            ///    для которого нужно подсчитать количество групп
            ///    Программа работает с числами N не превосходящими 1 000 000 000
            ///   
            /// 2. В ней есть два режима работы:
            ///   2.1. Первый - в консоли показывается только количество групп, т е значение M
            ///   2.2. Второй - программа получает заполненные группы и записывает их в файл используя один из
            ///                 вариантов работы с файлами
            ///            
            /// 3. После выполения пунктов 2.1 или 2.2 в консоли отображается время, за которое был выдан результат 
            ///    в секундах и миллисекундах
            /// 
            /// 4. После выполнения пунта 2.2 программа предлагает заархивировать данные и если пользователь соглашается -
            /// делает это.
            /// 
            /// Попробуйте составить конкуренцию начинающим программистам и решить предложенную задачу
            /// (добавление новых возможностей не возбраняется)
            ///
            /// * При выполнении текущего задания, необходимо документировать код 
            ///   Как пометками, так и xml документацией
            ///   В обязательном порядке создать несколько собственных методов
            #endregion
            Console.WriteLine("Введите число ( макисмальное 1_000_000_000)");
            uint end = Convert.ToUInt32(Console.ReadLine());
            //uint end = 500_000_000;
            uint[] intMass = masFil(end);
            var tempDate = DateTime.Now;
            var simpleMass = getSimpleMas(end, intMass);
            TimeSpan ts = DateTime.Now.Subtract(tempDate);
            Console.WriteLine($"Сделал строку простых числел за {ts.TotalMilliseconds}");
           // List<uint>[] groups = new List<uint>[calcM(end)];
            var tempDate1 = DateTime.Now;
            using (StreamWriter fs = new StreamWriter("1.txt"))
            {
                tempDate = DateTime.Now;
              //  groups[0] = new List<uint> {1};
                fs.WriteLine(1);

               // groups[1] = getSimpleMas(end, intMass);
                foreach (var e in simpleMass)
                {
                    fs.Write($"{e} ");
                }
                ts = DateTime.Now.Subtract(tempDate);
                Console.WriteLine($"Сделал записал 1 группу за {ts.TotalMilliseconds}");
                fs.WriteLine();
                uint M = calcM(end);
                tempDate = DateTime.Now;
                var secondGroup = calcSecondGroup(M, simpleMass, end);
                foreach (var e in secondGroup)
                {
                   fs.Write($"{e} ");

                }
                fs.WriteLine();
                ts = DateTime.Now.Subtract(tempDate);
                Console.WriteLine($"Сделал посчитал и записал 2 группу за {ts.TotalMilliseconds}");
                List<uint> nextGroup = new List<uint>();
                List<uint> previousGroup = secondGroup;
                Console.WriteLine(M);
                for (uint i = 2; i <= M; i++)
                {
                    tempDate = DateTime.Now;
                    nextGroup = calcNextGroup(i, M, simpleMass, previousGroup, end);
                    Console.WriteLine($"{i}         Пошла запись");
                    foreach (var e in nextGroup)
                    {
                       fs.Write($"{e} ");
                       // Console.Write($"{e} ");
                    }
                    fs.WriteLine();
                    previousGroup = nextGroup;
                    //Console.WriteLine($"Закончил группу {i}");
                    //groups[i] = calcNextGroup(i,M, groups, end);
                    ts = DateTime.Now.Subtract(tempDate);
                    Console.WriteLine($"Сделал посчитал и записал {i} группу за {ts.TotalMilliseconds}");
                }
            }
            ts = DateTime.Now.Subtract(tempDate1);
            Console.WriteLine($"Заполнил все группы за {ts.TotalMilliseconds}");
            
            Console.ReadKey();
        }

        static void WriteGroupInFile(StreamWriter sr,List<uint> group)
        {

        }

        static uint calcM(uint N)
        {
            uint temp = 0;
            int result = 1;
            do
            {
                temp++;
                result *= 2;
            } while (result <= N);
            return temp;

        }

        static uint[] masFil(uint end)
        {
            uint[] result = new uint[end + 1];
            result[0] = 0;
            for (uint i = 1; i < end + 1; i++)
            {
                result[i] = i;
            }
            return result;
        }

        static List<uint> getSimpleMas(uint end, uint[] mas)
        {
            Console.WriteLine();
            var list = new List<uint>();

            uint count = 0;
            for (uint i = 2; i < end + 1; i++)
            {
                if (mas[i] != 0)
                {
                    list.Add(mas[i]);
                    count++;
                    for (uint j = i * i; j < end + 1; j += i)
                    {
                        mas[j] = 0;
                    }
                }
            }
            //var result = new uint[count];
            //var countJ = 0;
            //for (int i = 0; i < count; i++)
            //{
            //    if(list[i]!=0)
            //    {
            //        result[i] = list[i];
            //        countJ++;
            //    }

            //}

            return list;
        }


        static List<uint> calcSecondGroup(uint M, List<uint> simple, uint end)
        {
            List<uint> result = new List<uint>();
            for (int i = 0; i < simple.Count; i++)
            {

                for (int j = i; j < simple.Count; j++)
                {
                    //Console.Write($" groups[1][{i}]({groups[1][i]})  * groups[1][{j}]({groups[1][j]}) ");
                    if (simple[i] * simple[j] < end)
                    {
                        // Console.WriteLine($"{groups[1][i] * groups[1][j]}");
                        result.Add(simple[i] * simple[j]);
                    }
                    else break;
                }
            }
            result.Sort();
            return result;
        }

        //static List<uint> calcSecondGroup(uint M,List<uint>[]groups,uint end)
        //{
        //    List<uint> result = new List<uint>();
        //    for (int i = 0; i < groups[1].Count; i++)
        //    {

        //        for (int j = i; j < groups[1].Count; j++)
        //        {
        //            //Console.Write($" groups[1][{i}]({groups[1][i]})  * groups[1][{j}]({groups[1][j]}) ");
        //            if (groups[1][i] * groups[1][j] < end)
        //            {
        //                // Console.WriteLine($"{groups[1][i] * groups[1][j]}");
        //                result.Add(groups[1][i] * groups[1][j]);
        //            }
        //            else break;
        //        }
        //    }
        //    result.Sort();
        //    return result;
        //}


        //static List<uint> calcNextGroup(uint count, uint M, List<uint>[] groups, uint end)
        //{
        //    List<uint> result = new List<uint>();
        //        for (int i = 0; i < groups[1].Count; i++)
        //        {
        //            for (int j = i; j < groups[count-1].Count; j++)
        //            {
        //                //Console.Write($" groups[1][{i}]({groups[1][i]})  * groups[1][{j}]({groups[1][j]}) ");
        //                if (groups[1][i] * groups[count-1][j] <= end)
        //                {
        //                    // Console.WriteLine($"{groups[1][i] * groups[1][j]}");
        //                    result.Add(groups[1][i] * groups[count-1][j]);
        //                }
        //                else break;
        //            }
        //        }
            
            
        //    result.Sort();
        //    return result.Distinct().ToList();
        //}

        static List<uint> calcNextGroup(uint count, uint M, List<uint> secGroup,List<uint>prevGroup, uint end)
        {
            List<uint> result = new List<uint>();
            for (int i = 0; i < secGroup.Count; i++)
            {
                for (int j = i; j < prevGroup.Count; j++)
                {
                    //Console.Write($" groups[1][{i}]({groups[1][i]})  * groups[1][{j}]({groups[1][j]}) ");
                    if (secGroup[i] * prevGroup[j] <= end)
                    {
                        // Console.WriteLine($"{groups[1][i] * groups[1][j]}");
                        result.Add(secGroup[i] * prevGroup[j]);
                    }
                    else break;
                }
            }

            Console.WriteLine("Пошла сортировка");
            result.Sort();
            return result.Distinct().ToList();
        }



    }
    }
