using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameMkv
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any()
             || string.Join("", args).Equals("--help")
             || string.Join("", args).Equals("-?")
             || string.Join("", args).Equals("?")
             || string.Join("", args).Equals("/?")
            )
            {
                Console.WriteLine("\r\nHelp");
                Console.WriteLine("\r\nUsage: RenameMkv [directoryname]");
                Console.WriteLine("\r\n       RenameMkv [directoryname/*.mkv]");
                Console.WriteLine("\r\n       RenameMkv [directoryname/?something.mkv]");
                Console.WriteLine("\r\n       RenameMkv [filename]");
                return;
            }
            var filelist = new List<string>();
            var rl =
                @"\\gen\d\Library.Movies\Desperately Seeking Susan (1985)\Desperately Seeking Susan (1985) 1080p AC3.mkv"
            ;
            rl =
                @"\\gen\d\Library.Movies\Desperately Seeking Susan (1985)\*.mkv";

            rl = args[0];
            if (rl.Contains("?") || rl.Contains("*"))
            {
                var pattern = Path.GetFileName(rl);
                var parent = Path.GetDirectoryName(rl);
                filelist = Directory.GetFiles(parent, pattern).ToList();
            }

            if (Directory.Exists(rl))
            {
                filelist = Directory.GetFiles(rl, "*.mkv").ToList()
                    .Union(Directory.GetFiles(rl, "*.mp4").ToList())
                    .ToList();
            }

            if (File.Exists(rl))
            {
                filelist.Add(rl);
            }

            foreach (var fileToProcess in filelist)
            {
                var tfile = TagLib.File.Create(fileToProcess);
                string title = tfile.Tag.Title;
                var newname = title.CleanName().Trim();
                if (!Path.GetExtension(newname)
                    .Equals(Path.GetExtension(fileToProcess), StringComparison.InvariantCultureIgnoreCase))
                    newname += Path.GetExtension(fileToProcess);
                TimeSpan duration = tfile.Properties.Duration;
                var targetFileName = Path.Combine(Path.GetDirectoryName(fileToProcess), newname);
                tfile.Dispose();
                tfile = null;
                
                try
                {
                    if(!File.Exists(targetFileName)) File.Move(fileToProcess, targetFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine();
                    Console.WriteLine();

                }

                Console.WriteLine("Title: {0}, duration: {1} => {2}", title, duration, newname);
            }

        }
    }
}
