using System;

namespace EmailJob
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailJobScheduler emailJobScheduler = new EmailJobScheduler();
            emailJobScheduler.Execute();
        }
    }
}
