using System;
using System.IO;
using System.Text;

namespace filemeneg
{
    /// <summary>
    /// класс программы
    /// </summary>
    class Program
    {
        // текущий путь к файлу
        static string nowPath;
        // путь к темп. хранению нужен при копировании
        static string tempPath = Path.Combine(Path.GetTempPath(), "tempfilenamepr");
        // название скопированного файла
        static string nameCopyFile;

        /// <summary>
        /// Основной метод
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Напиши при выборе операции \"help*\" и увидишь инструкцию");
            // номер  операции от 0 до 11
            int numberOfOperation = 1;
            do
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine($"Текущий путь: {nowPath}");
                    // выбор операции
                    ChoseOperation(out numberOfOperation);
                    // вызов операции
                    GetOperation(numberOfOperation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (numberOfOperation != 0);
        }

        /// <summary>
        /// вызывается нужный метод в зависимости от операции
        /// </summary>
        /// <param name="numberOfOperation">номер операции</param>
        private static void GetOperation(int numberOfOperation)
        {
            switch (numberOfOperation)
            {
                case 1:
                    ShowDisks();
                    return;
                case 2:
                    GetDirectory();
                    return;
                case 3:
                    ShowFilesFromDirectory();
                    return;
                case 4:
                    WriteToConsoleUTF8(Encoding.UTF8);
                    return;
                case 5:
                    WriteToConsoleDifferentEncording();
                    return;
                case 6:
                    CopyFile();
                    return;
                case 7:
                    GetInFile();
                    return;
                case 8:
                    DeleteFile();
                    return;
                case 9:
                    CreateFileUTF8(Encoding.UTF8);
                    return;
                case 10:
                    CreateFileDifferentEncording();
                    return;
                case 11:
                    FileСoncatenation();
                    return;
            }
        }

