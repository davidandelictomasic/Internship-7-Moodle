using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Infrastructure.Database.Seed
{
    public static class DatabaseSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin User", Email = "admin@moodle.com", PasswordHash = "admin123", Role = UserRole.Admin, DateOfBirth = new DateOnly(1980, 1, 15) },
                new User { Id = 2, Name = "Dr. Ivan Horvat", Email = "ivan.horvat@moodle.com", PasswordHash = "prof123", Role = UserRole.Professor, DateOfBirth = new DateOnly(1975, 5, 20) },
                new User { Id = 3, Name = "Dr. Ana Kovač", Email = "ana.kovac@moodle.com", PasswordHash = "prof123", Role = UserRole.Professor, DateOfBirth = new DateOnly(1982, 8, 10) },
                new User { Id = 4, Name = "Dr. Marko Babić", Email = "marko.babic@moodle.com", PasswordHash = "prof123", Role = UserRole.Professor, DateOfBirth = new DateOnly(1978, 3, 25) },
                new User { Id = 5, Name = "Petra Novak", Email = "petra.novak@student.moodle.com", PasswordHash = "student123", Role = UserRole.Student, DateOfBirth = new DateOnly(2000, 4, 12) },
                new User { Id = 6, Name = "Luka Jurić", Email = "luka.juric@student.moodle.com", PasswordHash = "student123", Role = UserRole.Student, DateOfBirth = new DateOnly(2001, 7, 8) },
                new User { Id = 7, Name = "Maja Perić", Email = "maja.peric@student.moodle.com", PasswordHash = "student123", Role = UserRole.Student, DateOfBirth = new DateOnly(1999, 11, 30) },
                new User { Id = 8, Name = "Tomislav Knežević", Email = "tomislav.knezevic@student.moodle.com", PasswordHash = "student123", Role = UserRole.Student, DateOfBirth = new DateOnly(2002, 2, 14) },
                new User { Id = 9, Name = "Sara Matić", Email = "sara.matic@student.moodle.com", PasswordHash = "student123", Role = UserRole.Student, DateOfBirth = new DateOnly(2000, 9, 5) }
            );

           
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Programiranje u C#", ProfessorId = 2 },
                new Course { Id = 2, Name = "Baze podataka", ProfessorId = 2 },
                new Course { Id = 3, Name = "Web razvoj", ProfessorId = 3 },
                new Course { Id = 4, Name = "Algoritmi i strukture podataka", ProfessorId = 3 },
                new Course { Id = 5, Name = "Računalne mreže", ProfessorId = 4 }
            );

            
            modelBuilder.Entity<CourseStudent>().HasData(
                new CourseStudent { UserId = 5, CourseId = 1, EnrolledAt = new DateTime(2025, 9, 1, 10, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 6, CourseId = 1, EnrolledAt = new DateTime(2025, 9, 1, 11, 30, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 7, CourseId = 1, EnrolledAt = new DateTime(2025, 9, 2, 9, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 5, CourseId = 2, EnrolledAt = new DateTime(2025, 9, 1, 10, 15, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 8, CourseId = 2, EnrolledAt = new DateTime(2025, 9, 3, 14, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 9, CourseId = 2, EnrolledAt = new DateTime(2025, 9, 3, 15, 30, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 6, CourseId = 3, EnrolledAt = new DateTime(2025, 9, 2, 8, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 7, CourseId = 3, EnrolledAt = new DateTime(2025, 9, 2, 8, 30, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 8, CourseId = 3, EnrolledAt = new DateTime(2025, 9, 2, 9, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 9, CourseId = 3, EnrolledAt = new DateTime(2025, 9, 2, 10, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 5, CourseId = 4, EnrolledAt = new DateTime(2025, 9, 4, 11, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 6, CourseId = 4, EnrolledAt = new DateTime(2025, 9, 4, 12, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 7, CourseId = 5, EnrolledAt = new DateTime(2025, 9, 5, 9, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 8, CourseId = 5, EnrolledAt = new DateTime(2025, 9, 5, 10, 0, 0, DateTimeKind.Utc) },
                new CourseStudent { UserId = 9, CourseId = 5, EnrolledAt = new DateTime(2025, 9, 5, 11, 0, 0, DateTimeKind.Utc) }
            );

            
            modelBuilder.Entity<Announcement>().HasData(
                new Announcement { Id = 1, CourseId = 1, ProfessorId = 2, Title = "Dobrodošli na kolegij", Content = "Dobrodošli na kolegij Programiranje u C#! Prvi sat je u ponedjeljak.", CreatedAt = new DateTime(2025, 9, 1, 8, 0, 0, DateTimeKind.Utc) },
                new Announcement { Id = 2, CourseId = 1, ProfessorId = 2, Title = "Zadaća 1", Content = "Prva zadaća je objavljena. Rok za predaju je za tjedan dana.", CreatedAt = new DateTime(2025, 9, 8, 10, 0, 0, DateTimeKind.Utc) },
                new Announcement { Id = 3, CourseId = 2, ProfessorId = 2, Title = "Instalacija PostgreSQL", Content = "Molim vas instalirajte PostgreSQL prije sljedećeg predavanja.", CreatedAt = new DateTime(2025, 9, 2, 9, 0, 0, DateTimeKind.Utc) },
                new Announcement { Id = 4, CourseId = 3, ProfessorId = 3, Title = "Projekt - teme", Content = "Odaberite temu za završni projekt do kraja mjeseca.", CreatedAt = new DateTime(2025, 9, 15, 14, 0, 0, DateTimeKind.Utc) },
                new Announcement { Id = 5, CourseId = 4, ProfessorId = 3, Title = "Kolokvij 1", Content = "Prvi kolokvij je zakazan za 15. listopada.", CreatedAt = new DateTime(2025, 9, 20, 11, 0, 0, DateTimeKind.Utc) },
                new Announcement { Id = 6, CourseId = 5, ProfessorId = 4, Title = "Laboratorijske vježbe", Content = "Laboratorijske vježbe počinju sljedeći tjedan.", CreatedAt = new DateTime(2025, 9, 10, 13, 0, 0, DateTimeKind.Utc) }
            );

           
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, CourseId = 1, Name = "Uvod u C#", Url = "https://moodle.com/materials/csharp-intro.pdf", AddedAt = new DateTime(2025, 9, 1, 8, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 2, CourseId = 1, Name = "OOP koncepti", Url = "https://moodle.com/materials/oop-concepts.pdf", AddedAt = new DateTime(2025, 9, 5, 9, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 3, CourseId = 1, Name = "LINQ tutorial", Url = "https://moodle.com/materials/linq-tutorial.pdf", AddedAt = new DateTime(2025, 9, 12, 10, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 4, CourseId = 2, Name = "SQL osnove", Url = "https://moodle.com/materials/sql-basics.pdf", AddedAt = new DateTime(2025, 9, 2, 8, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 5, CourseId = 2, Name = "Entity Framework Core", Url = "https://moodle.com/materials/ef-core.pdf", AddedAt = new DateTime(2025, 9, 9, 11, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 6, CourseId = 3, Name = "HTML & CSS", Url = "https://moodle.com/materials/html-css.pdf", AddedAt = new DateTime(2025, 9, 3, 8, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 7, CourseId = 3, Name = "JavaScript osnove", Url = "https://moodle.com/materials/js-basics.pdf", AddedAt = new DateTime(2025, 9, 10, 9, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 8, CourseId = 4, Name = "Složenost algoritama", Url = "https://moodle.com/materials/algorithm-complexity.pdf", AddedAt = new DateTime(2025, 9, 4, 10, 0, 0, DateTimeKind.Utc) },
                new Material { Id = 9, CourseId = 5, Name = "TCP/IP protokoli", Url = "https://moodle.com/materials/tcp-ip.pdf", AddedAt = new DateTime(2025, 9, 6, 8, 0, 0, DateTimeKind.Utc) }
            );

            
            modelBuilder.Entity<ChatRoom>().HasData(
                new ChatRoom { Id = 1, FirstUserId = 5, SecondUserId = 2 },
                new ChatRoom { Id = 2, FirstUserId = 6, SecondUserId = 3 },
                new ChatRoom { Id = 3, FirstUserId = 5, SecondUserId = 6 },
                new ChatRoom { Id = 4, FirstUserId = 7, SecondUserId = 4 }
            );

            
            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, ChatRoomId = 1, SenderId = 5, Content = "Poštovani profesore, imam pitanje vezano uz zadaću.", SentAt = new DateTime(2025, 9, 10, 14, 0, 0, DateTimeKind.Utc) },
                new Message { Id = 2, ChatRoomId = 1, SenderId = 2, Content = "Naravno, slobodno pitajte.", SentAt = new DateTime(2025, 9, 10, 14, 15, 0, DateTimeKind.Utc) },
                new Message { Id = 3, ChatRoomId = 1, SenderId = 5, Content = "Može li se zadaća predati dan kasnije?", SentAt = new DateTime(2025, 9, 10, 14, 20, 0, DateTimeKind.Utc) },
                new Message { Id = 4, ChatRoomId = 1, SenderId = 2, Content = "Da, ali uz umanjenje bodova od 10%.", SentAt = new DateTime(2025, 9, 10, 14, 30, 0, DateTimeKind.Utc) },
                new Message { Id = 5, ChatRoomId = 2, SenderId = 6, Content = "Profesorice, kada su konzultacije?", SentAt = new DateTime(2025, 9, 11, 10, 0, 0, DateTimeKind.Utc) },
                new Message { Id = 6, ChatRoomId = 2, SenderId = 3, Content = "Konzultacije su utorkom od 12-14h.", SentAt = new DateTime(2025, 9, 11, 10, 30, 0, DateTimeKind.Utc) },
                new Message { Id = 7, ChatRoomId = 3, SenderId = 5, Content = "Hej, jesi riješio zadaću iz C#?", SentAt = new DateTime(2025, 9, 12, 18, 0, 0, DateTimeKind.Utc) },
                new Message { Id = 8, ChatRoomId = 3, SenderId = 6, Content = "Radim na tome, treći zadatak mi nije jasan.", SentAt = new DateTime(2025, 9, 12, 18, 10, 0, DateTimeKind.Utc) },
                new Message { Id = 9, ChatRoomId = 3, SenderId = 5, Content = "Mogu ti pomoći, javi se na Discordu.", SentAt = new DateTime(2025, 9, 12, 18, 15, 0, DateTimeKind.Utc) },
                new Message { Id = 10, ChatRoomId = 4, SenderId = 7, Content = "Profesore, nedostaje mi materijal s prošlog predavanja.", SentAt = new DateTime(2025, 9, 13, 9, 0, 0, DateTimeKind.Utc) },
                new Message { Id = 11, ChatRoomId = 4, SenderId = 4, Content = "Uploadat ću ga danas do kraja dana.", SentAt = new DateTime(2025, 9, 13, 9, 45, 0, DateTimeKind.Utc) }
            );
        }
    }
}
 


