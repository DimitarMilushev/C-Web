using SIS.Framework;
using System;
using TORSHIA.Data;
using TORSHIA.Models;
using TORSHIA.Models.Enums;

namespace TORSHIA.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TorshiaContext();
            //for (int i = 1; i < 8; i++)
            //{
            //    context.Tasks.Add(new Task
            //    {
            //        Title = $"StarWars Episode:{i}",
            //        Description = $"Bad rating {i}/10",
            //        DueDate = DateTime.Now,
            //        Participants = "J. Lucfuk u dumb ass sellout bi4 nigg"               
            //    });
            //}
            //context.SaveChanges();

            WebHost.Start(new StartUp());
        }
    }
}