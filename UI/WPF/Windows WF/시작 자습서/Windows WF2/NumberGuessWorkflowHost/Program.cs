using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NumberGuessWorkflowActivities;

namespace NumberGuessWorkflowHost
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            AutoResetEvent idleEvent = new AutoResetEvent(false);

            var inputs = new Dictionary<string, object>() { { "MaxNumber", 100 } };

            WorkflowApplication wfApp = new WorkflowApplication(new SequentialNumberGuessWorkflow(), inputs);
            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                int Turns = Convert.ToInt32(e.Outputs["Turns"]);
                Console.WriteLine("Congratulations, you guessed the number in {0} turns.", Turns);

                syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                Console.WriteLine(e.Reason);
                syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                Console.WriteLine(e.UnhandledException.ToString());
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.Idle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                idleEvent.Set();
            };

            wfApp.Run();

            int Guess = -1;
            int GuessCheck = -1;

            // Loop until the workflow completes.
            WaitHandle[] handles = new WaitHandle[] { syncEvent, idleEvent };
            while (WaitHandle.WaitAny(handles) != 0)
            {
                // Gather the user input and resume the bookmark.
                bool validEntry = false;
                while (!validEntry)
                {
                    if (Guess == -1)
                    {
                        GuessCheck = -1;
                        if (!Int32.TryParse(Console.ReadLine(), out Guess))
                        {
                            Console.WriteLine("Please enter an Guess integer.");
                        }
                        else
                        {
                            validEntry = true;
                            wfApp.ResumeBookmark("EnterGuess", Guess);
                        }
                    }
                    else if (GuessCheck == -1)
                    {
                        Guess = -1;
                        if (!Int32.TryParse(Console.ReadLine(), out GuessCheck))
                        {
                            Console.WriteLine("Please enter an GuessCheck integer.");
                        }
                        else
                        {
                            validEntry = true;
                            wfApp.ResumeBookmark("EnterGuessCheck", GuessCheck);
                        }
                    }
                }
            }
        }
    }
}
