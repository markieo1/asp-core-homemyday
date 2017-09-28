using HomeMyDay.Models;
using System;
using System.Linq;

namespace HomeMyDay.Database
{
    public class SeedReviewDbData
    {
        public static void Seed(HomeMyDayDbContext context)
        {
            if (context.Reviews.Any())
            {
                return;
            }

            context.Reviews.Add(new Review()
            {
                Title = "Review 1",                
                Name = "Pieter B.",
                Date = new DateTime(2017, 9, 23),
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis mi vestibulum, finibus leo nec, iaculis ante. Aenean maximus dui in dolor finibus iaculis. Ut nisl risus, ultricies sit amet pretium eu, iaculis vel diam. Ut quis sollicitudin lorem. Nullam fermentum iaculis elit et aliquet. Fusce elementum aliquet nunc ut lacinia. Donec aliquam consectetur vehicula. Curabitur porttitor justo neque, at consequat augue fringilla id. In viverra interdum massa eu sodales. Morbi bibendum feugiat quam, sit amet euismod velit posuere nec. Ut eu urna est. Morbi felis erat, congue a magna eget, facilisis ultrices justo."
            });

            context.Reviews.Add(new Review()
            {
                Title = "Review 2",
                Name = "Ingrid Andriesse",
                Date = new DateTime(2017, 9, 24),
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis mi vestibulum, finibus leo nec, iaculis ante. Aenean maximus dui in dolor finibus iaculis. Ut nisl risus, ultricies sit amet pretium eu, iaculis vel diam. Ut quis sollicitudin lorem. Nullam fermentum iaculis elit et aliquet. Fusce elementum aliquet nunc ut lacinia. Donec aliquam consectetur vehicula. Curabitur porttitor justo neque, at consequat augue fringilla id. In viverra interdum massa eu sodales. Morbi bibendum feugiat quam, sit amet euismod velit posuere nec. Ut eu urna est. Morbi felis erat, congue a magna eget, facilisis ultrices justo."
            });

            context.Reviews.Add(new Review()
            {
                Title = "Review 3",
                Name = "Lennart achternaam",
                Date = new DateTime(2017, 9, 25),
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis mi vestibulum, finibus leo nec, iaculis ante. Aenean maximus dui in dolor finibus iaculis. Ut nisl risus, ultricies sit amet pretium eu, iaculis vel diam. Ut quis sollicitudin lorem. Nullam fermentum iaculis elit et aliquet. Fusce elementum aliquet nunc ut lacinia. Donec aliquam consectetur vehicula. Curabitur porttitor justo neque, at consequat augue fringilla id. In viverra interdum massa eu sodales. Morbi bibendum feugiat quam, sit amet euismod velit posuere nec. Ut eu urna est. Morbi felis erat, congue a magna eget, facilisis ultrices justo."
            });

            context.Reviews.Add(new Review()
            {
                Title = "Review 4",
                Name = "Pieter paulus",
                Date = new DateTime(2017, 9, 26),
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis mi vestibulum, finibus leo nec, iaculis ante. Aenean maximus dui in dolor finibus iaculis. Ut nisl risus, ultricies sit amet pretium eu, iaculis vel diam. Ut quis sollicitudin lorem. Nullam fermentum iaculis elit et aliquet. Fusce elementum aliquet nunc ut lacinia. Donec aliquam consectetur vehicula. Curabitur porttitor justo neque, at consequat augue fringilla id. In viverra interdum massa eu sodales. Morbi bibendum feugiat quam, sit amet euismod velit posuere nec. Ut eu urna est. Morbi felis erat, congue a magna eget, facilisis ultrices justo."
            });

            context.Reviews.Add(new Review()
            {
                Title = "Review 5",
                Name = "Marco Havermans",
                Date = new DateTime(2017, 9, 27),
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis mi vestibulum, finibus leo nec, iaculis ante. Aenean maximus dui in dolor finibus iaculis. Ut nisl risus, ultricies sit amet pretium eu, iaculis vel diam. Ut quis sollicitudin lorem. Nullam fermentum iaculis elit et aliquet. Fusce elementum aliquet nunc ut lacinia. Donec aliquam consectetur vehicula. Curabitur porttitor justo neque, at consequat augue fringilla id. In viverra interdum massa eu sodales. Morbi bibendum feugiat quam, sit amet euismod velit posuere nec. Ut eu urna est. Morbi felis erat, congue a magna eget, facilisis ultrices justo."
            });

            context.SaveChanges();
        }
    }
}
