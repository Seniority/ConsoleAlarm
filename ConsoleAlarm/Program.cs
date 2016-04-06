using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace ConsoleAlarm
{
    /// <summary>
    /// Console alarm clock by Seniority Labs. Created out of pure necessity.
    /// Coded by Anthony Senior 4/5/2016
    /// </summary>
 
    class Program
    {
        static string alarmTime;        // the time that the alarm will sound
        static string shutdownCode;     // what the user will need to type to turn off the alarm sound
        static string userInput;        // what the user types to attempt to shut off the alarm sound
        static int snoozeTime;          // determines how many minutes the alarm will snooze for
        static bool keepGoing = true;

        static SoundPlayer alarmSound = new SoundPlayer();

        static void Main(string[] args)
        {
            alarmSound.SoundLocation = "alarmSound.wav";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====\tWelcome to the Console Alarm Clock\t=====");
            Console.WriteLine();
            Console.WriteLine("Before you begin:");
            Console.WriteLine();
            Console.WriteLine("* Make sure your system is not set to shut down, restart, or sleep while application is running.");
            Console.WriteLine("* Adjust your system and speaker volumes in a way that is most effective for you.");
            Console.WriteLine("* If using this application during sleep, dim or turn off your monitor to reduce light disruption.");
            Console.WriteLine("* When prompted to set the alarm time, use the H:MM or HH:MM format (ex: 3:30 or 11:45 or 17:00).");
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Set the time the alarm will sound (HH:MM): ");
            alarmTime = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Set the alarm's shutdown code (used to turn alarm off - case sensitive): ");
            shutdownCode = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Set the alarm's snooze time (in minutes): ");
            snoozeTime = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> Your alarm will sound at " + alarmTime);
            Console.WriteLine(">> Snooze time: " + snoozeTime + " minute(s)");
            Console.WriteLine(">> Your alarm shutdown code is: " + shutdownCode);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to set alarm and begin clock.");
            Console.ReadLine();
            Console.WriteLine();

            while (DateTime.Now.ToShortTimeString() != alarmTime)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now.ToLongTimeString());
                Thread.Sleep(1000);
            }

            alarmSound.PlayLooping();

            while (keepGoing)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Type \"S\" to snooze or \"" + shutdownCode + "\" to turn alarm off: ");
                Console.ForegroundColor = ConsoleColor.White;
                userInput = Console.ReadLine();

                if (userInput == shutdownCode)
                {
                    alarmSound.Stop();
                    keepGoing = false;
                    Console.Write("The alarm has been turned off. Press ENTER to exit application.");
                    Console.ReadLine();
                }
                if (userInput.ToLower() == "s")
                {
                    snooze();
                }
            }
        }// end Main

        static void snooze()
        {
            alarmSound.Stop();

            string future = DateTime.Now.AddMinutes(snoozeTime).ToLongTimeString();

            while (future != DateTime.Now.ToLongTimeString())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now.ToLongTimeString());
                Thread.Sleep(1000);
            }

            alarmSound.PlayLooping();
        }

    }// end Class
}// end Namespace
