using System.Collections.Generic;
using Codurance.Bleater.Service;

namespace Codurance.Bleater.Console
{
    //SHEEP ARTIST: jgs
    //http://www.heartnsoul.com/ascii_art/sheep.txt

    class Program
    {
        static void Main()
        {
            System.Console.WriteLine(@"

WELCOME TO BLEATER!!!

                     _,._
                 __.'   _)
                <_,)'.-""a\
                  /' (    \ 
      _.-----..,-'   (`""--^ (BLEAT!!!)
     //              |       
    (|   `;      ,   |
      \   ;.----/  ,/
       ) // /   | |\ \
       \ \\`\   | |/ /
        \ \\ \  | |\/
         `"" `""  `""`
 
o posting: [user name] -> [message]

o reading: [user name]

o following: [user name] follows [another user]

o wall:	[user name] wall

o (press CTRL+Z to exit)

");

            var commandParser = new CommandParser();
            var commandExecutor = new CommandExecutor(new UserRepository(), new PostsFormatter(), WriteLines);

            string stringCommand;
            do
            {
                System.Console.Write("> ");
                stringCommand = System.Console.ReadLine();
                if (stringCommand != null)
                {
                    var command = commandParser.Parse(stringCommand);
                    commandExecutor.Execute(command);
                }
                    
            } while (stringCommand != null);
        }

        private static void WriteLines(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                System.Console.WriteLine(message);
            }
        }
    }
}
