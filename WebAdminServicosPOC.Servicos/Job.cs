using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebAdminServicosPOC.Servicos
{
    public class Job : IJob
    {
        public void Task(string id)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task jobId {0}", id);
            Console.ResetColor();

            Debug.WriteLine($"Task: {DateTime.Today}");
        }
    }
}
