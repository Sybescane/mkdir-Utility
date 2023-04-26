using System.IO;
using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace Lab1_OS_mkdir
{
    internal class mkdir
    {
        static void Main(string[] args)
        {
            new Command(args).Run();
        }
    }

    public class Command
    {
        public string parentDir;
        public List<string> directories = new List<string>();
        public List<string> parametrs = new List<string>();
        private readonly string[] keys = new string[] { "--help", "--version", "-v", "--verbose", "-p", "--parents" };

        public Command(string[] args)
        {
            parentDir = Directory.GetCurrentDirectory();
            GetAllParamsAndDirect(args);
        }

        private void GetAllParamsAndDirect(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("mkdir: проаущен операнд");
                Console.WriteLine("По команде «mkdir --help» можно получить дополнительную информацию.");
            }
            else
            {
                foreach (string arg in args)
                {
                    if (arg.Contains('-'))
                    {
                        parametrs.Add(arg);
                    }
                    else
                    {
                        directories.Add(arg);
                    }

                }
            }
        }
        public void Run()
        {
            foreach(string par in parametrs)
            {
                if (!keys.Contains(par))
                {
                    Console.WriteLine(
                        "mkdir: неизвестный ключ «{0}» \n" +
                        "По команде «mkdir --help» можно получить дополнительную информацию.", par);

                }
            }
            foreach(string par in parametrs)
            {
                if (par == "--version")
                    version();
                else if (par == "--help")
                    help();
            }
            createDir();
        }
        private void help()
        {
            Console.WriteLine(
                           "Использование: mkdir [КЛЮЧ]... КАТАЛОГ... \n" +
                           "Создает каталог(и), если он ещё не существует. \n \n" +
                           "Аргументы, обязательные для длинных ключей, обязательны и для коротких. \n" +
                           "  -m, --mode=РЕЖИМ  установить права доступа к файлу (как в chmod), \n" +
                           "                    a не a=rwx - umask" +
                           "  -p, --parents     не выдавать ошибку, если существует, создавать \n" +
                           "                    родительские каталоги, если необходимо \n" +
                           "  -v, --verbose     печатать соообщение о каждом созданном каталоге \n" +
                           "  -Z                установить контекст безопасности SELinux \n" +
                           "                    каждого создаваемого каталога равным типу по умолчанию \n" +
                           "      --context[=CTX]  подобно -Z, или если указаг CTX, то установить \n" +
                           "                    контекст безопасноти SELinux \n" +
                           "                    или SMACK равным CTX \n" +
                           "      --help     показать эту справку и выйти \n" +
                           "      --version  показать информацию о версии и выйти \n \n \n" +
                           "Оперативная справка GNU coreutils: <http://www.gnu.org/software/coreutils/> \n" +
                           "Об ошибках в переводе сообщений «mkdir» сообщайте по адресу <gnu@mx.ru> \n" +
                           "Полная документация: <http://www.gnu.org/software/coreutils/mkdir> \n" +
                           "или доступна локально: info '(coreutils) mkdir invocation'");
            Environment.Exit(0);
        }
        private void version()
        {
            Console.WriteLine(
                           "mkdir (GNU coreutils) 8.26 \n" +
                           "Copyright (C) 2016 Free Software Foundation, Inc.\n" +
                           "Лицензия GPLv3+: GNU GPL версии 3 или новее <http:gnu.org/licenses/gpl/html> \n" +
                           "Это свободное ПО: вы можете изменять и распространять его.\n" +
                           "Нет НИКАКИХ ГАРАНТИЙ до степени, разрешенной законом.");
            Environment.Exit(0);
        }
        private void createDir()
        {
            foreach (string dir in directories)
            {
                if (!Directory.Exists(parentDir + "/" + dir))
                {
                    if (!(parametrs.Contains("-p") || parametrs.Contains("--parents")) && dir.Split('/').Length > 1)
                    {
                        parent(dir);
                        continue;
                    }
                    Directory.CreateDirectory(parentDir + "/" + dir);
                    if (parametrs.Contains("-v") || parametrs.Contains("--verbose"))
                    {
                        verbose(dir);
                    }

                }
            }
        }
        private void verbose(string dir)
        {
            string[] dirSplit = dir.Split('/');
            string output = dirSplit[0];
            for (int i = 0; i < dirSplit.Length; ++i)
            {
                Console.WriteLine("mkdir: создан каталог {0}", output);
                output += "/" + dirSplit[i];
            }
        }
        private void parent(string dir)
        {
            Console.WriteLine("mkdir: невозможно создать каталог «{0}»: Нет такого файла или каталога", dir);
        }
    }
    
}