        /// <summary>
        /// 11-ая операция - конкатенация файлов
        /// </summary>
        private static void FileСoncatenation()
        {
            try
            {
                Console.WriteLine("1-добавить в конкатинацию файл, 2-вывести результат");
                // chose - выбор параметра добавить или выести результат
                int chose = ChoseNumber(2);
                // специальный временный путь для хранений
                string tempPath2 = Path.Combine(Path.GetTempPath(), "tempfilenamepr2");
                if (chose == 1)
                {
                    if (File.Exists(nowPath))
                    {
                        File.AppendAllText(tempPath2, File.ReadAllText(nowPath) + Environment.NewLine);
                        Console.WriteLine("Файл успешно добавлен в конкатенацию");
                    }
                    else
                    {
                        Console.WriteLine("Путь не ведет к файлу, файл не выбран");
                    }
                }
                if (chose == 2)
                {
                    if (File.Exists(tempPath2))
                    {
                        Console.WriteLine(File.ReadAllText(tempPath2));
                        File.WriteAllText(tempPath2, "");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 10 - ая операция по созданию файла в одной из трех кодировок
        /// </summary>
        private static void CreateFileDifferentEncording()
        {
            Console.WriteLine($"1. {Encoding.ASCII}");
            Console.WriteLine($"2. {Encoding.Unicode}");
            Console.WriteLine($"3. {Encoding.UTF32}");
            int chose = ChoseNumber(3);
            switch (chose)
            {
                case 1:
                    CreateFileUTF8(Encoding.ASCII);
                    return;
                case 2:
                    CreateFileUTF8(Encoding.Unicode);
                    return;
                case 3:
                    CreateFileUTF8(Encoding.UTF32);
                    return;
            }
        }

        /// <summary>
        /// создание файла в разной кодировки, вообще хотел поставить по умолчанию UTF8,
        /// но нельзя поставить по-умолчанию
        /// </summary>
        /// <param name="encord">выбор кодировки</param>
        private static void CreateFileUTF8(Encoding encord)
        {
            try
            {
                // считывание с консоли в массив строк
                string[] linesFromConsole = ReadFromConsole();
                Console.Write("Введите название файла: ");
                string nameFile = Console.ReadLine() + ".txt";
                File.WriteAllLines(Path.Combine(nowPath, nameFile), linesFromConsole, encord);
                Console.WriteLine("Файл успешно создан");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Файл не создан");
            }
        }

        /// <summary>
        /// метод по считываю из консоли, признак окончания ///
        /// </summary>
        /// <returns></returns>
        private static string[] ReadFromConsole()
        {
            Console.WriteLine("Вводите текст, а на последней строке напишите ///(признак окончания)");
            string[] lines = new string[0];
            string temp = Console.ReadLine();
            while (temp != "///")
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = temp;
                temp = Console.ReadLine();
            }
            return lines;
        }

        /// <summary>
        /// вывод в консоль в различной кодировке
        /// </summary>
        private static void WriteToConsoleDifferentEncording()
        {
            Console.WriteLine($"1. {Encoding.ASCII}");
            Console.WriteLine($"2. {Encoding.Unicode}");
            Console.WriteLine($"3. {Encoding.UTF32}");
            int chose = ChoseNumber(3);
            switch (chose)
            {
                case 1:
                    WriteToConsoleUTF8(Encoding.ASCII);
                    return;
                case 2:
                    WriteToConsoleUTF8(Encoding.Unicode);
                    return;
                case 3:
                    WriteToConsoleUTF8(Encoding.UTF32);
                    return;
            }
        }

        /// <summary>
        /// удаление файла
        /// </summary>
        private static void DeleteFile()
        {
            try
            {
                if (File.Exists(nowPath))
                {
                    File.Delete(nowPath);
                    Console.WriteLine("Файл успешно удален");
                }
                else
                {
                    Console.WriteLine("Путь не ведет к файлу, файл не выбран");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// вставка файла
        /// </summary>
        private static void GetInFile()
        {
            try
            {
                if (nameCopyFile != null)
                {
                    File.Copy(tempPath, Path.Combine(nowPath, nameCopyFile));
                    Console.WriteLine("Файл успешно вставлен");
                }
                else
                {
                    Console.WriteLine("Не было скопировано файла, чтобы вставить");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// копирование файла
        /// </summary>
        private static void CopyFile()
        {
            try
            {
                if (File.Exists(nowPath))
                {
                    File.Copy(nowPath, tempPath, true);
                    nameCopyFile = Path.GetFileName(nowPath);
                    Console.WriteLine("Файл успешно скопирован");
                }
                else
                {
                    Console.WriteLine("Путь не ведет к файлу, файл не выбран");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// вывод в консоль любой кодировке
        /// </summary>
        /// <param name="encor"></param>
        private static void WriteToConsoleUTF8(Encoding encor)
        {
            try
            {
                if (File.Exists(nowPath))
                {
                    Console.WriteLine(File.ReadAllText(nowPath, encor));
                }
                else
                {
                    Console.WriteLine("Путь не содержит файла");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// показ всех файлов папки
        /// </summary>
        private static void ShowFilesFromDirectory()
        {
            try
            {
                string[] files = Directory.GetFiles(nowPath);
                if (files.Length != 0)
                {
                    for (int i = 1; i <= files.Length; i++)
                    {
                        Console.WriteLine($"{i}. {files[i - 1]}");
                    }
                    int chose = ChoseNumber(files.Length);
                    nowPath = Path.Combine(nowPath, files[chose - 1]);

                }
                else
                {
                    Console.WriteLine("Отсутствуют файлы");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Невозможно просмотреть файлы по заданному пути");
            }
        }

        /// <summary>
        /// выбор способа выбора папки
        /// </summary>
        private static void GetDirectory()
        {
            int chose;
            do
            {
                Console.Write("Задать путь-1, перейти из текущий папки-2: ");
            } while (!int.TryParse(Console.ReadLine(), out chose) | chose < 1 | chose > 2);

            if (chose == 1)
            {
                AssignPath();
            }
            if (chose == 2)
            {
                ChoseDirectory();
            }
        }

        /// <summary>
        /// переход по папкам в живую
        /// </summary>
        private static void ChoseDirectory()
        {
            try
            {
                if (nowPath != null & nowPath != "")
                {
                    string[] directories = Directory.GetDirectories(nowPath);
                    Console.WriteLine("0. Выход из метода");
                    for (int i = 1; i <= directories.Length; i++)
                    {
                        Console.WriteLine($"{i}. {directories[i - 1]}");
                    }
                    int plus = 0;
                    if (Path.GetDirectoryName(nowPath) != null)
                    {
                        plus++;
                        Console.WriteLine($"{directories.Length + 1}. exit from this directory");
                    }
                    int chose = ChoseNumber(directories.Length + plus, 0);
                    if (chose == directories.Length + 1)
                    {
                        nowPath = Path.GetDirectoryName(nowPath);
                        ChoseDirectory();
                    }
                    if (chose != 0 & chose != directories.Length + 1)
                    {
                        nowPath = Path.Combine(nowPath, directories[chose - 1]);
                        ChoseDirectory();
                    }
                }
                else
                {
                    Console.WriteLine("Пустой путь");
                }
            }
            catch
            {
                Console.WriteLine("Невозможно прочитать папку по этому пути");
            }
        }

        /// <summary>
        /// выбор папи по пути
        /// </summary>
        private static void AssignPath()
        {
            Console.WriteLine("Введите полный путь к папке");
            string path;
            do
            {
                path = ReadLine();
            } while (path == "help*");

            if (Directory.Exists(path))
            {
                nowPath = path;
                Console.WriteLine("Путь успешно задан");
            }
            else
            {
                Console.WriteLine("Такой папки нет или она недоступна из-за разрешений");
            }
        }

        /// <summary>
        /// выбор диска
        /// </summary>
        private static void ShowDisks()
        {
            try
            {
                var allDrives = DriveInfo.GetDrives();
                for (int i = 1; i <= allDrives.Length; i++)
                {
                    Console.WriteLine($"{i}. Drive {allDrives[i - 1]}");
                }
                int number = ChoseNumber(allDrives.Length);
                nowPath = allDrives[number - 1].Name;
            }
            catch
            {
                Console.WriteLine("Не удалось прочитать ваши диски");
            }
        }


        /// <summary>
        /// выбор параметра от start до end
        /// </summary>
        /// <param name="length">длина выбора(конец)</param>
        /// <param name="start"> начало 1 по умолчанию</param>
        /// <returns></returns>
        private static int ChoseNumber(int length, int start = 1)
        {
            int number;
            do
            {
                Console.Write($"Выберите номер от {start} до {length}: ");
            } while (!int.TryParse(ReadLine(), out number) | number < start | number > length);
            return number;
        }

        /// <summary>
        /// выбор номера операции от 0 до 11
        /// </summary>
        /// <param name="numberOfOperation"></param>
        private static void ChoseOperation(out int numberOfOperation)
        {
            do
            {
                Console.Write("Введите тип операции от 0 до 11: ");

            } while (!int.TryParse(ReadLine(), out numberOfOperation) | numberOfOperation < 0 | numberOfOperation > 11);
        }

        /// <summary>
        /// переопределение Console.Readline()
        /// </summary>
        /// <returns></returns>
        private static string ReadLine()
        {
            string line = Console.ReadLine();
            if (line == "help*")
            {
                Instruct();
            }
            return line;
        }

        /// <summary>
        /// вывод инструкции
        /// </summary>
        private static void Instruct()
        {
            Console.WriteLine();
            Console.WriteLine("0.окончание программы");
            Console.WriteLine("1.просмотр списка дисков компьютера и выбор диска;");
            Console.WriteLine("2.переход в другую директорию(выбор папки);");
            Console.WriteLine("3.просмотр списка файлов в директории;");
            Console.WriteLine("4.вывод содержимого текстового файла в консоль в кодировке UTF-8;");
            Console.WriteLine("5.вывод содержимого текстового файла в консоль в выбранной");
            Console.WriteLine("6.копирование файла;");
            Console.WriteLine("7.перемещение файла в выбранную пользователем директорию;");
            Console.WriteLine("8.удаление файла;");
            Console.WriteLine("9.создание простого текстового файла в кодировке UTF-8;");
            Console.WriteLine("10.создание простого текстового файла в выбранной пользователем");
            Console.WriteLine("кодировке(предоставляется не менее трех вариантов);");
            Console.WriteLine("11.конкатенация содержимого двух или более текстовых файлов и вывод");
            Console.WriteLine("результата в консоль в кодировке UTF-8.");
            Console.WriteLine("Выберите номер от 1 до 11, про которых хотите узнать подробнее, 0 для продолжения");
            int chose = ChoseNumber(11, 0);
            MoreInstructions(chose);
            Console.WriteLine();
        }

        /// <summary>
        /// доп инструкция
        /// </summary>
        /// <param name="chose"></param>
        private static void MoreInstructions(int chose)
        {
            switch (chose)
            {
                case 1:
                    Instruct1();
                    return;
                case 2:
                    Instruct2();
                    return;
                case 3:
                    Instruct3();
                    return;
                case 4:
                    Instruct4();
                    return;
                case 5:
                    Instruct5();
                    return;
                case 6:
                    Instruct6();
                    return;
                case 7:
                    Instruct7();
                    return;
                case 8:
                    Instruct8();
                    return;
                case 9:
                    Instruct9();
                    return;
                case 10:
                    Instruct10();
                    return;
                case 11:
                    Instruct11();
                    return;
            }
        }

        /// <summary>
        /// Инструкция к 1-ой операции
        /// </summary>
        private static void Instruct1()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 1 - выбор диска");
            Console.WriteLine("В начале всей работы ты должен выбрать диск");
            Console.WriteLine("Или же в операции 2.1 указать конечный путь к файлу");
            Console.WriteLine("Также выбор диска нужен, чтобы выйти из файла");
            Console.WriteLine("Потому что выход из файла не предусмотрен");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 2-ой операции
        /// </summary>
        private static void Instruct2()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 2 - выбор папки");
            Console.WriteLine("В начале ты выбираешь способ задания папки путем или выбором из списка");
            Console.WriteLine("если ты работаешь выбором из списка, то есть возможность");
            Console.WriteLine("завершить при выборе 0, а при выборе последнего значения");
            Console.WriteLine("можно выйти на папку выше");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 3-ой операции
        /// </summary>
        private static void Instruct3()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 3 - выбор файла");
            Console.WriteLine("Путь должен быть указан к папке где храниться файл");
            Console.WriteLine("Чтобы сделать так путь нужно использовать операции 1 и 2");
            Console.WriteLine("Выбери из списка нужный файл и всё");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 4-ой операции
        /// </summary>
        private static void Instruct4()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 4 - вывод в кодировке UTF8");
            Console.WriteLine("Сначала с помощью операций 1-3 укажи путь на файл");
            Console.WriteLine("который ты так хочешь вывести");
            Console.WriteLine("Тогда о чудо выведиться нужный файл в консоль");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 5-ой операции
        /// </summary>
        private static void Instruct5()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 5 - вывод в выбираемой кодировке");
            Console.WriteLine("Выбери кодировку из спика");
            Console.WriteLine("С помощью операций 1-3 укажи путь на файл");
            Console.WriteLine("который ты так хочешь вывести");
            Console.WriteLine("Тогда о чудо выведиться нужный файл в консоль");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 6-ой операции
        /// </summary>
        private static void Instruct6()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 6 - копирование файла");
            Console.WriteLine("С помощью операций 1-3 укажи путь на файл");
            Console.WriteLine("Далее зайди в операцию 6 и файл скопируется");
            Console.WriteLine("Чтобы его куда-то вставить нужнао операция 7");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 7-ой операции
        /// </summary>
        private static void Instruct7()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 7 - вставка файла");
            Console.WriteLine("Сначала нужно скопировать файл с помощью 6");
            Console.WriteLine("Далее выбрать папку в которую хочешь вставить с помощь операций 1-3");
            Console.WriteLine("Используешь операцию 7 и о чудо файл в той папке");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 8-ой операции
        /// </summary>
        private static void Instruct8()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 8 - удаление файла");
            Console.WriteLine("С помощью 1-3 выбираешь файл");
            Console.WriteLine("Заходишь в 8 и файл удалиться");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 9-ой операции
        /// </summary>
        private static void Instruct9()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 9 - создание текстового файла в UTF8");
            Console.WriteLine("Выбираешь папку с помощь 1-2 операций");
            Console.WriteLine("Заходишь в 9 и дальше там всё понятно как создать");
            Console.WriteLine("Еще предложит написать имя");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 10-ой операции
        /// </summary>
        private static void Instruct10()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 10 - создание текстового файла с выбором кодировки");
            Console.WriteLine("Выбираешь папку с помощь 1-2 операций");
            Console.WriteLine("Заходишь в 10 и дальше там всё понятно как создать");
            Console.WriteLine("Еще предложит написать имя и кодировку выбрать");
            Console.WriteLine();
        }

        /// <summary>
        /// Инструкция к 11-ой операции
        /// </summary>
        private static void Instruct11()
        {
            Console.WriteLine();
            Console.WriteLine("Инструкция к использованию операции 11 - конкатинация файлов");
            Console.WriteLine("Выбираешь первый файл с помощью операций 1-3");
            Console.WriteLine("Заходишь в 11 и добавляешь в конкатенацию");
            Console.WriteLine("Можно повторить это несколько раз");
            Console.WriteLine("Далее заходишь в 11 и выводишь результат конкатенации");
            Console.WriteLine("Кстати вывести можно только раз, потом удалиться");
            Console.WriteLine();
        }
    }
}
