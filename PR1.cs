using System;
using System.IO;
using System.Xml.Linq;
using System.Text.Json;
using System.IO.Compression;

namespace PR1
{
    class Student
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Year { get; set; }
        public string Group { get; set; }
    }

    class Program
    {
        static void getDiskInformation()
        {
            Console.WriteLine("\n1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.\n");
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"\nНазвание: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize / 1073741824} GB");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace / 1073741824} GB");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
        }

        static void processTextFile(string path)
        {
            Console.WriteLine("\n2. Работа с файлами.\n");
            FileInfo fileInf = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"\nФайл, создан по пути: {path}");
                    if (fileInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileInf.Name);
                        Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fileInf.Length);
                        Console.WriteLine();
                    }
                }
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    Console.Write("Введите строку для записи в файл: ");
                    sw.WriteLine(Console.ReadLine());
                }
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.Write("Информация из файла: ");
                    Console.WriteLine(sr.ReadToEnd());
                }
                Console.Write("Введите 1 для удаления файла:\n");
                int sigh = int.Parse(Console.ReadLine());
                if (sigh == 1)
                {
                    if (fileInf.Exists)
                    {
                        fileInf.Delete();
                        Console.WriteLine($"Файл по пути {path} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Файл по пути {path} не удален.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void processJsonFile(string path)
        {
            Console.WriteLine("\n3. Работа с форматом JSON. \n");
            FileInfo fileJSON = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"Файл, создан по пути: {path}");
                    if (fileJSON.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileJSON.Name);
                        Console.WriteLine("Время создания: {0}", fileJSON.CreationTime);
                        Console.WriteLine("Размер: {0}", fileJSON.Length);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    Student student = new Student();
                    Console.Write("Введите имя студента: ");
                    student.Name = Console.ReadLine();
                    Console.Write("Введите фамилию студента: ");
                    student.SurName = Console.ReadLine();
                    Console.Write("Введите группу студента: ");
                    student.Group = Console.ReadLine();
                    while (true)
                    {
                        Console.Write("Введите год поступления студента: ");
                        string year = Console.ReadLine();
                        if (int.TryParse(year, out int number))
                        {
                            student.Year = number;
                            break;
                        }
                        Console.Write("Вы ввели не число, введите число еще раз: ");
                    }
                    sw.WriteLine(JsonSerializer.Serialize<Student>(student));
                }
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.Write("\nИнформация из файла:\n");
                    Student restoredStudent = JsonSerializer.Deserialize<Student>(sr.ReadToEnd());
                    Console.WriteLine($"Name: {restoredStudent.Name}\nSurname: {restoredStudent.SurName}");
                    Console.WriteLine($"Group: {restoredStudent.Group}\nYear: {restoredStudent.Year}");
                }
                Console.Write("Введите 1 для удаления файла:\n");
                int sigh = int.Parse(Console.ReadLine());
                if (sigh == 1)
                {
                    if (fileJSON.Exists)
                    {
                        fileJSON.Delete();
                        Console.WriteLine($"Файл по пути {path} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Файл по пути {path} не удален.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void processXMLFile(string path)
        {
            Console.WriteLine("\n4. Работа с форматом XML. \n");
            FileInfo fileXML = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"Файл, создан по пути: {path}");

                    if (fileXML.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileXML.Name);
                        Console.WriteLine("Время создания: {0}", fileXML.CreationTime);
                        Console.WriteLine("Размер: {0}", fileXML.Length);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            XDocument xDoc = new XDocument();
            XElement student = new XElement("student");
            Console.Write("Введите имя студента: ");
            XAttribute nameXAttr = new XAttribute("name", Console.ReadLine());
            Console.Write("Введите фамилию студента: ");
            XAttribute surnameXAttr = new XAttribute("surname", Console.ReadLine());
            Console.Write("Введите группу студента: ");
            XElement groupXElm = new XElement("group", Console.ReadLine());
            Console.Write("Введите год поступления студента: ");
            XElement yearXElm = new XElement("year", Console.ReadLine());
            Console.Write("Введите факультет студента: ");
            XElement facultyXElm = new XElement("faculty", Console.ReadLine());
            student.Add(nameXAttr);
            student.Add(surnameXAttr);
            student.Add(groupXElm);
            student.Add(yearXElm);
            student.Add(facultyXElm);
            XElement students = new XElement("students");
            students.Add(student);
            xDoc.Add(students);
            xDoc.Save(path);
            XDocument xDocLoad = XDocument.Load(path);
            XElement studentXElement = xDocLoad.Element("students").Element("student");
            nameXAttr = studentXElement.Attribute("name");
            surnameXAttr = studentXElement.Attribute("surname");
            groupXElm = studentXElement.Element("group");
            yearXElm = studentXElement.Element("year");
            facultyXElm = studentXElement.Element("faculty");
            Console.WriteLine("\nИнформация в файле:\n");
            Console.WriteLine($"Имя и фамилия студента: {nameXAttr.Value} {surnameXAttr.Value}");
            Console.WriteLine($"Группа студента: {groupXElm.Value}");
            Console.WriteLine($"Год поступления студента: {yearXElm.Value}");
            Console.WriteLine($"Факультет студента: {facultyXElm.Value}");
            Console.Write("Введите 1 для удаления файла:\n");
            int sigh = int.Parse(Console.ReadLine());
            if (sigh == 1)
            {
                if (fileXML.Exists)
                {
                    fileXML.Delete();
                    Console.WriteLine($"Файл по пути {path} удален.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Файл по пути {path} не удален.");
            }
        }

        static void processZIPArchive(string pathArchive, string pathFile)
        {
            Console.WriteLine("\n5. Создание zip архива, добавление туда файла, определение размера архива.\n");
            try
            {
                using (FileStream fStream = File.Create(pathArchive))
                {
                    Console.WriteLine($"Файл, создан по пути: {pathArchive}");
                    FileInfo fileInf = new FileInfo(pathArchive);
                    if (fileInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileInf.Name);
                        Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fileInf.Length);
                        Console.WriteLine();
                    }
                }
                using (FileStream zipToOpen = new FileStream(pathArchive, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry fileText = archive.CreateEntry(pathFile);
                        using (StreamWriter writer = new StreamWriter(fileText.Open()))
                        {
                            Console.Write($"Введите данные в файл для добавления его в архив {pathArchive}: ");
                            writer.WriteLine(Console.ReadLine());
                        }
                    }
                }
                FileInfo fileInfArchive = new FileInfo(pathArchive);
                if (fileInfArchive.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfArchive.Name);
                    Console.WriteLine("Время создания: {0}", fileInfArchive.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfArchive.Length);
                    Console.WriteLine();
                }
                using (ZipArchive zip = ZipFile.OpenRead(pathArchive))
                {
                    zip.ExtractToDirectory("./");
                }
                Console.WriteLine("Данные из разархифированного файла: ");
                FileInfo fileInfText = new FileInfo(pathFile);
                if (fileInfText.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfText.Name);
                    Console.WriteLine("Время создания: {0}", fileInfText.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfText.Length);
                    Console.WriteLine();
                }
                using (StreamReader sr = new StreamReader(pathFile))
                {
                    Console.Write("Информация из файла: ");
                    Console.WriteLine(sr.ReadToEnd());
                }
                if (fileInfText.Exists)
                {
                    fileInfText.Delete();
                    Console.WriteLine($"Разархивированный файл по пути {pathFile} удален.");
                    Console.WriteLine();
                }
                Console.Write("Введите 1 для удаления файла:\n");
                int sign = int.Parse(Console.ReadLine());
                if (sign == 1)
                {
                    using (ZipArchive archive = ZipFile.Open(pathArchive, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry archiveEntry = archive.Entries[0];
                        archiveEntry.Delete();
                    }
                    Console.WriteLine($"Файл {pathFile} в архиве {pathArchive} был удален.");
                }
                else
                {
                    Console.WriteLine("$Файл { pathFile} в архиве { pathArchive} не был удален.");
                }

                FileInfo fileInfArchiveEmpty = new FileInfo(pathArchive);
                if (fileInfArchiveEmpty.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfArchiveEmpty.Name);
                    Console.WriteLine("Время создания: {0}", fileInfArchiveEmpty.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfArchiveEmpty.Length);
                    Console.WriteLine();
                }
                Console.Write("Введите 1 для удаления файла:\n");
                sign = int.Parse(Console.ReadLine());
                if (sign == 1)
                {
                    if (fileInfArchive.Exists)
                    {
                        fileInfArchive.Delete();
                        Console.WriteLine($"Файл по пути {pathArchive} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Файл по пути {pathArchive} не удален.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nВведите цифру для доступа к заданиям:");
                Console.WriteLine("\n1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы. ");
                Console.WriteLine("2. Работа с файлами ");
                Console.WriteLine("3. Работа с форматом JSON ");
                Console.WriteLine("4. Работа с форматом XML ");
                Console.WriteLine("5. Создание zip архива, добавление туда файла, определение размера архива ");
                Console.WriteLine("6. Выход из меню\n");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        getDiskInformation();
                        break;
                    case 2:
                        string pathTXT = "text.txt";
                        processTextFile(pathTXT);
                        break;
                    case 3:
                        string pathJSON = "student.json";
                        processJsonFile(pathJSON);
                        break;
                    case 4:
                        string pathXML = "students.xml";
                        processXMLFile(pathXML);
                        break;
                    case 5:
                        string pathZIP = "zip.zip";
                        string pathZIPFile = "zip.txt";
                        processZIPArchive(pathZIP, pathZIPFile);
                        break;
                    case 6:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Данного пункта нет в меню");
                        break;
                }
            }
            Console.Read();
        }
    }
}
