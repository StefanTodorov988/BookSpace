using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Data.DTO;
using BookSpace.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BookSpace.Data
{
    public class DatabaseSeedService : IDatabaseSeedService
    {
        private readonly BookSpaceContext ctx;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DatabaseSeedService(BookSpaceContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.ctx = ctx;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void SeedData()
        {
            this.SeedBooks();
            this.SeedGenres();
            this.SeedAuthors();
            this.SeedRoles();
            this.SeedUsers();
            this.SeedComments();
            this.SeedGenresBooks();
            this.SeedBookAuthors();
            this.SeedBookUsers();
        }

        private void SeedBookUsers()
        {
            if (ctx.BooksUsers.Any())
            {
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();
                string[] userIds = ctx.Users.Select(u => u.Id).ToArray();

                // For each user add 2 books with different state
                List<BookUser> bookUsers = new List<BookUser>();
                Random rnd = new Random();
                foreach (var uId in userIds)
                {
                    bookUsers.Add(new BookUser
                    {
                        UserId = uId,
                        BookId = bookIds[rnd.Next(0, bookIds.Length / 2)],
                        State = Models.Enums.BookState.Read
                    });

                    bookUsers.Add(new BookUser
                    {
                        UserId = uId,
                        BookId = bookIds[rnd.Next(bookIds.Length / 2, bookIds.Length)],
                        State = Models.Enums.BookState.ToRead
                    });
                }

                ctx.BooksUsers.AddRange(bookUsers);
                ctx.SaveChanges();
            }
        }

        private void SeedBookAuthors()
        {
            if (!ctx.BooksAuthors.Any())
            {
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();
                string[] authorIds = ctx.Authors.Select(a => a.AuthorId).ToArray();

                List<BookAuthor> bookAuthors = new List<BookAuthor>();
                Random rnd = new Random();
                foreach (var bId in bookIds)
                {
                    bookAuthors.Add(new BookAuthor
                    {
                        BookId = bId,
                        AuthorId = authorIds[rnd.Next(0, authorIds.Length)]
                    });
                }

                ctx.BooksAuthors.AddRange(bookAuthors);
                ctx.SaveChanges();
            }
        }

        private void SeedGenresBooks()
        {
            if (!ctx.BooksGenres.Any())
            {
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();
                string[] genreIds = ctx.Genres.Select(b => b.GenreId).ToArray();

                List<BookGenre> booksGenres = new List<BookGenre>();
                Random rnd = new Random();
                foreach (var bId in bookIds)
                {
                    booksGenres.Add(new BookGenre
                    {
                        BookId = bId,
                        GenreId = genreIds[rnd.Next(0, genreIds.Length)]
                    });
                }

                ctx.BooksGenres.AddRange(booksGenres);
                ctx.SaveChanges();
            }
        }

        private void SeedComments()
        {
            if (!ctx.Comments.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Comments.json?sv=2017-07-29&sr=b&sig=uLLXu01htpg8zgc0y5B6n%2BLzU4lSY%2BPSYse5MjTgeSg%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Comment[] allComments = JsonConvert.DeserializeObject<Comment[]>(responseBody);

                string[] usersIds = ctx.Users.Select(i => i.Id).ToArray();
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();

                Random rnd = new Random();

                foreach (var comment in allComments)
                {
                    comment.UserId = usersIds[rnd.Next(0, usersIds.Length)];
                    comment.BookId = bookIds[rnd.Next(0, bookIds.Length)];
                }

                ctx.Comments.AddRange(allComments);
                ctx.SaveChanges();
            }
        }

        private void SeedUsers()
        {
            if (!this.ctx.Users.Any())
            {
                // Seed admin
                var adminEmail = "admin@admin.com";
                var adminUsername = adminEmail;
                var adminPassword = "Asd12@";
                this.CreateUser(adminEmail, adminUsername, adminPassword);
                this.SetRoleToUser("admin@admin.com", "Admin");

                // Add some users
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Users.json?sv=2017-07-29&sr=b&sig=YxSCb%2BO4HLa569eyyW8Gbm4pr7p3B5Mp9%2BuXkE2S8Es%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                var allUsers = JsonConvert.DeserializeObject<UserSeedDto[]>(responseBody);

                foreach (var user in allUsers)
                {
                    this.CreateUser(user.Email, user.Username, "Asd12@");
                    this.SetRoleToUser(user.Email, "User");
                }
                
                ctx.SaveChanges();
            }
        }

        private void SeedRoles()
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        private void SeedAuthors()
        {
            if (!ctx.Authors.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Authors.json?sv=2017-07-29&sr=b&sig=eb5EKh3itVTNsBqzqCEII1OU20z21XzYWUYCMk86ClE%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Author[] allAuthors = JsonConvert.DeserializeObject<Author[]>(responseBody);
                ctx.Authors.AddRange(allAuthors);
                ctx.SaveChanges();
            }
        }

        private void SeedGenres()
        {
            if (!ctx.Genres.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Genres.json?sv=2017-07-29&sr=b&sig=CJ74O0PbWVZoecuXRF9UFagG9mBdp%2BB6RRU2SK5CeCA%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Genre[] allGenres = JsonConvert.DeserializeObject<Genre[]>(responseBody);
                ctx.Genres.AddRange(allGenres);
                ctx.SaveChanges();
            }
        }

        private void SeedBooks()
        {
            if (!ctx.Books.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Books.json?sv=2017-07-29&sr=b&sig=ikTp51MPPEOCowkqL7hym8sGGycWlDN0Ztw2JgeN5%2Fg%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Book[] allBooks = JsonConvert.DeserializeObject<Book[]>(responseBody);
                ctx.Books.AddRange(allBooks);
                ctx.SaveChanges();
            }
        }

        private async Task<string> ReadJsonAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        private void CreateUser(string email, string username, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };

            var result = userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void SetRoleToUser(string email, string role)
        {
            var user = ctx.Users.SingleOrDefault(u => u.Email == email);

            var result = userManager.AddToRoleAsync(user, role).Result;

            if (role == "User")
            {
                user.LockoutEnabled = true;
            }

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}
